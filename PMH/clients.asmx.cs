using MobileHub.Functionality.Clients;
using MobileHub.Objects;
using MobileHub.Objects.ResultTypes;
using MobileHub.Objects.Synchronization;
using MobileHub.Repositories;
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
        [XmlInclude(typeof(QueryResult))]

        [WebMethod]
        public MethodResult Login(int ClientId, string Key, string IPv4, int Port)
        {
            return Functions.ClientLogin(ClientId, Key, IPv4, Port);
        }

        [WebMethod]
        public MethodResult Logout(int ConnectionId)
        {
            return Functions.ClientLogout(ConnectionId);
        }

        [WebMethod]
        public GetCommandsMethodResult GetCommands(int ConnectionId)
        {
            GetCommandsMethodResult mr = new GetCommandsMethodResult();
            try
            {
                if (!Connections.Instance.ConnectionExists(ConnectionId))
                    throw new Exception("Invalid Connection Id");

                var ConnectionDetails = Connections.Instance.Retrieve(ConnectionId);
                var output = Commands.Instance.CommandRepo.Values.ToList().FindAll((p) => p.ClientId == ConnectionDetails.ClientId);
                
                output.ForEach((p) => Commands.Instance.Remove(p.RequestId));
                mr.Commands = output;
                mr.Success();
            }
            catch (Exception e)
            {
                mr.Failure(e);
            }
            return mr;
        }
        
        [WebMethod]
        public MethodResult Respond(Response Resp)
        {
            Logs.Instance.Add(Resp.Resp);
            return Functions.ClientRespond(Resp);
        }
        [WebMethod]
        public MethodResult ResponsePartial(ResponseComponent Component)
        {
            MethodResult mr = new MethodResult();
            Responses.Instance.Add(Component.Id, Component);
            Logs.Instance.Add(Component.Resp);
            return mr.Success();
        }

        
        [WebMethod]
        public ClientSynchroData SyncDataGet()
        {
            return Functions.SyncInfoGet();
        }

        public void ClientUserPermissionCreate() { }
        public void ClientUserPermissionDelete() { }
        public void ClientUserPermissionRetrieve() { }

        public void AuthenticUserAppPermissionCreate() { }
        public void AuthenticUserAppPermissionDelete() { }
        public void AuthenticUserAppPermissionRetrieve() { }

    }

}