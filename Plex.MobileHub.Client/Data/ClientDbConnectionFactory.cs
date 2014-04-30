﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Data;
using System.Xml.Serialization;
using Oracle.DataAccess.Client;
using System.Globalization;

using MobileHubClient.Logs;
namespace MobileHubClient.Data
{
    public class ClientDbConnectionFactory
    {
        const string EnvironmentVariablePath = "path";
        const string Lsnrctl = "lsnrctl.exe";

        static ClientDbConnectionFactory instance = new ClientDbConnectionFactory();
        public static ClientDbConnectionFactory Instance
        {
            get
            {
                return instance;
            }
        }

        public static void Reset()
        {
            instance = new ClientDbConnectionFactory();
        }

        private ClientDbConnectionFactory()
        {
            Discover();
            Sources = new List<string>();
            Companies = new List<string>();
            Maps = new List<DataSourceMap>();
        }

        public IDbConnection GetConnection(string companyCode, bool isOpen = true)
        {
            int CompanyIndex = Companies.IndexOf(companyCode);
            var Source = Sources[Maps.FindAll(p => p.CompanyIndex == CompanyIndex)[1].SourceIndex];
            var Conn = new OracleConnection(Source);
            if (isOpen) Conn.Open();
            return Conn;
        }

        public IEnumerable<IDbConnection> GetUniqueConnections()
        {
            List<string> Results = new List<string>();
            foreach(var ConnectionString in Sources)
            {
                var Conn = new OracleConnection(ConnectionString);
                Conn.Open();
                var dbInfo = Conn.Query("select * from v$database").ToString();
                if(Results.Contains(dbInfo)) continue;
                Results.Add(dbInfo);
                yield return Conn;
            }
        }

        public static IDbConnection ActiveConnection(string connectionString)
        {
            var v = new OracleConnection(connectionString);
            v.Open();
            return v;
        }

        public List<String> Sources
        {
            get;
            set;
        }
        public List<String> Companies
        {
            get;
            set;
        }
        public List<DataSourceMap> Maps
        {
            get;
            set;
        }


        public void init()
        {
        }

        public void Discover()
        {
            List<String> Sour = new List<String>();
            List<String> Comp = new List<String>();
            List<DataSourceMap> Maps = new List<DataSourceMap>();
            foreach (var lsnr in GetListeners())
            {
                var Status = lsnr.LsnrCtlGetStatus();
                foreach (var Cstring in GenerateConnectionStrings(Listener.ExtractServiceNames(Status), Listener.ExtractEndPointSummary(Status)))
                    try
                    {
                        LogManager.Instance.Add("Created string Cstring : " + Cstring);
                        using (var Conn = new OracleConnection(Cstring))
                        {
                            Conn.Open();
                            if (!IsPMHC(Conn)) continue;
                            LogManager.Instance.Add("PMH is pressent");
                            foreach (var c in GetCodes(Conn))
                            {
                                if (!Comp.Exists(p => c == p))
                                    Comp.Add(c);
                                if (!Sour.Exists(p => Cstring == p))
                                    Sour.Add(Cstring);
                                Maps.Add(new DataSourceMap() { CompanyIndex = Comp.LastIndexOf(c), SourceIndex = Sour.LastIndexOf(Cstring) });
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        LogManager.Instance.Add(e);
                    }
            }
            this.Companies = Comp;
            this.Sources = Sour;
            this.Maps = Maps;
        }

        static IEnumerable<string> GenerateConnectionStrings(IEnumerable<string> ServiceNames, IEnumerable<string> Endpoints)
        {
            string d = @"User Id=jas;Password=!!!plex!!!sa;Connect Timeout=5;Data Source=*";
            string s = @"(CONNECT_DATA=(SERVICE_NAME=*))";

            foreach (var n in ServiceNames)
                foreach (var ep in Endpoints)
                    yield return d.Replace("*", ep.Insert(ep.LastIndexOf(')'), "*").Replace("*", s.Replace("*", n))).Replace("(", " (");
        }

        static IEnumerable<Listener> GetListeners()
        {
            var Path = Environment.GetEnvironmentVariable(EnvironmentVariablePath);
            var PathFolders = Path.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var FolderPath in PathFolders)
                if (Directory.Exists(FolderPath))
                    foreach (var FilePath in Directory.GetFiles(FolderPath, Lsnrctl))
                            yield return new Listener() { Executable = FilePath };
        }

        static bool IsPMHC(IDbConnection Conn)
        {
            string sql = @"select count(*) as IS_PMHC from (select 1 from dba_source where name = upper(:pmh) and rownum<=1)";
            return Convert.ToBoolean(Conn.Query(sql,"pmh")[0,0]);
        }
        
        static IEnumerable<string> GetCodes(IDbConnection Conn)
        {
            var Collection = new List<String>();
            var Result = Conn.Query(@"select c.code from company c")["CODE"];
            foreach (string row in Result)
                Collection.Add(row);
            return Collection;
        }
       
        public override string ToString()
        {
            var stringwriter = new StringWriter(CultureInfo.CurrentCulture);
            new XmlSerializer(this.GetType()).Serialize(stringwriter, this);
            return stringwriter.ToString();
        }
    }
}
