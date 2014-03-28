using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using MobileHub.Repositories;
using MobileHub.Exceptions;
using MobileHub.Data.Tables;
using MobileHub.Objects;

namespace MobileHub.Functionality.API
{
    public class QryExecute : FunctionStrategyBase<QueryResult>
    {
        public QueryResult Strategy(int connectionId, string queryName, DateTime? time = null)
        {
            QueryResult r = new QueryResult();
            try
            {
                if (!Consumers.Instance.Exists(connectionId))
                    throw new InvalidConsumerException();
                var cust = Consumers.Instance.Retrieve(connectionId);

                var dbList = new List<CLIENT_DB_COMPANIES>(CLIENT_DB_COMPANIES.GetAll());
                var index = dbList.FindIndex((p) => p.DB_COMPANY_ID == cust.DatabaseId);
                if (index == -1) throw new Exception("Cannot find specified database");

                var Queries = new List<APP_QUERIES>(APP_QUERIES.GetAll());
                var QueryIndex = Queries.FindIndex((p) => p.NAME == queryName && p.APP_ID == Consumers.Instance.Retrieve(connectionId).AppId);
                if (QueryIndex == -1) throw new InvalidQueryException();

                Logs.Instance.Add(Queries[QueryIndex].QUERY_ID);
                Logs.Instance.Add(Queries[QueryIndex].NAME);

                r = fQuery(cust.ClientId, dbList[index].COMPANY_CODE, Queries[QueryIndex].QUERY_ID, time);
                r.Success(r.Response);
            }
            catch (Exception e)
            {
                r.Failure(e);
            }
            return r;
        }

        static QueryResult fQuery(int ClientId, string Code, int Qry, DateTime? Time = null)
        {
            List<object> args = new List<object>();
            args.Add(Code);
            args.Add(Qry);
            args.Add(Time);
            int i = new int();
            i = Commands.Instance.Add(ClientId, "ExecuteRegisteredQuery", args);
            Logs.Instance.Add(i);
            return Responses.Instance.GetResponse<QueryResult>(i);
        }
    }
}