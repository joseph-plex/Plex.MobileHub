using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

using MobileHub.Repositories;
using MobileHub.Objects;
using MobileHub.Exceptions;
using MobileHub.Data.Tables;

namespace MobileHub.Functionality.API
{
    public static partial class Functions
    {
        public static List<QueryResult> DeviceSynchronization(int ConnectionId, int DeviceId, int? DeviceUserId)
        {
            //todo Check to make sure connectionId is Valid
            if (!Consumers.Instance.Exists(ConnectionId)) throw new InvalidConsumerException();
            var cons = Consumers.Instance.Retrieve(ConnectionId);
            //todo Check to make sure DeviceId exists
            //var Device = DeviceGet(DeviceId);
            //todo make sure device user id exists
            //todo Get all queries for application
            var App = ApplicationGet(cons.AppId);
            List<QueryResult> Results = new List<QueryResult>();
            Parallel.ForEach(App.GetAPP_QUERIES(),(queries)=>
            {
                //Results.Add(QryExecute(ConnectionId, queries.NAME));
            });

            return Results;
            //todo Get date associated with device user Id
            //todo get all query execution times
            //todo create query with date commands and insert dates from device user data queries.
            
            //todo queue commands in parallel
            //todo assert responses into specialized method result
            //todo create a new device user record
            //todo create new device user data queries for each query
            //todo open new connection and open transactions, insert new device_user data and deviceuserdatauqeries
            //todo return aggregate of results
        }

        public static DEVICES DeviceGet(int DeviceId)
        {
            var Devices = DEVICES.GetAll().ToList();
            var DeviceIndex = Devices.FindIndex((p) => p.DEVICE_ID == DeviceId);
            if (DeviceIndex == -1) throw new Exception("Invalid Device Id");
            return Devices[DeviceIndex];
        }
    }
}