using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

using Plex.MobileHub.Functionality.Developers;
using Plex.MobileHub.Repositories;
using Plex.MobileHub.Exceptions;
using Plex.MobileHub.Objects;
using Plex.MobileHub.Data.Tables;
using Plex.MobileHub.Objects.Synchronization;

namespace Plex.MobileHub
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
        public List<TableDefinition> AppGetTableDefinition(int AppId, string AppAuthKey)
        {
            return Functions.AppGetTableDefinition(AppId, AppAuthKey);
        }

        [WebMethod]
        public List<QueryDefinition> AppGetQueries(int AppId, string AppAuthKey)
        {
            return Functions.AppGetQueries(AppId, AppAuthKey);
        }

        [WebMethod]
        public void ClientGetDatabaseList(int ClientId, int nAppId)
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
        public bool QryCreate(int AppId, string AppAuthKey, QueryDefinition Query)
        {
            return Functions.QryCreate(AppId, AppAuthKey, Query);
        }

        [WebMethod]
        public bool QryDelete(int AppId, string AppAuthKey, string QueryName)
        {
            return Functions.QryDelete(AppId, AppAuthKey, QueryName);
        }

        [WebMethod]
        public DevelSyncData GetAppSyncData(int AppId, string AppKey)
        {
            return DevelSyncData.GetDataForApp(AppId, AppKey);
        }
    }
}
