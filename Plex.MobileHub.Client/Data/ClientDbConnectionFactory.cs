using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Data;
using System.Xml.Serialization;
using Oracle.DataAccess.Client;
using System.Globalization;
using Plex.Logs;
using MobileHubClient.Core;
using System.Timers;
namespace MobileHubClient.Data
{
    public class ClientDbConnectionFactory
    {
        const string EnvironmentVariablePath = "path";
        const string Lsnrctl = "lsnrctl.exe";
        Timer DbCheckTimer;
        public ClientDbConnectionFactory()
        {
            CompanyConnectionPairings = new List<KeyValuePair<String, String>>();
            Task.Run(()=> Discover());

            DbCheckTimer = new Timer();
            DbCheckTimer.Elapsed += DbCheckTimer_Elapsed;
            DbCheckTimer.AutoReset = true;
            DbCheckTimer.Interval = 300000;
            DbCheckTimer.Enabled = true;
        }

        void DbCheckTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (CompanyConnectionPairings.Count == 0)
                Discover();
        }

        public static IDbConnection ActiveConnection(String connectionString)
        {
            var v = new OracleConnection(connectionString);
            v.Open();
            return v;
        }

        public List<KeyValuePair<String, String>> CompanyConnectionPairings
        {
            get;
            set;
        }

        public ClientDbConnectionFactory Discover()
        {
            lock (this) { 
                using(LogCache cache = ClientService.Logs.CreateLogCache())
                {
                    cache.Add("Starting Discovery");
                  
                    var Pairings = new List<KeyValuePair<String, String>>();
                    Parallel.ForEach(GetListeners(), p=> Pairings.AddRange(ParellelListenerHandle(p)));
                    CompanyConnectionPairings = Pairings;
                    
                    cache.Add("Discovery Complete");
                    cache.CommitCache();
                    return this;
                }
            }
        }

        IEnumerable<KeyValuePair<String, String>> ParellelListenerHandle(Listener lsnr)
        {
            using(LogCache cache = ClientService.Logs.CreateLogCache())
            {
                cache.Add("Reading from Lsnrctl : " + lsnr.Executable);
                
                var status = lsnr.LsnrCtlGetStatus();
                var values = new List<KeyValuePair<String, String>>();
                var ServiceNames = Listener.ExtractServiceNames(status);
                var EndPointSummary = Listener.ExtractEndPointSummary(status);
                var ConnectionStrings =  GenerateConnectionStrings(ServiceNames, EndPointSummary);
                Parallel.ForEach(ConnectionStrings, p => values.AddRange(IsMobileHubComponentInstalled(p)));

                if (values.Count == 0)
                    cache.Add(lsnr.Executable + " has 0/" + ConnectionStrings.Count() + " usable Companies strings");
                else
                    foreach (var kvp in values)
                        cache.Add("Found Code " + kvp.Key + " in connection string " + kvp.Value);

                cache.CommitCache();
                return values;
            }
        }
        IEnumerable<KeyValuePair<String, String>> IsMobileHubComponentInstalled(String connectionString)
        {
            List<KeyValuePair<String, String>> values = new List<KeyValuePair<String, String>>();
            try
            {
                using (LogCache cache = ClientService.Logs.CreateLogCache())
                using (var Conn = new OracleConnection(connectionString))
                {
                    Conn.Open();
                    if (!IsPMHC(Conn))
                        return new KeyValuePair<String,String>[0];
                    foreach (var code in GetCodes(Conn))
                        values.Add(new KeyValuePair<string, string>(code, connectionString));
                    return values;
                }
            }
            catch(OracleException)
            {
                return new KeyValuePair<String, String>[0];
            }
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
