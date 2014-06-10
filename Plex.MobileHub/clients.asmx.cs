using Plex.MobileHub.Data;
using Plex.MobileHub.Data.Types;
using Plex.MobileHub.Data.Tables;
using Plex.MobileHub.Functionality.Clients;
using Plex.MobileHub.Objects;
using Plex.MobileHub.Objects.Synchronization;
using Plex.MobileHub.Repositories;
using System;
using System.Collections.Generic;
using System.Web.Services;
using System.Xml.Serialization;
using System.Linq;
namespace Plex.MobileHub
{
    /// <summary>
    /// Summary description for Service1
    /// </summary>
    [WebService(Namespace = "http://pmh.plexxis.com")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ClientSDK : WebService
    {
        [XmlInclude(typeof(IUDData))]
        [XmlInclude(typeof(RQryResult))]

        [WebMethod]
        public int Login(int ClientId, string Key, string IPv4, int Port)
        {
            return Functions.ClientLogin(ClientId, Key, IPv4, Port);
        }

        [WebMethod]
        public void Logout(int ConnectionId)
        {
            Functions.ClientLogout(ConnectionId);
        }

        [WebMethod]
        public List<Command> GetCommands(int ConnectionId)
        {
            return new GetCommands().Strategy(ConnectionId);
        }

        [WebMethod]
        public void Respond(Response Resp)
        {
            Logs.Instance.Add("Full Response " + Resp.Id + " Recieved");
            Functions.ClientRespond(Resp);
        }
        [WebMethod]
        public void ResponsePartial(ResponseComponent Component)
        {
            Logs.Instance.Add("partial Response " +Component.Id+ " Component " +  Component.ComponentId + "Recieved");
            Responses.Instance.Add(Component.Id, Component);
            Logs.Instance.Add(Component.Resp);
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
        public ClientSynchroData SyncDataGet()
        {
            return Functions.SyncInfoGet();
        }

       

        #region APP_QUERIES CRUD

        //todo implement 
        [WebMethod]
        public void AppQueriesRetrieve() { }
        #endregion

        
        #region APP_USER_TYPES CRUD
        [WebMethod]
        public void AppUserTypesRetrieve() 
        {
            //todo implement AppUserTypesRetrieve
        }
        #endregion


        #region CLIENTS CRUD
        public void ClientsRetrieve()
        {
            //todo implement ClientsRetrieve
        }
        #endregion


        #region CLIENT_APPS CRUD
        public void ClientsAppsRetrieve()
        {
            //todo imlplement ClientsAppsRetrieve
        }
        #endregion


        #region CLIENT_DB_COMPANIES CRUD

        [WebMethod]
        public bool AddClientDbCompany(int clientId, string companyCode, string ConnectionString)
        {
            if (!Connections.Instance.ConnectionExists(clientId))
                return false;

            var client = Connections.Instance.Retrieve(clientId);
            if (!client.IsConnected)
                return false;

            using (var Conn = Utilities.GetConnection(true))
            {
                new CLIENT_DB_COMPANIES()
                {
                    COMPANY_CODE = companyCode,
                    DATABASE_SID = ConnectionString,
                    CLIENT_ID = clientId,
                    DB_COMPANY_ID = Convert.ToInt32(Conn.Query("select id_gen.nextval from dual")[0, 0])
                };
            }
            return true;
        }
       
        [WebMethod]
        public void ClientDbCompanyAdd(int clientId, string companyCode, string connectionString)
        {
            //todo implement  ClientDbCompanyAdd
        }

        [WebMethod]
        public void ClientDbCompanyRemove() 
        {
            //todo implement ClientDbCompanyRemove
        }
        #endregion


        #region CLIENT_DB_COMPANY_USERS CRUD
        [WebMethod]
        public void ClientDbCompanyUserAdd(int appId, int dcCompanyUserId, int appUserTypeId)
        {
            //todo implement ClientDbCompanyUserAdd
            return;
        }

        [WebMethod]
        public void ClientDbCompanyUserRemove()
        {
            //todo implement ClientDbCompanyUserRemove
            return;
        }
        #endregion

        #region CLIENTS_DB_COMPANY_USER_APPS CRUD
        [WebMethod]
        public void ClientDbCompanyUserAppsAdd()
        {
            //todo implement ClientDbCompanyUserAppsAdd
            return;
        }
        [WebMethod]
        public void ClientDbCompanyUserAppsRemove()
        {
            //todo implement ClientDbCompanyUserAppsRemove
            return;
        }
        #endregion


        #region CLIENT_USERS CRUD
        [WebMethod]
        public void ClientUserAdd(int clientId, string name, string password)
        {
            //todo implement ClientUserAdd
            return;
        }

        [WebMethod]
        public void ClientUserRemove(int clientUserId)
        {
            RemoveClientUser strategy = new RemoveClientUser();
            strategy.Strategy(clientUserId);
            return;
        }
        public List<CLIENT_USERS> ClientUserRetrieveAllForClient(int clientId)
        {
            return CLIENT_USERS.GetAll().Where(p => p.CLIENT_ID == clientId).ToList();
        }

        public CLIENT_USERS ClientUserRetrieve(int clientUserId)
        {
            return CLIENT_USERS.GetAll().FirstOrDefault(p => p.USER_ID == clientUserId);
        }
        #endregion

        #region LOGS CRUD
        [WebMethod]
        public void LogAdd(DateTime logDate, string description, int id = 0)
        {
            //todo implement LogAdd
            return;
        }
        #endregion

        #region QUERY_SEQUENCE_REQUEST CRUD
        #endregion
    }
}