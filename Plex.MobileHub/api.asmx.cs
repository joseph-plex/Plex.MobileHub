using Plex.MobileHub.Functionality.API;
using Plex.MobileHub.Objects;
using Plex.MobileHub.Objects.ResultTypes;
using Plex.MobileHub.Repositories;
using System;
using System.Collections.Generic;
using System.Web.Services;
using System.Web;
using System.ServiceModel;
using System.Xml;
using System.Xml.Serialization;
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
        public MethodResult ConnectionStatus(int ConnectionId)
        {
            MethodResult mr = new MethodResult();
            try
            {
                //todo This needs to implement the following
                /*
                    CONSTANT CONN_STATUS_OK = 1
                    CONSTANT CONN_STATUS_PMH_ONLY = 2
                    CONSTANT CONN_STATUS_INVALID_ID = 3
                    CONSTANT CONN_STATUS_CLIENT_DB_OFFLINE = 4 //not implemented
                 **/

                if(!Consumers.Instance.Exists(ConnectionId))
                    return mr.Success(3,"The connection Id Specified is invalid");

                var Cons = Consumers.Instance.Retrieve(ConnectionId);
                var Conn = Connections.Instance.Retrieve(Cons.ClientId);

                Conn.RequestPull();
                return mr.Success(1, "The Connection is Okay");

            }
            catch(EndpointNotFoundException)
            {
                return mr.Success(2, "The Client is not online");
            }
            catch(TimeoutException)
            {
                return mr.Success(2, "The Client is not online");
            }
        }

        [WebMethod]
        public RQryResult QryExecute(int ConnectionId, string QueryName)
        {
            return new QryExecute().Strategy(ConnectionId, QueryName);
        }

        [WebMethod]
        public XmlDocument QryExecuteXml(int ConnectionId, string QueryName)
        {
            return new QryExecuteXml().Strategy(ConnectionId, QueryName);
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

        //[WebMethod]
        //public DeviceSynchronizeMethodResult DeviceSynchronize(int ConnectionId, int UserDataId )
        //{
        //    return new DeviceSynchronize().Strategy(ConnectionId, UserDataId);
        //}

        [WebMethod]
        public DeviceSynchronizeMethodResult DeviceSynchronize(int ConnectionId, int VersionId)
        {
            return new DeviceSynchronize().AlternateStrategy(ConnectionId, VersionId);
        }

        [WebMethod]
        public MethodResult DeviceRequestId(int connectionId)
        {
            return new DeviceRequestId().Strategy(connectionId);
        }
        [WebMethod]
        public object test()
        {
            return new object();
        }
    }
}
