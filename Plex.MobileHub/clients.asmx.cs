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

        [WebMethod]
        public void AppQueriesRetrieve() 
        {
            //todo implement AppQueriesRetrieve

        }
        #endregion

        
        #region APP_USER_TYPES CRUD
        [WebMethod]
        public void AppUserTypesRetrieve() 
        {
            //todo implement AppUserTypesRetrieve
        }
        #endregion


        #region CLIENTS CRUD
        public CLIENTS ClientsRetrieve(int id)
        {
            return CLIENTS.GetAll().First(p => p.CLIENT_ID == id);
        }
        #endregion

        #region CLIENT_APPS CRUD
        public CLIENT_APPS ClientsAppsRetrieve(int id)
        {
            return CLIENT_APPS.GetAll().First(p => p.CLIENT_APP_ID == id);
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
            
            var company = new CLIENT_DB_COMPANIES();
            company.COMPANY_CODE = companyCode;
            company.CLIENT_ID = clientId;
            company.DATABASE_SID = connectionString;
            using (var connection = Utilities.GetConnection(true)){
                company.DB_COMPANY_ID = Convert.ToInt32(connection.Query("select id_gen.nextval from dual")[0, 0]);
                company.Insert(connection);
            }
        }

        [WebMethod]
        public void ClientDbCompanyRemove(int id) 
        {
            CLIENT_DB_COMPANIES.GetAll().First(p => p.DB_COMPANY_ID == id).Delete();
        }
        #endregion

        #region CLIENT_DB_COMPANY_USERS CRUD
        [WebMethod]
        public void ClientDbCompanyUserAdd(int dbCompanyId, int UserId, string ConnectAs = null)
        {
            CLIENT_DB_COMPANY_USERS userPermission = new CLIENT_DB_COMPANY_USERS();
            userPermission.DB_COMPANY_ID = dbCompanyId;
            userPermission.USER_ID = UserId;
            userPermission.CONNECT_AS = ConnectAs; 
            
            using (var connection = Utilities.GetConnection(true))
            {
                userPermission.DB_COMPANY_USER_ID = Convert.ToInt32(connection.Query("select id_gen.nextval from dual ")[0, 0]);
                userPermission.Insert(connection);
            }
        }

        [WebMethod]
        public void ClientDbCompanyUserRemove(int id)
        {
            CLIENT_DB_COMPANY_USERS.GetAll().First(p => p.DB_COMPANY_USER_ID == id).Delete();
        }
        #endregion

        #region CLIENTS_DB_COMPANY_USER_APPS CRUD
        [WebMethod]
        public void ClientDbCompanyUserAppsAdd(int appId, int dbCompanyUserId, int? appUserTypeId = null)
        {
            CLIENT_DB_COMPANY_USER_APPS permission = new CLIENT_DB_COMPANY_USER_APPS();
            permission.APP_ID = appId;
            permission.APP_USER_TYPE_ID = appUserTypeId;
            permission.DB_COMPANY_USER_ID = dbCompanyUserId;
            
            using(var connection = Utilities.GetConnection(true))
            {
                permission.DB_COMPANY_USER_APP_ID = Convert.ToInt32(connection.Query("select id_gen.nextval from dual ")[0, 0]);
                permission.Insert(connection);
            }
        }
        [WebMethod]
        public void ClientDbCompanyUserAppsRemove(int id)
        {
            CLIENT_DB_COMPANY_USER_APPS.GetAll().First(p => p.DB_COMPANY_USER_APP_ID == id).Delete();
        }
        #endregion

        #region CLIENT_USERS CRUD
        [WebMethod]
        public void ClientUserAdd(int clientId, string name, string password)
        {

            CLIENT_USERS user =  new CLIENT_USERS();
            user.CLIENT_ID = clientId;
            user.PASSWORD = password;
            user.NAME = name;
            using (var connection = Utilities.GetConnection(true))
            {
                user.USER_ID = Convert.ToInt32(connection.Query("select id_gen.nextval from dual ")[0, 0]);
                user.Insert(connection);
            }
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

        #region QUERY_SEQUENCE_REQUEST CRUD
        #endregion
    }
}