using System;
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
        [XmlInclude(typeof(QueryResult))]
        [XmlInclude(typeof(IUDData))]

        [WebMethod]
        public MethodResult Login(int ClientId, string Key)
        {
            MethodResult mr = new MethodResult();
            return mr.Success(Connections.Instance.Add(new ConnectionData(){
                ClientId = ClientId,
                Key = Key,
                InitTime = DateTime.Now,
                LastCheck = Stopwatch.StartNew()
            })); 
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

        //[WebMethod]
        //public List<Access.Apps> ClientGetApps()
        //{
        //    return Access.Apps.GetAll();
        //}

        //[WebMethod]
        //public List<Access.AppQueries> ClientGetAppQueries()
        //{
        //    return Access.AppQueries.GetAll();
        //}

        //[WebMethod]
        //public List<Access.AppQueryColumns> ClientGetAppQueryColumns()
        //{
        //    return Access.AppQueryColumns.GetAll();
        //}

        //[WebMethod]
        //public List<Access.AppQueryConditions> ClientGetAppCondition()
        //{
        //    return Access.AppQueryConditions.GetAll();
        //}

        //[WebMethod]
        //public List<Access.AppTables> ClientGetAppTables()
        //{
        //    return Access.AppTables.GetAll();
        //}
        
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

        //[WebMethod]
        //public List<AppSyncData> ClientGetSyncInformation()
        //{
        //    return new List<AppSyncData>(AppSyncData.RetrieveCompleteSyncData());
        //}

        public Manager GetManager()
        {
            return (Manager)Application["Manager"];
        }
        
        [WebMethod]
        public ClientSynchroData SyncDataGet()
        {
            return Functions.SyncInfoGet();
        }


        //public void ClientUserCreate(Access.ClientUsers Client) 
        //{
        //   Client.Insert();
        //}
        //public void ClientUserModifiy(Access.ClientUsers Client) 
        //{
        //    Client.Update();
        //}
        //public Access.ClientUsers ClientUserRetrieve() 
        //{
        //    return null;
        //}
        //public void ClientUserDelete() 
        //{
        //}
        //public List<Access.ClientUsers> ClientUserGetAll() 
        //{
        //    return Access.ClientUsers.GetAll();
        //}

        public void ClientUserPermissionCreate() { }
        public void ClientUserPermissionDelete() { }
        public void ClientUserPermissionRetrieve() { }

        public void AuthenticUserAppPermissionCreate() { }
        public void AuthenticUserAppPermissionDelete() { }
        public void AuthenticUserAppPermissionRetrieve() { }

    //    public class AppSyncData
    //    {
    //        public static IEnumerable<AppSyncData> RetrieveCompleteSyncData()
    //        {
    //            var Apps = Access.Apps.GetAll();
    //            var AppQueries = Access.AppQueries.GetAll();
    //            var AppQueryColumns = Access.AppQueryColumns.GetAll();
    //            var AppQueryConditions = Access.AppQueryConditions.GetAll();
    //            foreach (var app in Apps)
    //            {
    //                AppSyncData current = new AppSyncData()
    //                {
    //                    App = app,
    //                };
    //                foreach (var Query in AppQueries.FindAll((p) => p.AppId == app.AppId))
    //                    current.AppQueries.Add(new QuerySyncData()
    //                    {
    //                        Query = Query,
    //                        AppQueryConditions = AppQueryConditions.FindAll((p) => p.QueryId == Query.QueryId),
    //                        AppQueryColumns = AppQueryColumns.FindAll((p) => p.QueryId == Query.QueryId)
    //                    });
    //                yield return current;
    //            }
    //        }
    //        public Access.Apps App = new Access.Apps();
    //        public List<QuerySyncData> AppQueries = new List<QuerySyncData>();
    //    }
    //    public class QuerySyncData
    //    {
    //        public Access.AppQueries Query = new Access.AppQueries();
    //        public List<Access.AppQueryColumns> AppQueryColumns = new List<Access.AppQueryColumns>();
    //        public List<Access.AppQueryConditions> AppQueryConditions = new List<Access.AppQueryConditions>();
    //    }
    }

}