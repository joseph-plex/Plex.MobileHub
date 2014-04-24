using Plex.MobileHub.Functionality.API;
using Plex.MobileHub.Objects;
using Plex.MobileHub.Objects.ResultTypes;
using Plex.MobileHub.Repositories;
using System;
using System.Collections.Generic;
using System.Web.Services;
using System.Xml;
namespace Plex.MobileHub
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
        public MethodResult ConnectionRelease(int ConnectionId)
        {
            return new ConnectionRelease().Strategy(ConnectionId);
        }

        [WebMethod]
        public int ConnectionStatus(int nConnectionId)
        {
            throw new NotImplementedException();
        }

        [WebMethod]
        public RQryResult QryExecute(int nConnectionId, string QueryName)
        {
            return new QryExecute().Strategy(nConnectionId, QueryName);
        }

        [WebMethod]
        public XmlDocument QryExecuteXml(int nConnectionId, string QueryName)
        {
            return new QryExecuteXml().Strategy(nConnectionId, QueryName);
        }
        [WebMethod]
        public MethodResult IUD(int nConnectionId, IUDData DBModData)
        {
            throw new NotImplementedException();
        }
        
        /*********************************** Internal Plexxis Methods *****************************************/
        
        [WebMethod]
        public void Sync()
        {
            Commands.Instance.SyncAllClientDatabases();
        }

        [WebMethod]
        public QryResult QueryDatabase(int ConnectionId, String Query)
        {
            return new QueryDatabase().Strategy(ConnectionId, Query);
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
