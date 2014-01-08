using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plex.PMH.Functionality.API
{
    public static partial class Functions
    {
        public static void DeviceSynchronization(int ConnectionId, int DeviceId, int DeviceUserId)
        {
            //todo Check to make sure connectionId is Valid
            //todo Check to make sure DeviceId exists
            //todo make sure device user id exists
            //todo Get all queries for application
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
    }
}