﻿using Plex.Diagnostics;
using Plex.MobileHub.Data;
using Plex.MobileHub.Data.Tables;
using Plex.MobileHub.Data.Types;
using Plex.MobileHub.Objects;
using Plex.MobileHub.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Services;
using System.Xml;
using System.Xml.Serialization;
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
        public string GetExternalIP()
        {
            using (var sr = new System.IO.StreamReader(System.Net.WebRequest.Create("http://checkip.dyndns.org").GetResponse().GetResponseStream()))
                return sr.ReadToEnd().Trim().Split(':')[1].Substring(1).Split('<')[0];
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
        public Result Query(string sql, object[] arguments)
        {
            using (var Conn = Utilities.GetConnection(true))
                return Conn.Query(sql, arguments);
        }

        [WebMethod]
        public int NonQuery(string sql, object[] arguments)
        {
            using (var Conn = Utilities.GetConnection(true))
                return Conn.NonQuery(sql, arguments);
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
        public string Test()
        {
            return new APP_QUERIES().GetUpdateText();
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

        [WebMethod]
        public int AppAdd(string authKey, string title, string description, bool isClientCustomApp)
        {
            APPS apps = new APPS(){ AUTH_KEY = authKey, TITLE = title, DESCRIPTION = description, IS_CLIENT_CUSTOM_APP = Convert.ToInt32(isClientCustomApp)};
            using (var conn = Utilities.GetConnection(true)) { 
                apps.APP_ID = Convert.ToInt32(conn.Query("select id_gen.nextval from dual")[0, 0]);
                apps.Insert(conn);
            }
            return apps.APP_ID;
        }

        [WebMethod]
        public int AppTableAdd(int appId, string name, string desc, int insert, int update, int delete, int select)
        {
            APP_TABLES aTables = new APP_TABLES() 
            { 
                APP_ID = appId, 
                NAME = name, 
                DESCRIPTION = desc, 
                INSERT_ALLOWED = insert, 
                UPDATE_ALLOWED = update, 
                DELETE_ALLOWED = delete,
                QUERY_ALLOWED = select 
            };
            using (var conn = Utilities.GetConnection(true))
            {
                aTables.TABLE_ID = Convert.ToInt32(conn.Query("select id_gen.nextval from dual")[0, 0]);
                aTables.Insert(conn);
            }
            return aTables.TABLE_ID;
        }

        [WebMethod]
        public int AppTableColumnAdd(int tabId, string nom, int seq, string type, int length, int prec, int scale, int nullable, int readOnly, int isLong, int isKey, string keyType, int unique, string desc)
        {
            using (var conn = Utilities.GetConnection(true)) 
            { 
                APP_TABLE_COLUMNS column = new APP_TABLE_COLUMNS()
                {
                    TABLE_COLUMN_ID =  Convert.ToInt32(conn.Query("select id_gen.nextval from dual")[0, 0]),
                    TABLE_ID = tabId,
                    COLUMN_NAME = nom,
                    COLUMN_SEQUENCE = seq,
                    DATA_TYPE = type,
                    DATA_LENGTH = length,
                    DATA_PRECISION = prec,
                    DATA_SCALE = scale,
                    ALLOW_DB_NULL = nullable,
                    IS_READ_ONLY = readOnly, 
                    IS_LONG = isLong,
                    IS_KEY = isKey,
                    KEY_TYPE = keyType,
                    IS_UNIQUE = unique,
                    DESCRIPTION = desc
                };
                column.Insert(conn);
                return column.TABLE_COLUMN_ID;
            }
        }

        [WebMethod]
        public int ClientAdd(int clientId, string desc, string key)
        {
            new CLIENTS(){ CLIENT_ID = clientId, DESCRIPTION = desc, CLIENT_KEY = key }.Insert();
            return clientId;
        }

        [WebMethod]
        public int ClientAppsAdd(int appId, int clientId)
        {
            using(var conn = Utilities.GetConnection(true))
            {
                var cApps = new CLIENT_APPS() 
                {
                    CLIENT_APP_ID = Convert.ToInt32(conn.Query("select id_gen.nextval from dual")[0, 0]),
                    APP_ID = appId,
                    CLIENT_ID = clientId
                };
                cApps.Insert(conn);
                return cApps.CLIENT_APP_ID;
            }
        }

        [WebMethod]
        public void AppRemove(int id)
        {
            var app = APPS.GetAll().FirstOrDefault(p => p.APP_ID == id);
            if (app == null) throw new Exception("Invalid Application Id");
            app.Delete();
        }

        [WebMethod]
        public void AppTableRemove(int id)
        {
            var appTable = APP_TABLES.GetAll().FirstOrDefault(p => p.TABLE_ID == id);
            if (appTable == null) throw new Exception("Invalid Application Table Id");
            appTable.Delete();
        }

        [WebMethod]
        public void AppTableColumnRemove(int id)
        {
            var appTableColumn = APP_TABLE_COLUMNS.GetAll().FirstOrDefault(p => p.TABLE_COLUMN_ID == id);
            if (appTableColumn == null) throw new Exception("Invalid Application Table Column Id");
            appTableColumn.Delete();
        }

        [WebMethod]
        public void Clients(int id)
        {
            var client = CLIENTS.GetAll().FirstOrDefault(p => p.CLIENT_ID == id);
            if (client == null) throw new Exception("Client does not exist");
            client.Delete();
        }

        [WebMethod]
        public void ClientAppRemove(int id) 
        {
            var clientApp = CLIENT_APPS.GetAll().FirstOrDefault(p => p.CLIENT_APP_ID == id);
            if (clientApp == null) throw new Exception("Client App permission does not exist");
            clientApp.Delete();
        }
    }
}
