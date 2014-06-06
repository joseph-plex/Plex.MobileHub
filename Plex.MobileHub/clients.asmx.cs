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
        public bool AddClientDbCompany(int clientId, string companyCode,string ConnectionString)
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
                    DB_COMPANY_ID = Convert.ToInt32(Conn.Query("select id_gen.nextval from dual")[0,0])
                };
            }
            return true;
        }

        [WebMethod]
        public ClientSynchroData SyncDataGet()
        {
            return Functions.SyncInfoGet();
        }

        [WebMethod]
        public void ClientDbCompanyAdd(int clientId, string companyCode, string connectionString )
        {
            return;
        }

        [WebMethod]
        public void ClientDbCompanyUserAdd(int appId, int dcCompanyUserId, int appUserTypeId)
        {
            return;
        }

        [WebMethod]
        public void ClientDbCompanyUserAppsAdd()
        {
            return;
        }

        [WebMethod]
        public void ClientUserAdd(int clientId, string name, string password)
        {
            return;
        }

        [WebMethod]
        public void LogAdd(DateTime logDate, string description, int id = 0)
        {
            return;
        }

        [WebMethod]
        public void ClientDbCompanyRemove() { }

        [WebMethod]
        public void ClientDbCompanyUserRemove()
        {
            return;
        }

        [WebMethod]
        public void ClientDbCompanyUserAppsRemove()
        {
            return;
        }

        [WebMethod]
        public void ClientUserRemove(int clientUserId)
        {
            RemoveClientUser strategy = new RemoveClientUser();
            strategy.Strategy(clientUserId);
            return;
        }
    }
}