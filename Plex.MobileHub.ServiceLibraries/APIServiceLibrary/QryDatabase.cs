using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plex.MobileHub.Data;
using Plex.MobileHub.Data.Types;
namespace Plex.MobileHub.ServiceLibraries.APIServiceLibrary
{
    public class QryDatabase : MethodStrategyBase<MethodResult>
    {
        public IRepository<Consumer> ConsumerRepository { get; set; }
        public IRepository<APP_QUERIES> AppQueryRepository { get; set; }
        public IRepository<CLIENT_DB_COMPANIES> ClientDbCompaniesRepository { get; set; }
        public IRepository<ClientInformation> ClientInfoRepository { get; set; }

   
        public QryResult Strategy(Int32 connectionId, String Query, params object [] arguments)
        {
            try
            {
                if (!ConsumerRepository.Exists(p=> p.ConsumerId == connectionId))
                    throw new Exception();

                var consumer = ConsumerRepository.Retrieve(p=> p.ConsumerId == connectionId);
                var clientInfo = ClientInfoRepository.Retrieve(p => consumer.ClientId == p.ClientId);
                var dbs = ClientDbCompaniesRepository.RetrieveAll().Where(p => p.CLIENT_ID == consumer.ClientId && p.COMPANY_CODE == consumer.DatabaseCode);

                List<Exception> exceptions = new List<Exception>();
                foreach(var database in dbs)
                {
                    try {
                        return clientInfo.Service.ClientCallback.Query(database.DATABASE_CSTRING, Query, arguments);
                    }
                    catch(Exception e){
                        exceptions.Add(e);
                        continue;
                    }
                }
                throw new AggregateException(exceptions);
            }
            catch (Exception e)
            {
                QryResult result = new QryResult();
                result.Failure(e);
                return result;
            }
        }
    }

}
