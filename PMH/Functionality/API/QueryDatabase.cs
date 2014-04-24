using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Plex.MobileHub.Objects;
using Plex.MobileHub.Repositories;

namespace Plex.MobileHub.Functionality.API
{
    public class QueryDatabase : FunctionStrategyBase<QryResult>
    {

        public QryResult Strategy(int connectionId, string query)
        {
            QryResult r = new QryResult();
            try
            {
                if (!Consumers.Instance.Exists(connectionId))
                    throw new Exception("The connection you are specifying does not exist");

                var consumer = Consumers.Instance.Retrieve(connectionId);
                var dbCode = consumer.ClientDbCompaniesGet().COMPANY_CODE;

                var client = consumer.ClientGet();
                r = client.ExecuteRequest<RQryResult>("Query", dbCode, query);
                
            }
            catch(Exception e)
            {
                r.Failure(e);
            }
            return r;
        }
    }
}