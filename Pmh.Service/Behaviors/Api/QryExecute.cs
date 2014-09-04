using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plex.Data;
using Pmh.ServiceLibrary.Types;
using Pmh.ServiceLibrary;

namespace Pmh.ServiceLibrary.ApiService

{
    public class QryExecute : MethodStrategyBase<RegisteredQueryResult> 
    {
        public RegisteredQueryResult Strategy(Int32 connectionId, String queryName, DateTime? time)
        {
            try
            {
                if (!GetRepository<ConsumerInformation>().Exists(p => p.ConsumerId == connectionId))
                    throw new Exception();

                var consumer = GetRepository<ConsumerInformation>().Retrieve(p => p.ConsumerId == connectionId);
                var clientInfo = GetRepository<ClientCallback>().Retrieve(p => consumer.ClientId == p.ClientId);

                var query = GetRepository<APP_QUERIES>().Retrieve(p => p.NAME == queryName && p.APP_ID == consumer.AppId);

                var dbs = GetRepository<CLIENT_DB_COMPANIES>().RetrieveAll().Where(p => p.CLIENT_ID == consumer.ClientId && p.COMPANY_CODE == consumer.DatabaseCode);

                if (query == null) //No such query name exists
                    throw new Exception("No Such Query Exists for this Application.");
                if (dbs.Count() == 0)
                    throw new Exception("No Accessible Databases for current user");

                List<Exception> exceptions = new List<Exception>();
                foreach (var database in dbs)
                {
                    try
                    {
                        RegisteredQueryResult rqr = clientInfo.ExecuteRegisteredQuery(database.DATABASE_CSTRING, queryName, time);
                        rqr.Success();
                        rqr.TableName = query.TABLE_NAME;
                        rqr.QueryName = query.NAME;
                        return rqr;
                    }
                    catch (Exception e)
                    {
                        exceptions.Add(e);
                        continue;
                    }
                }
                throw new AggregateException(exceptions);
            }
            catch(Exception e)
            {
                RegisteredQueryResult result = new RegisteredQueryResult();
                result.Failure(e);
                return result;
            }
        }
    }
}
