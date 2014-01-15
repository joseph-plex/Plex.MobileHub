﻿using System;
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
        /***************************************************************************************************
        ********************************  Offical Plexxis Methods  *****************************************
        ****************************************************************************************************/
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
                Logs.GetInstance().Add(e);
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
        public bool QryCreate(int nAppId, string sAppAuthKey,  QueryDefinition Query)
        {
            return Functions.QryCreate(nAppId, sAppAuthKey, Query);
        }

        [WebMethod]
        public bool QryDelete(int nAppId, string sAppAuthKey, string sQueryName)
        {
            return Functions.QryDelete(nAppId, sAppAuthKey, sQueryName);
        }

        [WebMethod]
        public XmlDocument QryExecute(int nConnectionId, string QueryName, DateTime? Time)
        {
            return Functions.QryExecute(nConnectionId, QueryName, Time);
        }

        [WebMethod]
        public XmlDocument QueryExecuteXml(int nConnectionId, string QueryName)
        {
            return Functions.QryExecute(nConnectionId, QueryName, null);
        }

        [WebMethod]
        public QueryResult QueryExecuteObj(int nConnectionId, string QueryName)
        {
            return Responses.ToObj<QueryResult>(Functions.QryExecute(nConnectionId, QueryName, null));
        }

        [WebMethod]
        public MethodResult IUD(int nConnectionId, IUDData DBModData)
        {
            return Functions.IUD(nConnectionId, DBModData);
        }

        [WebMethod]
        public List<XmlDocument> DeviceSynchronization(int ConnectionId)
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

        //[WebMethod]
        //public int AppTableColumns(int TableId)
        //{
        //    var atc = Access.AppTableColumns.GetAll();
        //    var cols = atc.Count((p) => TableId == p.TableId);
        //    return atc.Count; 
        //}

        [WebMethod]
        public XmlDocument QueryDatabaseXml(int nConnectionId, String Query)
        {
            try
            {
                if (!Consumers.Instance.Exists(nConnectionId))
                    throw new Exception("The connection you are specifying does not exist");
                var cust = Consumers.Instance.Retrieve(nConnectionId);
                var dbList = CLIENT_DB_COMPANIES.GetAll().ToList();
                var index = dbList.FindIndex((p) => p.DB_COMPANY_ID == cust.DatabaseId);
                return fQueryx(cust.ClientId, dbList[index].COMPANY_CODE, Query);
            }
            catch (Exception e)
            {
                Logs.GetInstance().Add(e);
            }
            return null;
        }

        public XmlDocument fQueryx(int ClientId, string Code, string Qry)
        {
            List<object> args = new List<object>();
            args.Add(Code);
            args.Add(Qry);
            int i = Commands.Instance.Add(ClientId, "Query", args);
            Logs.GetInstance().Add(i);
            return Responses.Instance.GetResponse(i);
        }


        [WebMethod]
        public QueryResult QueryDatabaseObj(int nConnectionId, String Query)
        {
            try
            {
                if (!Consumers.Instance.Exists(nConnectionId))
                    throw new Exception("The connection you are specifying does not exist");
                var cust = Consumers.Instance.Retrieve(nConnectionId);
                var dbList = CLIENT_DB_COMPANIES.GetAll().ToList();
                var index = dbList.FindIndex((p) => p.DB_COMPANY_ID == cust.DatabaseId);
                return fQueryo(cust.ClientId, dbList[index].COMPANY_CODE, Query);
            }
            catch (Exception e)
            {
                Logs.GetInstance().Add(e);
            }
            return null;
        }

        public QueryResult fQueryo(int ClientId, string Code, string Qry)
        {
            List<object> args = new List<object>();
            args.Add(Code);
            args.Add(Qry);
            int i = Commands.Instance.Add(ClientId, "Query", args);
            Logs.GetInstance().Add(i);
            return Responses.Instance.GetResponse<QueryResult>(i);
        }
    }
}
