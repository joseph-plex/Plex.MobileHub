using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Xml;
using System.Xml.Serialization;

using System.Reflection;


using Plex.PMH.Repositories;
using Plex.PMH.Data;
using Plex.PMH.Data.Tables;
using Plex.PMH.Objects;
using Plex.PMH.Data.Types;
namespace Plex.PMH
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
     
        public class ClientDpInfo
        {
            public String Name;
            public Int32 ClientId;

            public Boolean Online;
            public DateTime Connected;
            public DateTime OnlineDuration;
        }

        [WebMethod]
        public List<Command> GetCommandRepo()
        {
            return Commands.Instance.CommandRepo.Values.ToList();
        }
        [WebMethod]
        public List<ConnectionData> GetConnectionRepository()
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
            return Logs.GetInstance().GetLogs();
        }
        [WebMethod]
        public List<Response> GetResponseRepository()
        {
            return Responses.Instance.GetRepo().Values.ToList();
        }

        [WebMethod]
        public List<Log> LogsGetAll()
        {
            return Logs.GetInstance().GetLogs();
        }

        [WebMethod]
        public Result QueryPMH(string sql)
        {
            return Utilities.GetConnection(true).Query(sql);
        }
    }
}
