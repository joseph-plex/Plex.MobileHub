using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Plex.MobileHub.Repositories;
using Plex.MobileHub.Exceptions;
using Plex.MobileHub.Data.Tables;
using Plex.MobileHub.Objects;
namespace Plex.MobileHub.Functionality.API
{
    public class IUD : FunctionStrategyBase<MethodResult>
    {

        public MethodResult Strategy(int connectionId, IUDData[] Data)
        {

            if (!Consumers.Instance.Exists(connectionId))
                throw new InvalidConsumerException();
            var consumer = Consumers.Instance.Retrieve(connectionId);
            
            //Make sure the data bases basic tests.
            for (int i = 0; i < Data.Length; i++)
                if (!Data[i].IsValid())
                    throw new Exception("IUDData #" + i + " is invalid");

            //If user references a table name that they do not have access, then fail fast.
            var appTables = APP_TABLES.GetAll().Where(p => p.APP_ID == consumer.AppId);
            if (Data.Any(p => !appTables.Any(at => at.NAME == p.TableName)))
                throw new Exception("Unauthorized Table Reference in IUDData");

            //If user reference a table column name that they do not have access, fail fast.
            var appTableColumns = APP_TABLE_COLUMNS.GetAll().Where(p => appTables.Any(at => at.TABLE_ID == p.TABLE_ID));
            foreach(var iud in Data)
                foreach(var col in iud.ColumnNames)
                    if (!appTableColumns.Any(p => p.COLUMN_NAME == col))
                        throw new Exception("Unauthorized Table Column Reference in IUDData");

            return consumer.ClientGet().ExecuteRequest<MethodResult>("IUD", consumer.ClientDbCompaniesGet().COMPANY_CODE ,Data);
        }
    }
}