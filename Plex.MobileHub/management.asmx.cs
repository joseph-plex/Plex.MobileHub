﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Xml;
using System.Xml.Serialization;

using System.Reflection;


using Plex.MobileHub.Repositories;
using Plex.MobileHub.Data;
using Plex.MobileHub.Data.Tables;
using Plex.MobileHub.Objects.Clients;
using Plex.MobileHub.Objects;
using Plex.MobileHub.Data.Types;

using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using Plex.Diagnostics;
namespace Plex.MobileHub
{
    /// <summary>
    /// Summary description for ManagerSDK
    /// </summary>
    [WebService(Namespace = "http://pmh.plexxis.com")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ManagerSDK : WebService
    {
        [XmlInclude(typeof(APP_QUERIES))]
        [XmlInclude(typeof(APP_QUERY_COLUMNS))]
        [XmlInclude(typeof(APP_QUERY_CONDITIONS))]
        [XmlInclude(typeof(APP_TABLE_COLUMNS))]

        [XmlInclude(typeof(APP_TABLES))]
        [XmlInclude(typeof(APP_USER_TYPES))]
        [XmlInclude(typeof(APPS))]
        [XmlInclude(typeof(CLIENT_APPS))]

        [XmlInclude(typeof(CLIENT_DB_COMPANIES))]
        [XmlInclude(typeof(CLIENT_DB_COMPANY_USER_APPS))]
        [XmlInclude(typeof(CLIENT_DB_COMPANY_USERS))]
        [XmlInclude(typeof(CLIENT_USERS))]

        [XmlInclude(typeof(CLIENTS))]
        [XmlInclude(typeof(LOGS))]
        [XmlInclude(typeof(PMH_SETTINGS))]
        [XmlInclude(typeof(QUERY_SEQUENCE_REQUESTS))]


        [WebMethod]
        public void ValidateDataStructures()
        {
            Utilities.AreTablesCorrect();
            Utilities.AreObjectsCorrect();
        }

        [WebMethod]
        public string GetExternalIP()
        {
            using (var sr = new System.IO.StreamReader(System.Net.WebRequest.Create("http://checkip.dyndns.org").GetResponse().GetResponseStream()))
                return sr.ReadToEnd().Trim().Split(':')[1].Substring(1).Split('<')[0];
        }
     
        public class ClientDpInfo
        {
            public String Name;
            public Int32 ClientId;

            public Boolean Online;
            public DateTime Connected;
            public DateTime OnlineDuration;
        }
        [WebMethod]
        public List<Task> GetTaskData()
        {
            return TimeAnalyst.Tasks.ToList(); ;
        }
        [WebMethod]
        public List<Command> GetCommandRepo()
        {
            return Commands.Instance.CommandRepo.Values.ToList();
        }
        [WebMethod]
        public List<Client> GetConnectionRepository()
        {
            return Connections.Instance.GetAll().Values.ToList();
        }
        [WebMethod]
        public List<Consumer> GetConsumerRepository()
        {
            return Consumers.Instance.GetAll().Values.ToList();
        }
        [WebMethod]
        public List<Log> GetLogRepository()
        {
            return Logs.Instance.GetLogs();
        }
        [WebMethod]
        public List<Response> GetResponseRepository()
        {
            return Responses.Instance.GetRepo().Values.ToList();
        }

        [WebMethod]
        public List<Log> LogsGetAll()
        {
            return Logs.Instance.GetLogs();
        }

        [WebMethod]
        public Result QueryPMH(string sql)
        {
            return Utilities.GetConnection(true).Query(sql);
        }
        [WebMethod]
        public void RetrieveCommand(int Id) 
        {
            Connections.Instance.Retrieve(Id).RequestPull();
        }

        [WebMethod]
        public void ResetLogs()
        {
            Logs.Instance.logs.Clear();
        }

        [WebMethod]
        public Result Test()
        {
            using (var conn = Utilities.GetConnection(true))
            {
                using (var command = (OracleCommand)conn.CreateCommand("select * from all_cons_columns where constraint_name in (:a)"))
                {
                    var values = new string[] { "PA_COMPANY_ID", "COMPANYID_FK" };
                    OracleParameter param = new OracleParameter();
                    param.OracleDbType = OracleDbType.Varchar2;
                    param.Value = values;
                    command.ArrayBindCount = values.Length;

                    //command.Parameters.Add(command.CreateParameter("COMPANYID_FK"));
                    //IEnumerable<string> t = new string[] { "COMPANYID_FK", "PA_COMPANY_ID" };
                    command.Parameters.Add(param);

                    return new Result(command.ExecuteReader());
                }
            }
        }

        [WebMethod]
        public object GetReferenceTree()
        {
            XmlDocument Tree = new XmlDocument();

            using (var Conn = Utilities.GetConnection(true))
            {
                var user_constraints = Conn.Query("select c.* from user_tables t,user_constraints c where c.TABLE_NAME = t.TABLE_NAME and c.CONSTRAINT_TYPE = 'R'");

            }

            return Tree;
        }
    }
}