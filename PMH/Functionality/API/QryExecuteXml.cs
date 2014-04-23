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
    public class QryExecuteXml : FunctionStrategyBase<XmlDocument>
    {
        public XmlDocument Strategy(int ConnectionId, string queryName, DateTime? Time = null)
        {
            RQryResult r = new RQryResult();
            try
            {
                if (!Consumers.Instance.Exists(ConnectionId)) throw new InvalidConsumerException();
                var cust = Consumers.Instance.Retrieve(ConnectionId);

                var query = cust.GetAccessibleQueries().First(p => p.APP_ID == cust.AppId && p.NAME == queryName);
                if (query == null) throw new InvalidQueryException();

                Logs.Instance.Add(query.NAME + " : " + query.QUERY_ID);

                var dbCode = cust.ClientDbCompaniesGet().COMPANY_CODE;
                var client = Connections.Instance.Retrieve(cust.ClientId);

                r = client.ExecuteRequest<RQryResult>("ExecuteRegisteredQuery", dbCode, query.QUERY_ID, Time);
                if (r.Response > 0)
                    r.Success(r.Response);
            }
            catch (Exception e)
            {
                r.Failure(e);
            }
            return r;
        }
    }
}