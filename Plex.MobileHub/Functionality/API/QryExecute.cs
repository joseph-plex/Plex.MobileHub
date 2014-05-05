﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;
using Plex.MobileHub.Repositories;
using Plex.MobileHub.Exceptions;
using Plex.MobileHub.Data.Tables;
using Plex.MobileHub.Objects;
using Plex.Diagnostics;
namespace Plex.MobileHub.Functionality.API
{
    public class QryExecute : FunctionStrategyBase<RQryResult>
    {
        public RQryResult Strategy(int connectionId, string queryName, DateTime? time = null)
        {
            RQryResult r = new RQryResult();
            try
            {
                if (!Consumers.Instance.Exists(connectionId)) 
                    throw new InvalidConsumerException();
                var cust = Consumers.Instance.Retrieve(connectionId);

                var query = cust.GetAccessibleQueries().FirstOrDefault(p => p.APP_ID == cust.AppId && p.NAME == queryName);
                if (query == null)
                    throw new InvalidQueryException();

                Logs.Instance.Add("Executing Query" + query.NAME + " : Id - " + query.QUERY_ID);

                var dbCode = cust.ClientDbCompaniesGet().COMPANY_CODE;
                var client = Connections.Instance.Retrieve(cust.ClientId);
                TimeAnalyst.StartTask(query.QUERY_ID.ToString());
                r = client.ExecuteRequest<RQryResult>("ExecuteRegisteredQuery", dbCode, query.QUERY_ID, time);
                TimeAnalyst.StopTask(query.QUERY_ID.ToString());
                
                if(r.Response > 0)
                    r.Success(r.Response);

                if (query.TABLE_NAME == null || query.TABLE_NAME == string.Empty)
                    r.TableName = APP_TABLES.GetAll().First(p => p.TABLE_ID == query.TABLE_ID).NAME;
                else
                    r.TableName = query.TABLE_NAME;

                r.QueryName = query.NAME;
            }
            catch (Exception e)
            {
                r.Failure(e);
            }
            return r;
        }
    }
}