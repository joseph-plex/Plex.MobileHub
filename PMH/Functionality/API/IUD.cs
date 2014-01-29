using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using Plex.PMH.Objects;
using Plex.PMH.Repositories;
using Plex.PMH.Exceptions;

using Plex.PMH.Data.Tables;
namespace Plex.PMH.Functionality.API
{
    public static partial class Functions
    {
        public static MethodResult IUD(int ConnectionId, IUDData ChangeRequest)
        {

            if (!Consumers.Instance.Exists(ConnectionId)) throw new InvalidConsumerException();

            IUDVerification(ChangeRequest);
            IUDValidation(ChangeRequest);

            //Get the Consumer information to retrieve Application table
            var Consumer = Consumers.Instance.Retrieve(ConnectionId);
            var ApplicationTables = new List<APP_TABLES>(APP_TABLES.GetAll());

            //Make sure sure the table is actually a legitimate table
            int TableIndex = ApplicationTables.FindIndex((p) => p.APP_ID == Consumer.AppId && p.NAME == ChangeRequest.TableName);
            if (TableIndex == -1) throw new ArgumentException("Invalid Table Name");

            //Check to make sure all columns are valid based on column names 
            int ErrIndex = new int();
            var ApplicationColumns = new List<APP_TABLE_COLUMNS>(APP_TABLE_COLUMNS.GetAll()).FindAll((p) => p.TABLE_ID == ApplicationTables[TableIndex].TABLE_ID);
            if ((ErrIndex = ChangeRequest.ColumnNames.FindIndex((p) => !ApplicationColumns.Exists((p2) => p2.COLUMN_NAME == p))) != -1)
                throw new ArgumentException("Invalid Column Name : " + ChangeRequest.ColumnNames[ErrIndex]);


            if (ApplicationTables[TableIndex].DELETE_ALLOWED == 0)
                if ((ErrIndex = ChangeRequest.Rows.FindIndex((p) => p.DBAction == 1)) != -1)
                    throw new ArgumentException("Invalid permissions to delete from record index : " + ErrIndex);
            if (ApplicationTables[TableIndex].INSERT_ALLOWED == 0)
                if ((ErrIndex = ChangeRequest.Rows.FindIndex((p) => p.DBAction == 1)) != -1)
                    throw new ArgumentException("Invalid permissions to Insert from record index : " + ErrIndex);
            if (ApplicationTables[TableIndex].UPDATE_ALLOWED == 0)
                if ((ErrIndex = ChangeRequest.Rows.FindIndex((p) => p.DBAction == 2)) != -1)
                    throw new ArgumentException("Invalid permissions to index from record index : " + ErrIndex);
            if (ApplicationTables[TableIndex].QUERY_ALLOWED == 0)
                if ((ErrIndex = ChangeRequest.Rows.FindIndex((p) => p.DBAction == 0)) != -1)
                    throw new ArgumentException("Invalid permissions to Read from record index : " + ErrIndex);


            var DBCode = new List<CLIENT_DB_COMPANIES>(CLIENT_DB_COMPANIES.GetAll()).Find((p) => p.DB_COMPANY_ID == Consumer.DatabaseId).COMPANY_CODE;
            //Create Command, Register it and wait for response
            var Comm = CommandFactory.CreateIUDCommand(Consumer.ClientId, DBCode, ChangeRequest);
            Comm.RequestId = Commands.GetKey();
            return Responses.Instance.GetResponse<MethodResult>(Commands.Instance.Add(Comm));

            //todo check to ensure primary key is including in all columns. Type verification might be decent too.
        }

        public static void IUDVerification(IUDData ChangeRequest)
        {
            if (ChangeRequest == null)
                throw new ArgumentNullException("ChangeRequest");
            if (ChangeRequest.Rows == null)
                throw new ArgumentNullException("ChangeRequest.Rows");
            if (ChangeRequest.TableName == null)
                throw new ArgumentNullException("ChangeRequest.TableName");
        }

        public static void IUDValidation(IUDData ChangeRequest)
        {
            if (ChangeRequest.TableName == string.Empty)
                throw new ArgumentException("A table name must have a value");
            if (ChangeRequest.ColumnNames.Count == 0)
                throw new ArgumentException("Must have at least 1 column name rows");
            if (ChangeRequest.Rows.Count == 0)
                throw new ArgumentException("Must have at least 1 row");

            var HasIUD = false;
            foreach (var row in ChangeRequest.Rows)
                if (row.Values.Count != ChangeRequest.ColumnNames.Count)
                    throw new ArgumentException("All Row Values should have the same as values as there are column Names");
                else if (row.DBAction != 0)
                    HasIUD = true;
            if (!HasIUD)
                throw new ArgumentException("At least one operation must be Insert, Update or Delete");
        }

    }
}