﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Threading;
using System.Xml.Serialization;
using System.Xml;
using System.IO;
using Plex.PMH.Repositories;
using Plex.PMH.Objects;
using Plex.PMH.Objects.Synchronization;
using Plex.PMH.Functionality.Clients;
using System.Diagnostics;
namespace Plex.PMH
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
        public MethodResult Login(int ClientId, string Key)
        {
            MethodResult mr = new MethodResult();
            try
            {
                Connections.Instance.Add(new ConnectionData()
                {
                    ClientId = ClientId,
                    Key = Key,
                    InitTime = DateTime.Now,
                    LastCheck = Stopwatch.StartNew()
                });
                mr.Success();
            }
            catch (Exception e)
            {
                mr.Failure(e);
            }
            return mr;
        }

        [WebMethod]
        public MethodResult Logout(int ConnectionId)
        {
            MethodResult mr = new MethodResult();
            Connections.Instance.Remove(ConnectionId);
            return mr.Success();
        }

        [WebMethod]
        public GetCommandsMethodResult GetCommands(int ConnectionId)
        {
            GetCommandsMethodResult mr = new GetCommandsMethodResult();
            try
            {
                if (!Connections.Instance.ConnectionExists(ConnectionId))
                    throw new Exception("Invalid Connection Id");

                Connections.Instance.CheckIn(ConnectionId);

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
            MethodResult mr = new MethodResult();
            Logs.GetInstance().Add(Resp.Resp);
            Responses.Instance.Add(Resp.Id,Resp);
            return mr.Success();
        }
        [WebMethod]
        public MethodResult ResponsePartial(ResponseComponent Component)
        {
            MethodResult mr = new MethodResult();
            Responses.Instance.Add(Component.Id, Component);
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