using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plex.MobileHub.Data;
using Plex.MobileHub.Data.Types;
namespace Plex.MobileHub.ServiceLibraries.APIServiceLibrary
{
    public class QryExecute : MethodStrategyBase<QryResult> 
    {
        public IRepository<Consumer> ConsumerRepository { get; set; }
        public IRepository<APP_QUERIES> AppQueryRepository { get; set; }
        public IRepository<ClientInformation> ClientInfoRepository { get; set; }
        public IRepository<CLIENT_DB_COMPANIES> ClientDbCompaniesRepository { get; set; }

        public RegisteredQueryResult Strategy(Int32 connectionId, String queryName, DateTime? time)
        {
            try
            {
                if (!ConsumerRepository.Exists(p => p.ConsumerId == connectionId))
                    throw new Exception(); 
                
                var consumer = ConsumerRepository.Retrieve(p => p.ConsumerId == connectionId);
                var clientInfo = ClientInfoRepository.Retrieve(p => consumer.ClientId == p.ClientId);
                var query = AppQueryRepository.Retrieve(p => p.NAME == queryName && p.APP_ID == consumer.AppId);
                var dbs = ClientDbCompaniesRepository.RetrieveAll().Where(p => p.CLIENT_ID == consumer.ClientId && p.COMPANY_CODE == consumer.DatabaseCode);

                if (query == null) //No such query name exists
                    throw new Exception("No Such Query Exists for this Application.");
                if (dbs.Count() == 0)
                    throw new Exception("No Accessible Databases for current user");

                List<Exception> exceptions = new List<Exception>();
                foreach (var database in dbs)
                {
                    try
                    {
                        RegisteredQueryResult rqr = clientInfo.Callback.ExecuteRegisteredQuery(database.DATABASE_CSTRING, queryName, time);
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
