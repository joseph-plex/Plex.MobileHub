using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Plex.PMH.Exceptions;
using Plex.PMH.Objects;
using Plex.PMH.Data.Tables;

namespace Plex.PMH.Functionality.API
{
    public static partial class Functions
    {
        //todo Make this return a method result.
        public static List<QueryDefinition> AppGetQueries(int ApplicationId, string ApplicationAuthenticationKey)
        {
            var Applications = new List<APPS>(APPS.GetAll());
            var CurrentAppIndex = Applications.FindIndex((p) => p.APP_ID == ApplicationId);
            if (CurrentAppIndex == -1) throw new InvalidApplicationException();
            var App = Applications[CurrentAppIndex];
         
            //todo create proper exception for this
            if (App.AUTH_KEY != ApplicationAuthenticationKey) throw new Exception();
            

            //Get all Application Queries
            var ApplicationQueries = new List<APP_QUERIES>(App.GetAPP_QUERIES());
            var QueryConditions = new List<APP_QUERY_CONDITIONS>(APP_QUERY_CONDITIONS.GetAll());
            var ApplicationTables = new List<APP_TABLES>(APP_TABLES.GetAll());
            var QueryColumns = new List<APP_QUERY_COLUMNS>(APP_QUERY_COLUMNS.GetAll());

            List<QueryDefinition> definitions = new List<QueryDefinition>();
            foreach (var ApplicationQuery in ApplicationQueries)
            {
                QueryDefinition definition = new QueryDefinition();

                //What is the Query Name?
                definition.QueryName = ApplicationQuery.NAME;

                //What is the Query TableName?
                var ApplicationTableIndex = ApplicationTables.FindIndex((p) => p.TABLE_ID == ApplicationQuery.TABLE_ID);
                if (ApplicationTableIndex == -1) throw new Exception();
                definition.TableName = ApplicationTables[ApplicationTableIndex].NAME;

                //Is the Query a delta?
                definition.TrackDelta = Convert.ToBoolean(ApplicationQuery.IS_DELTA);

                //What is the actual SQL
                definition.Sql = ApplicationQuery.SQL;

                //Is What are the conditions (if any)
                foreach (var Condition in QueryConditions.FindAll((p) => p.QUERY_ID == ApplicationQuery.QUERY_ID))
                    definition.WhereClauses.Add(new Condition(){
                        ColumnName = Condition.COLUMN_NAME,
                        ColumnNVL = Condition.COLUMN_NVL,
                        ColumnValue = Condition.COLUMN_VALUE,
                        Operator = Condition.OPERATOR
                    });

                //What are the query columns (if any, any there should be some)
                foreach (var Column in QueryColumns.FindAll((p) => p.QUERY_ID == ApplicationQuery.QUERY_ID))
                    definition.ColumnNames.Add(Column.COLUMN_NAME);
                definitions.Add(definition);
            }
            return definitions;
        }
    }
}