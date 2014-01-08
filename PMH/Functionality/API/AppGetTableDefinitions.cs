using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Plex.PMH.Objects;
using Plex.PMH.Exceptions;
using Plex.PMH.Data.Tables;
namespace Plex.PMH.Functionality.API
{
    public static partial class Functions
    {
        public static List<TableDefinition> AppGetTableDefinition(int ApplicationId, string ApplicationAuthenticationKey)
        {
            var Applications = new List<APPS>(APPS.GetAll());//.APPS.ToList();
            var CurrentAppIndex = Applications.FindIndex((p) => p.APP_ID == ApplicationId);

            if (CurrentAppIndex == -1) throw new InvalidApplicationException();

            //todo create proper exception for this
            if (Applications[CurrentAppIndex].AUTH_KEY != ApplicationAuthenticationKey) throw new Exception();

            //Get all the Queries for this application
            var ApplicationTables = new List<APP_TABLES>(APP_TABLES.GetAll()).FindAll((p) => p.APP_ID == ApplicationId);
            var ApplicationCols = new List<APP_TABLE_COLUMNS>(APP_TABLE_COLUMNS.GetAll());
            //Well they passed all the checks 
            List<TableDefinition> AppTableDefinition = new List<TableDefinition>();

            //Iterate through applicable tables and turn them into table definitions.
            foreach (var t in ApplicationTables)
            {
                TableDefinition d = new TableDefinition();
                d.AllowDelete = Convert.ToBoolean(t.DELETE_ALLOWED);
                d.AllowInsert = Convert.ToBoolean(t.INSERT_ALLOWED);
                d.AllowUpdate = Convert.ToBoolean(t.UPDATE_ALLOWED);
                d.AllowQuery = Convert.ToBoolean(t.INSERT_ALLOWED);

                d.TableName = t.NAME ;

                foreach (var c in ApplicationCols)
                    d.Columns.Add(new Column()
                    {
                        ColumnName = c.COLUMN_NAME,
                        ColumnSequence = c.COLUMN_SEQUENCE,
                        DataType = c.DATA_TYPE,
                        DataLength = c.DATA_LENGTH,
                        DataPrecision = c.DATA_PRECISION,
                        DataScale = c.DATA_SCALE,
                        AllowDbNull = Convert.ToBoolean(c.ALLOW_DB_NULL),
                        IsReadOnly = Convert.ToBoolean(c.IS_READ_ONLY),
                        IsLong = Convert.ToBoolean(c.IS_LONG),
                        IsKey = Convert.ToBoolean(c.IS_KEY),
                        KeyType = c.KEY_TYPE,
                        IsUnique = Convert.ToBoolean(c.IS_UNIQUE),
                        Description = c.DESCRIPTION
                    });
                AppTableDefinition.Add(d);
            }
            //return the goodies.
            return AppTableDefinition;

        }
    }
}