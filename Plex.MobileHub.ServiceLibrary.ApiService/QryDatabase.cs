using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plex.MobileHub.ServiceLibrary.Types;
namespace Plex.MobileHub.ServiceLibrary.ApiService

{
    public class QryDatabase : MethodStrategyBase<MethodResult>
    {
        public QryResult Strategy(Int32 connectionId, String Query, params object [] arguments)
        {
            try
            {
                if (!GetRepository<ConsumerInformation>().Exists(p => p.ConsumerId == connectionId))
                    throw new Exception();

                var consumer = GetRepository<ConsumerInformation>().Retrieve(p => p.ConsumerId == connectionId);
                var clientInfo = GetRepository<ClientInformation>().Retrieve(p => consumer.ClientId == p.ClientId);

                var dbs = GetRepository<CLIENT_DB_COMPANIES>().RetrieveAll().Where(p => p.CLIENT_ID == consumer.ClientId && p.COMPANY_CODE == consumer.DatabaseCode);

                if (dbs.Count() == 0)
                    throw new Exception("No Accessible Databases for current user");
                List<Exception> exceptions = new List<Exception>();
                foreach(var database in dbs)
                {
                    try {
                        QryResult qr = clientInfo.Callback.Query(database.DATABASE_CSTRING, Query, arguments);
                        qr.Success();
                        return qr;
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
