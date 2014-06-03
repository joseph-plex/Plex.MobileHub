using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

using Plex.MobileHub.Objects;
using Plex.MobileHub.Repositories;
using Plex.MobileHub.Objects.ResultTypes;
using Plex.MobileHub.Data.Tables;
using Plex.MobileHub.Data;
using Plex.Diagnostics;
using System.Diagnostics;

namespace Plex.MobileHub.Functionality.API
{
    public class DeviceSynchronize : FunctionStrategyBase<DeviceSynchronizeMethodResult>
    {
        public DeviceSynchronizeMethodResult Strategy(int connectionId, int userDataId)
        {
            var result = new DeviceSynchronizeMethodResult();
            try
            {
                TimeAnalyst.StartTask("Device Synchro");
                TimeAnalyst.StartTask("Init");
                var consumer = Consumers.Instance.Retrieve(connectionId);
                var app = APPS.GetAll().First(p => p.APP_ID == consumer.AppId);
                var Queries = app.GetAPP_QUERIES();
                var QueryData = GetQueryData(Queries, userDataId);
                var DeviceUserQueryCollection = new List<DEVICE_USER_DATA_QUERIES>();
                var CurrentUserData = new DEVICE_USER_DATA() { DEVICE_ID = null, EXECUTION_INITIATION = result.StartTimeStamp = DateTime.Now };
                CurrentUserData.USER_PERMISSION = GetUserPermission(ClientDBCompanyUsers(consumer.DatabaseId, consumer.UserId).DB_COMPANY_USER_ID, app.APP_ID) ?? -1;

                TimeAnalyst.StopTask("Init");
                TimeAnalyst.StartTask("Queries");

                foreach (var query in Queries)
                {
                    var QueryFrom = QueryFromGet(QueryData, query.QUERY_ID);
                    var _result = new QryExecute().Strategy(connectionId, query.NAME, QueryFrom);
                    DeviceUserQueryCollection.Add(DeviceUserQueryDataCreate(_result, query.QUERY_ID));
                    result.Results.Add(_result);
                }
                TimeAnalyst.StopTask("Queries");

                CurrentUserData.EXECUTION_COMPLETION = result.CompletionTimeStamp = DateTime.Now;
                InsertDeviceUserData(CurrentUserData, DeviceUserQueryCollection);
                result.Success(CurrentUserData.USER_DATA_ID);
                TimeAnalyst.StopTask("Device Synchro");
            }
            catch (Exception e)
            {
                result.Failure(e.ToString());
            }
            return result;
        }
        static int? GetUserPermission(int DbCompanyUserId, int AppId)
        {
            var Permission = CLIENT_DB_COMPANY_USER_APPS.GetAll().FirstOrDefault((p) => p.DB_COMPANY_USER_ID == DbCompanyUserId && p.APP_ID == AppId);
            return (Permission != null) ? Permission.DB_COMPANY_USER_APP_ID : (int?)null;
        }
        static CLIENT_DB_COMPANY_USERS ClientDBCompanyUsers(int DbCompanyId, int UserId)
        {
            var collection = new List<CLIENT_DB_COMPANY_USERS>(CLIENT_DB_COMPANY_USERS.GetAll());
            var DBIndex = collection.FindIndex((p) => p.DB_COMPANY_ID == DbCompanyId && UserId == p.USER_ID);
            if (DBIndex == -1) throw new Exception("User does not have access to the database");
            return collection[DBIndex];
        }
        static APPS AppGet(int deviceId)
        {
            var device = DEVICES.GetAll().FirstOrDefault(p => p.DEVICE_ID == deviceId);
            if (device == null) throw new Exception("Invalid Device Id");
            return APPS.GetAll().FirstOrDefault(p => p.APP_ID == device.APP_ID);
        }
        static IEnumerable<DEVICE_USER_DATA_QUERIES> GetQueryData(IEnumerable<APP_QUERIES> queries, int? userDataId)
        {
            return DEVICE_USER_DATA_QUERIES.GetAll()
                .Where(p => userDataId == p.DEVICE_USER_DATA_ID && queries.Any(q => q.QUERY_ID == p.QUERY_ID));
        }
        static DateTime? QueryFromGet(IEnumerable<DEVICE_USER_DATA_QUERIES> dataSet, int queryId)
        {
            var PreviousQueryData = dataSet.FirstOrDefault(p => p.QUERY_ID == queryId);
            return (PreviousQueryData != null)? PreviousQueryData.QUERY_EXECUTION_TIME: (DateTime?)null;
        }
        static DEVICE_USER_DATA_QUERIES DeviceUserQueryDataCreate(RQryResult result, int QueryId) 
        {
            using (var Conn = Utilities.GetConnection(true))
                return new DEVICE_USER_DATA_QUERIES()
                {
                    QUERY_EXECUTION_TIME = result.StartTimeStamp,
                    DEVICE_USER_DATA_QUERIES_ID = Convert.ToInt32(Conn.Query("select DEVICE_ID.Nextval from dual")[0, 0]),
                    QUERY_ID = QueryId,
                };
        }
        static bool InsertDeviceUserData(DEVICE_USER_DATA UserData, IEnumerable<DEVICE_USER_DATA_QUERIES> UserQueryData)
        {
            using (var Conn = Utilities.GetConnection(true))
            {
                using (var Trans = Conn.BeginTransaction())
                {
                    UserData.USER_DATA_ID = Convert.ToInt32(Conn.Query("select DEVICE_ID.Nextval from dual")[0, 0]);
                    UserData.Insert(Conn);
                    foreach (var o in UserQueryData)
                    {
                        o.DEVICE_USER_DATA_ID = UserData.USER_DATA_ID;
                        o.Insert(Conn);
                    }
                    Trans.Commit();
                }
            }
            return true;
        }

