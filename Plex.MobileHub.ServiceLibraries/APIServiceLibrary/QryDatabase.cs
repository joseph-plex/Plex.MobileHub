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

        public MethodResult Strategy(int connectionId, string queryName, DateTime? time = null)
        {
            try
            {
                if (!ConsumerRepository.Exists(p=> p.ConsumerId == connectionId))
                    throw new Exception();

                var consumer = ConsumerRepository.Retrieve(p=> p.ConsumerId == connectionId);
                var query = AppQueryRepository.Retrieve(p => p.APP_ID == consumer.AppId && queryName == p.NAME);
                
                if (query == null)
                    throw new Exception();

                ClientDbCompaniesRepository.RetrieveAll().Where(p => p.CLIENT_ID == consumer.ClientId && p.COMPANY_CODE == consumer.DatabaseCode);

                //Complete this.
                //todo : Execute Query

                return null;
            }
            catch(Exception e)
            {
                return new MethodResult().Failure(e);
            }
        }
    }
}
