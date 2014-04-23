using MobileHub.Functionality.Clients;
using MobileHub.Objects;
using MobileHub.Objects.ResultTypes;
using MobileHub.Objects.Synchronization;
using MobileHub.Repositories;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Web.Services;
using System.Xml.Serialization;

namespace MobileHub
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
            Functions.ClientRespond(Resp);
        }
        [WebMethod]
        public void ResponsePartial(ResponseComponent Component)
        {
            Responses.Instance.Add(Component.Id, Component);
            Logs.Instance.Add(Component.Resp);
        }

        [WebMethod]
        public ClientSynchroData SyncDataGet()
        {
            return Functions.SyncInfoGet();
        }

        public MethodResult CheckIn(int clientInstanceId)
        {
            throw new NotImplementedException();
        }
        public MethodResult IsConnected(int clientInstanceId)
        {
            throw new NotImplementedException();
        }
    }
}