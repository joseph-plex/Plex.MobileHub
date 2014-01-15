using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Xml;
using System.Xml.Serialization;

using Plex.PMH.Repositories;
using Plex.PMH.Data;
using Plex.PMH.Data.Tables;
using Plex.PMH.Objects;

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
        public List<object> Get1() { return new List<Object>(APP_QUERIES.GetAll()); }
        [WebMethod]
        public List<object> Get2() { return new List<Object>(APP_QUERY_COLUMNS.GetAll()); }
        [WebMethod]
        public List<object> Get3() { return new List<Object>(APP_QUERY_CONDITIONS.GetAll()); }
        [WebMethod]
        public List<object> Get4() { return new List<Object>(APP_TABLE_COLUMNS.GetAll()); }
        [WebMethod]
        public int GetSequence() { return Utilities.SequenceNextValue(Sequences.ID_GEN); }
        [WebMethod]
        public int GetSequence2() { return Utilities.SequenceNextValue(Sequences.DEVICE_ID); }

        [WebMethod]
        public List<object> Get5() { return new List<Object>(APP_TABLES.GetAll()); }
        [WebMethod]
        public List<object> Get6() { return new List<Object>(APP_USER_TYPES.GetAll()); }
        [WebMethod]
        public List<object> Get7() { return new List<Object>(APPS.GetAll()); }
        [WebMethod]
        public List<object> Get8() { return new List<Object>(CLIENT_APPS.GetAll()); }

        [WebMethod]
        public List<object> Get9() { return new List<Object>(CLIENT_DB_COMPANIES.GetAll()); }
        [WebMethod]
        public List<object> Get10() { return new List<Object>(CLIENT_DB_COMPANY_USER_APPS.GetAll()); }
        [WebMethod]
        public List<object> Get11() { return new List<Object>(CLIENT_DB_COMPANY_USERS.GetAll()); }
        [WebMethod]
        public List<object> Get12() { return new List<Object>(CLIENT_USERS.GetAll()); }

        [WebMethod]
        public List<object> Get13() { return new List<Object>(CLIENTS.GetAll()); }
        [WebMethod]
        public List<object> Get14() { return new List<Object>(LOGS.GetAll()); }
        [WebMethod]
        public List<object> Get15() { return new List<Object>(PMH_SETTINGS.GetAll()); }
        [WebMethod]
        public List<object> Get16() { return new List<Object>(QUERY_SEQUENCE_REQUESTS.GetAll()); }


        [WebMethod]
        public List<object> Get17() { return new List<object>(CLIENTS.GetAll().ToList().Find((p) => p.CLIENT_ID == 9999).GetCLIENT_USERS()); }

        [WebMethod]
        public List<object> Get18() { return new List<object>(CLIENTS.GetAll().ToList().Find((p)=> p.CLIENT_ID == 9999).GetCLIENT_APPS()); }

        [WebMethod]
        public List<object> Get19() { return new List<object>(CLIENTS.GetAll().ToList().Find((p) => p.CLIENT_ID == 9999).GetCLIENT_DB_COMPANIES()); }
        //public AppUI.Column[] GetAllClients()
        //{
        //    //AppUI.Column a = new AppUI.Column();
        //    throw new NotImplementedException();

        //}
        


        //[WebMethod]
        //public List<Access.Apps> AppsGet()
        //{
        //    return Access.Apps.GetAll();
        //}

        //[WebMethod]
        //public bool AppCreate(Access.Apps a)
        //{
        //    if (!Access.Apps.GetAll().Exists((d) => d.AppId == a.AppId))
        //        return a.Insert();
        //    return false;
        //}

        //[WebMethod]
        //public bool AppDelete(Access.Apps a)
        //{
        //    if (Access.Apps.GetAll().Exists((d) => d.AppId == a.AppId))
        //        return a.Delete();
        //    return true;
        //}

        //[WebMethod]
        //public bool AppUpdate(Access.Apps a)
        //{
        //    if (Access.Apps.GetAll().Exists((d) => d.AppId == a.AppId))
        //        return a.Update();
        //    return false;
        //}

        //[WebMethod]
        //public List<Access.Clients> ClientsGet()
        //{
        //    return Access.Clients.GetAll();
        //}

        //[WebMethod]
        //public bool ClientCreate(Access.Clients c)
        //{
        //    return c.Insert();
        //}

        //[WebMethod]
        //public bool ClientDelete(Access.Clients c)
        //{
        //    if (Access.Clients.GetAll().Exists((a) => a.ClientId == c.ClientId)) 
        //        return c.Delete();
        //    return true;
        //}

        //[WebMethod]
        //public List<Access.Logs> LogsGet()
        //{
        //    return Access.Logs.GetAll();
        //}
        public Manager GetManager()
        {
            return (Manager)Application["Manager"];
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
        public QueryResult QueryPMH(string sql)
        {
            return Utilities.Query(sql);
        }
    }
}
