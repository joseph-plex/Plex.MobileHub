using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

using Plex.PMH.Repositories;
using Plex.PMH.Exceptions;
using Plex.PMH.Data.Tables;
using Plex.PMH.Objects;

namespace Plex.PMH.Functionality.API
{
    public static partial class Functions
    {
        public static XmlDocument QryExecuteXml(int ConnectionId, string QueryName, DateTime? Time = null)
        {
            QueryResult r = new QueryResult();
            try
            {
                if (!Consumers.Instance.Exists(ConnectionId))
                    throw new InvalidConsumerException();
                var cust = Consumers.Instance.Retrieve(ConnectionId);

                var dbList = new List<CLIENT_DB_COMPANIES>(CLIENT_DB_COMPANIES.GetAll());
                var index = dbList.FindIndex((p) => p.DB_COMPANY_ID == cust.DatabaseId);
                if (index == -1) throw new Exception("Cannot find specified database");

                var Queries = new List<APP_QUERIES>(APP_QUERIES.GetAll());
                var QueryIndex = Queries.FindIndex((p) => p.NAME == QueryName && p.APP_ID == Consumers.Instance.Retrieve(ConnectionId).AppId);
                if (QueryIndex == -1) throw new InvalidQueryException();

                Logs.Instance.Add(Queries[QueryIndex].QUERY_ID);
                Logs.Instance.Add(Queries[QueryIndex].NAME);

                r = fQuery(cust.ClientId, dbList[index].COMPANY_CODE, Queries[QueryIndex].QUERY_ID, Time);
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