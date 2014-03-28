using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using MobileHub.Objects;
using MobileHub.Repositories;
using MobileHub.Objects.ResultTypes;
using MobileHub.Data.Tables;
using MobileHub.Data;

namespace MobileHub.Functionality.API
{
    public class DeviceSynchronize : FunctionStrategyBase<DeviceSynchronizeMethodResult>
    {
        public DeviceSynchronizeMethodResult Strategy(int connectionId, int deviceId, int userDataId)
        {
            var result = new DeviceSynchronizeMethodResult();
            try
            {
                var app = AppGet(deviceId);
                var Queries = app.GetAPP_QUERIES();
                var QueryData = GetQueryData(Queries, userDataId);
                var consumer = Consumers.Instance.Retrieve(connectionId);
                var DeviceUserQueryCollection = new List<DEVICE_USER_DATA_QUERIES>();
                var CurrentUserData = new DEVICE_USER_DATA() { DEVICE_ID = deviceId, EXECUTION_INITIATION = result.StartTimeStamp = DateTime.Now };
                CurrentUserData.USER_PERMISSION = GetUserPermission(ClientDBCompanyUsers(consumer.DatabaseId,consumer.UserId).DB_COMPANY_USER_ID, app.APP_ID) ?? -1;
                foreach (var query in Queries)
                {
                    var QueryFrom = QueryFromGet(QueryData, query.QUERY_ID);
                    var _result = new QryExecute().Strategy(connectionId, query.NAME, QueryFrom);
                    DeviceUserQueryCollection.Add(DeviceUserQueryDataCreate(_result, query.QUERY_ID));
                    result.Results.Add(_result);
                }

                CurrentUserData.EXECUTION_COMPLETION = result.CompletionTimeStamp = DateTime.Now;
                InsertDeviceUserData(CurrentUserData, DeviceUserQueryCollection);
                result.Success(CurrentUserData.USER_DATA_ID);
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

        static DEVICE_USER_DATA_QUERIES DeviceUserQueryDataCreate(QueryResult result, int QueryId) 
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

   
    }
}