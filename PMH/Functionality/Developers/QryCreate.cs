using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Plex.PMH.Exceptions;
using Plex.PMH.Objects;
using Plex.PMH.Data;
using Plex.PMH.Data.Tables;

namespace Plex.PMH.Functionality.Developers
{
    public static partial class Functions
    {
        public static bool QryCreate(int ApplicationId, string ApplicationAuthenticationKey, QueryDefinition QryDef)
        {
            //Check The Arguments, failure to do this correctly will result in bad things
            if (QryDef == null) throw new ArgumentNullException("QryDef", "Query Definition cannot be null");
            if (QryDef.QueryName == string.Empty || QryDef.QueryName == null) throw new ArgumentNullException("QryDef.QueryName", "Queries must have names");
            if (QryDef.ColumnNames == null || QryDef.ColumnNames.Count == 0) throw new ArgumentNullException("QryDef.ColumnNames", "Queries must have at least 1 column");

            var Applications = new List<APPS>(APPS.GetAll());
            var ApplicationIndex = Applications.FindIndex((p) => p.APP_ID == ApplicationId);

            if (ApplicationIndex == -1)
                throw new InvalidApplicationException();

            if (Applications[ApplicationIndex].AUTH_KEY != ApplicationAuthenticationKey)
                throw new InvalidApplicationKeyException();


           // var QueryIndex = Access.AppQueries.GetAll().FindIndex((p) => Applications[ApplicationIndex].AppId == p.AppId && p.Name == QryDef.QueryName);
            //if (QueryIndex != -1) throw new Exception("A query of this name already exists for this application");
            if (new List<APP_QUERIES>(APP_QUERIES.GetAll()).Exists((p) => Applications[ApplicationIndex].APP_ID == p.APP_ID && p.NAME == QryDef.QueryName))
                throw new Exception("A query of this name already exists for this application");

            var Columns = new List<APP_QUERY_COLUMNS>();
            var Conditions = new List<APP_QUERY_CONDITIONS>();
            //List<Access.AppQueryConditions> Conditions = new List<Access.AppQueryConditions>();
            if (QryDef.WhereClauses != null)
                foreach (var Clause in QryDef.WhereClauses)
                    Conditions.Add(new APP_QUERY_CONDITIONS()
                    {
                        OPERATOR = Clause.Operator,
                        COLUMN_NAME = Clause.ColumnName,
                        COLUMN_VALUE = Clause.ColumnValue,
                        COLUMN_NVL = Clause.ColumnNVL
                    });

            for (int i = 0; i < QryDef.ColumnNames.Count; i++)
            {
                Columns.Add(new APP_QUERY_COLUMNS()
                {
                    COLUMN_NAME = QryDef.ColumnNames[i],
                    COLUMN_SEQUENCE = i + 1,
                });
            }

            var Tables = new List<APP_TABLES>(APP_TABLES.GetAll());
            var TableIndex = Tables.FindIndex((p) => p.APP_ID == ApplicationId && QryDef.TableName == p.NAME);
            if (TableIndex == -1) //todo make a real exception 
                throw new Exception("This application does not have access to a table with this name");
            //Get the relevant database objects
            using (var Conn = Utilities.GetConnection(true))
            {
                var Transaction = Conn.BeginTransaction();

                //Set the query object
                try
                {
                    var Query = new APP_QUERIES();

                    Query.QUERY_ID = Convert.ToInt32(Conn.Query("select ID_GEN.NEXTVAL from dual")[0, 0]);
                    Query.APP_ID = ApplicationId;
                    Query.NAME = QryDef.QueryName;

                    Query.SQL = QryDef.Sql;

                    Query.DESCRIPTION = QryDef.Description;
                    Query.TABLE_ID = Tables[TableIndex].TABLE_ID;
                    Query.IS_DELTA = Convert.ToInt32(QryDef.TrackDelta);

                    //todo Move this out of here
                    Query.SEQ_LIMIT_TIMESPAN = new int();
                    Query.SEQ_LIMIT = new int();

                    //Insert Query into database
                    Query.Insert(Conn);
                    foreach (var c in Conditions)
                    {
                        c.CONDITION_ID = Convert.ToInt32(Conn.Query("select ID_GEN.NEXTVAL from dual")[0, 0]);
                        c.QUERY_ID = Query.QUERY_ID;
                        c.Insert(Conn);
                    }
                    foreach (var c in Columns)
                    {
                        c.COLUMN_ID = Convert.ToInt32(Conn.Query("select ID_GEN.NEXTVAL from dual")[0, 0]);
                        c.QUERY_ID = Query.QUERY_ID;
                        c.Insert(Conn);
                    }
                }
                catch
                {
                    Transaction.Rollback(); ;
                    throw;
                }
                Transaction.Commit();
            }
            return true;
        }
    }
}