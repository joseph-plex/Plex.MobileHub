using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml;
using System.Threading;
using Oracle.DataAccess.Client;
using System.Data;
using System.Web.Configuration;
using System.Xml.Serialization;
using System.IO;


using Plex.PMH.Functionality.API;
using Plex.PMH.Repositories;
using Plex.PMH.Exceptions;
using Plex.PMH.Objects;
using Plex.PMH.Data.Tables;

namespace Plex.PMH
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
            MethodResult mr = new MethodResult();
            try
            {
                mr.Success(Functions.ConnectionConnect(ClientId, AppId, Database, User, Password));
            }
            catch (Exception e)
            {
                mr.Failure(e.Message);
                Logs.Instance.Add(e);
            }
            return mr;
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

        [WebMethod]
        public List<QueryResult> DeviceSynchronization(int ConnectionId)
        {
            return Functions.DeviceSynchronization(ConnectionId, 0, null);
        }
        ///****************************************************************************************************
        // ********************************* Internal Plexxis Methods *****************************************
        // ****************************************************************************************************/
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
    }
}
