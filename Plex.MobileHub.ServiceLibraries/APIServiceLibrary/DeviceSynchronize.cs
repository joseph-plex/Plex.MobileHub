using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

using Plex.MobileHub.Data;
using Plex.MobileHub.Data.Types;
using Plex.MobileHub.ServiceLibraries.ClientServiceLibrary;

namespace Plex.MobileHub.ServiceLibraries.APIServiceLibrary
{
    public class DeviceSynchronize : MethodStrategyBase<Object>
    {
        IApiService Service { get; set; }
        public IRepository<DEV_DATA> DevDataRepository { get; set; }
        public IRepository<Consumer> ConsumerRepository { get; set; }
        public IRepository<APP_QUERIES> AppQueriesRepository { get; set; }
        public IRepository<DEV_DATA_VER> DevDataVerRepository { get; set; }
        public IRepository<DEV_DATA_VER_QUERIES> DevDataVerQueriesRepository { get; set; }

        public DeviceSynchronize(IApiService service)
        {
            Service = service;
        }
        public Object Strategy(int connectionId, int DataVersion)
        {
            try { 
                //Init
                IDbConnection connection;
                DEV_DATA data = new DEV_DATA();
                IEnumerable<DEV_DATA_VER_QUERIES> pQueries = null;
                DEV_DATA_VER previous = null, current = new DEV_DATA_VER();
                Consumer consumer = ConsumerRepository.Retrieve(p => p.ConsumerId == connectionId);

                //If DataVersion is specified get the previous aggregate data set information. If it doesn't exist then I/O error.
                if (DataVersion > 0 && (previous = DevDataVerRepository.Retrieve(p => p.DEV_DATA_VER_ID == DataVersion)) == null)
                    throw new Exception("Device Database Version does not exist");

                connection = OracleRepository.GetIDbConnection(); 

                //Depending on previous data (if it exists) creating a developer data version differs
                current.DEV_DATA_VER_ID = Convert.ToInt32(connection.Query("select DEV_DATA_VER_SEQ.NextVal from dual")[0, 0]);
                if (previous == null)
                {
                    pQueries = new List<DEV_DATA_VER_QUERIES>();
                    current.DEV_DATA_ID = data.DEVICE_DATABASE_ID;
                    DevDataRepository.Insert(data = new DEV_DATA()
                    {
                        DEVICE_DATABASE_ID = Convert.ToInt32(connection.Query("select DEV_DATA_SEQ.nextval from dual")[0, 0]),
                        CLIENT_ID = consumer.ClientId,
                        USER_ID = consumer.UserId,
                        APP_ID = consumer.AppId
                    });
                }
                else
                {
                    current.DEV_DATA_VER_ID = previous.DEV_DATA_ID;
                    data = DevDataRepository.Retrieve(p => p.DEVICE_DATABASE_ID == current.DEV_DATA_VER_ID);
                }
                DevDataVerRepository.Insert(current);

                //Get a list of queries
                pQueries = (previous == null) ?  new List<DEV_DATA_VER_QUERIES>() :
                    DevDataVerQueriesRepository.RetrieveAll().Where(p => p.DATABASE_VERSION_ID == previous.DEV_DATA_VER_ID);

                var queries = AppQueriesRepository.RetrieveAll().Where(p => p.APP_ID == data.APP_ID);
                var results = new List<RegisteredQueryResult>();

                using (connection)
                {
                    var result = new DeviceSynchronizeMethodResult() { StartTimeStamp = DateTime.Now };
                    Parallel.ForEach<APP_QUERIES>(queries, q =>
                        {
                            RegisteredQueryResult rqr;
                            var pQuery = pQueries.FirstOrDefault(p => p.QUERY_ID == q.QUERY_ID);
                            results.Add(rqr = (pQuery == null) ?
                                Service.QryExecute(connectionId, q.NAME) :
                                Service.QryExecute(connectionId, q.NAME, pQuery.QUERY_EXECUTION_TIME));
                            DevDataVerQueriesRepository.Insert(new DEV_DATA_VER_QUERIES()
                            {
                                QUERY_ID = q.QUERY_ID,
                                DATABASE_VERSION_ID = current.DEV_DATA_VER_ID,
                                QUERY_EXECUTION_TIME = rqr.StartTimeStamp
                            });

                        });
                    result.CompletionTimeStamp = DateTime.Now;
                    result.Success(current.DEV_DATA_VER_ID);
                    return result;
                }
            }
            catch (Exception e)
            {
                var result = new DeviceSynchronizeMethodResult();
                result.Failure(e);
                return result;
            }
        }
    }
}
