using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

using MobileHub.Repositories;
using MobileHub.Exceptions;
using MobileHub.Data.Tables;
using MobileHub.Objects;

namespace MobileHub.Functionality.API
{
    public static partial class Functions
    {
        public static XmlDocument QryExecuteXml(int ConnectionId, string QueryName, DateTime? Time = null)
        {
            QueryResult r = new QueryResult();
            try
            {
                if (!Consumers.Instance.Exists(ConnectionId)) throw new InvalidConsumerException();
                var cust = Consumers.Instance.Retrieve(ConnectionId);

                var query = cust.GetAccessibleQueries().First(p => p.APP_ID == cust.AppId);
                if (query == null) throw new InvalidQueryException();

                Logs.Instance.Add(query.NAME + " : " + query.QUERY_ID);

                var dbCode = cust.ClientDbCompaniesGet().COMPANY_CODE;
                var client = Connections.Instance.Retrieve(cust.ClientId);

                r = client.ExecuteRequest<QueryResult>("ExecuteRegisteredQuery", dbCode, query.QUERY_ID, Time);
                r.Success();
            }
            catch (Exception e)
            {
                r.Failure(e);
            }
            return r;
        }
    }
}