        public DeviceSynchronizeMethodResult AlternateStrategy(int connectionId, int DataVersion)
        {
            DeviceSynchronizeMethodResult result = new DeviceSynchronizeMethodResult();
            try
            {
                DEV_DATA_VER Previous = null;
                IEnumerable<DEV_DATA_VER_QUERIES> pQueries = null;
                if (DataVersion != 0) { 
                    Previous = DEV_DATA_VER.GetAll().FirstOrDefault(p => p.DEV_DATA_VER_ID == DataVersion);
                
                    if (Previous == null) throw new Exception("Device Database Version does not exist");
                    pQueries = (Previous == null) ? new List<DEV_DATA_VER_QUERIES>() : DEV_DATA_VER_QUERIES.GetAll().Where(p => p.DATABASE_VERSION_ID == Previous.DEV_DATA_VER_ID).ToList();
                }
                DEV_DATA_VER current = new DEV_DATA_VER()
                {
                    DEV_DATA_ID = (DataVersion != 0) ? (Previous ?? new DEV_DATA_VER()).DEV_DATA_ID : InitDeviceDatabase(connectionId),
                    DEV_DATA_VER_ID = Utilities.GetNextSequenceValue(SequenceType.DEV_DATA_VER_SEQ)
                };
                current.Insert();

                List<APP_QUERIES> Queries = new List<APP_QUERIES>(GetDeviceDataVersionQueries(current.DEV_DATA_VER_ID));
                List<RQryResult> results = new List<RQryResult>();

                using(var Conn = Utilities.GetConnection(true))
                {
                    result.StartTimeStamp = DateTime.Now;
                    Parallel.ForEach<APP_QUERIES>(Queries, q =>
                    {
                        RQryResult res;
                        var pQuery = (pQueries ?? new List<DEV_DATA_VER_QUERIES>()).FirstOrDefault(p => p.QUERY_ID == q.QUERY_ID);

                        results.Add(res = (pQuery == null) ?
                            new QryExecute().Strategy(connectionId, q.NAME, null) :
                            new QryExecute().Strategy(connectionId, q.NAME, pQuery.QUERY_EXECUTION_TIME));

                        new DEV_DATA_VER_QUERIES()
                        {
                            QUERY_ID = q.QUERY_ID,
                            DATABASE_VERSION_ID = current.DEV_DATA_VER_ID,
                            QUERY_EXECUTION_TIME =  res.StartTimeStamp
                        }.Insert();
                    });
                    result.CompletionTimeStamp = DateTime.Now;
                }
                result.Results = results;
                result.Success(current.DEV_DATA_VER_ID);
            }
            catch(Exception e)
            {
                result.Failure(e);
            }
            return result;
        }


        public int InitDeviceDatabase(int connectionId)
        {
            DEV_DATA tuple = new DEV_DATA();
            Consumer consumer = Consumers.Instance.Retrieve(connectionId);

            using (var Conn = Utilities.GetConnection(true))
                tuple.DEVICE_DATABASE_ID = Convert.ToInt32(Conn.Query("select DEV_DATA_SEQ.nextval from dual")[0,0]); //Utilities.GetNextSequenceValue(SequenceType.DEV_DATA_SEQ);

            tuple.APP_ID = consumer.AppId;
            tuple.CLIENT_ID = consumer.ClientId;
            tuple.USER_ID = consumer.UserId;

            tuple.Insert();

            return tuple.DEVICE_DATABASE_ID;
        }

        IEnumerable<APP_QUERIES> GetDeviceDataVersionQueries(int DataVersion)
        {
            var version = DEV_DATA_VER.GetAll().FirstOrDefault(p => p.DEV_DATA_VER_ID == DataVersion);
            if(version == null) throw new Exception("Device Database Version does not exist");

            var database = DEV_DATA.GetAll().FirstOrDefault(p =>p.DEVICE_DATABASE_ID == version.DEV_DATA_ID);
            if (database == null) throw new Exception("Device Database does not exist");

            return APP_QUERIES.GetAll().Where(p => p.APP_ID == database.APP_ID);
        }
    }
}