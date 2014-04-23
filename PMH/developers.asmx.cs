using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

using MobileHub.Functionality.Developers;
using MobileHub.Repositories;
using MobileHub.Exceptions;
using MobileHub.Objects;
using MobileHub.Data.Tables;
using MobileHub.Objects.Synchronization;

namespace MobileHub
{
    /// <summary>
    /// Summary description for Developers
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Developers : WebService
    {
        [WebMethod]
        public List<TableDefinition> AppGetTableDefinition(int nAppId, string sAppAuthKey)
        {
            return Functions.AppGetTableDefinition(nAppId, sAppAuthKey);
        }

        [WebMethod]
        public List<QueryDefinition> AppGetQueries(int AppId, string AppAuthKey)
        {
            return Functions.AppGetQueries(AppId, AppAuthKey);
        }

        [WebMethod]
        public void ClientGetDatabaseList(int nClientId, int nAppId)
        {
            throw new NotImplementedException();
        }

        [WebMethod]
        public bool QryCreateOffTable(int AppId, string Auth, int TableId, string QueryName)
        {
            var tables = APP_TABLES.GetAll().ToList();
            List<string> ColNames = new List<string>();
            var TableIndex = tables.FindIndex((p) => p.TABLE_ID == TableId && AppId == p.APP_ID);
            if (tables.Count == 0) throw new Exception("No table with the specified TableId/AppId was found");

            var Cols = APP_TABLE_COLUMNS.GetAll().ToList();
            Cols = Cols.FindAll((p) => p.TABLE_ID == tables[TableIndex].TABLE_ID);

            foreach (var col in Cols) ColNames.Add(col.COLUMN_NAME);
            QueryDefinition def = new QueryDefinition();
            def.TableName = tables[TableIndex].NAME;
            def.Sql = "test";
            def.TrackDelta = true;
            def.WhereClauses = null;
            def.ColumnNames = ColNames;
            def.QueryName = QueryName;

            return Functions.QryCreate(AppId, Auth, def);
        }


        [WebMethod]
        public bool QryCreate(int nAppId, string sAppAuthKey, QueryDefinition Query)
        {
            return Functions.QryCreate(nAppId, sAppAuthKey, Query);
        }

        [WebMethod]
        public bool QryDelete(int nAppId, string sAppAuthKey, string sQueryName)
        {
            return Functions.QryDelete(nAppId, sAppAuthKey, sQueryName);
        }

        [WebMethod]
        public DevelSyncData GetAppSyncData(int AppId, string AppKey)
        {
            return DevelSyncData.GetDataForApp(AppId, AppKey);
        }
    }
}
