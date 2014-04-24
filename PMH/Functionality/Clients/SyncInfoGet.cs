
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using Plex.MobileHub.Objects.Synchronization;
using Plex.MobileHub.Data.Tables;

namespace Plex.MobileHub.Functionality.Clients
{
    public static partial class Functions
    {
        public static ClientSynchroData SyncInfoGet()
        {
            ClientSynchroData SyncData = new ClientSynchroData();
            foreach (var app in APPS.GetAll())
            {
                SyncData.Apps.Add(new ApplicationSynchroData()
                {
                    ApplicationDescription = app.DESCRIPTION,
                    ApplicationId = app.APP_ID,
                    ApplicationTitle = app.TITLE,
                    ApplicationQueries = ParseQueries(app.GetAPP_QUERIES()).ToList()
                });
            }
            return SyncData;
        }

        private static IEnumerable<QuerySynchroData> ParseQueries(IEnumerable<APP_QUERIES> AppQueries)
        {
            var AppTables = APP_TABLES.GetAll().ToList();
            foreach (var Query in AppQueries)
            {
                yield return new QuerySynchroData()
                {
                    TableName = AppTables.Find((p)=> p.TABLE_ID == Query.TABLE_ID).NAME,
                    Description = Query.DESCRIPTION,
                    QueryId = Query.QUERY_ID,
                    Cols = ParseColumns(Query.GetAPP_QUERY_COLUMNS()).ToList(),
                    Conds = ParseConditions(Query.GetAPP_QUERY_CONDITIONS()).ToList()
                };
            }
        }

        private static IEnumerable<QueryColumnSynchroData> ParseColumns(IEnumerable<APP_QUERY_COLUMNS> Cols)
        {
            foreach (var Col in Cols)
            {
                yield return new QueryColumnSynchroData()
                {
                    ColId = Col.COLUMN_ID,
                    ColName = Col.COLUMN_NAME,
                    ColSeq = Col.COLUMN_SEQUENCE
                };
            }
        }

        public static IEnumerable<QueryConditionSynchroData> ParseConditions(IEnumerable<APP_QUERY_CONDITIONS> conds)
        {
            foreach (var Condition in conds)
            {
                yield return new QueryConditionSynchroData()
                {
                    ColumnName = Condition.COLUMN_NAME,
                    ColumnNVL = Condition.COLUMN_NVL,
                    ColumnValue = Condition.COLUMN_VALUE,
                    Operation = Condition.OPERATOR
                };
            }
        }
    }
}