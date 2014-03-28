using MobileHub.Functionality.API;
using MobileHub.Objects;
using MobileHub.Objects.ResultTypes;
using MobileHub.Repositories;
using System;
using System.Collections.Generic;
using System.Web.Services;
using System.Xml;
namespace MobileHub
{
    /// <summary>
    /// Summary description for api
    /// </summary>
    [WebService(Namespace = "http://pmh.plexxis.com")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class API : WebService
    {
        [WebMethod]
        public MethodResult ConnectionConnect(int ClientId, int AppId, String Database, String User, String Password)
        {
            return new ConnectionConnect().Strategy(ClientId, AppId, Database, User, Password);
        }

        [WebMethod]
        public void ConnectionRelease(int nConnectionId)
        {
            Functions.ConnectionRelease(nConnectionId);
        }

        [WebMethod]
        public int ConnectionStatus(int nConnectionId)
        {
            return (int)Functions.ConnectionGetStatus(nConnectionId);
        }

        [WebMethod]
        public QueryResult QryExecute(int nConnectionId, string QueryName)
        {
            return Functions.QryExecute(nConnectionId, QueryName, null);
        }

        [WebMethod]
        public XmlDocument QryExecuteXml(int nConnectionId, string QueryName)
        {
            return Functions.QryExecute(nConnectionId, QueryName, null);
        }
        [WebMethod]
        public MethodResult IUD(int nConnectionId, IUDData DBModData)
        {
            return Functions.IUD(nConnectionId, DBModData);
        }
        
        /*********************************** Internal Plexxis Methods *****************************************/
        
        [WebMethod]
        public void Sync()
        {
            Commands.Instance.SyncAllClientDatabases();
        }

        [WebMethod]
        public QueryResult QueryDatabase(int ConnectionId, String Query)
        {
            return Functions.QueryDatabase(ConnectionId, Query);
        }

        [WebMethod]
        public DeviceSynchronizeMethodResult DeviceSynchronize(int connectionId, int deviceId, int userDataId )
        {
            return new DeviceSynchronize().Strategy(connectionId, deviceId, userDataId);
        }

        [WebMethod]
        public MethodResult DeviceRequestId(int connectionId)
        {
            return new DeviceRequestId().Strategy(connectionId);
        }
    }
}
