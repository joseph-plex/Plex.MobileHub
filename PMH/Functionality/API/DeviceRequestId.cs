using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using MobileHub.Objects;
using MobileHub.Data;
using MobileHub.Data.Tables;
using MobileHub.Repositories;

namespace MobileHub.Functionality.API
{
    public class DeviceRequestId : FunctionStrategyBase<MethodResult>
    {
        public MethodResult Strategy(int ConnectionId)
        {
            MethodResult result = new MethodResult();
            try
            {
                DEVICES tuple = new DEVICES();
                var ConnData = Consumers.Instance.Retrieve(ConnectionId);

                using (var Conn = Utilities.GetConnection(true))
                    tuple.DEVICE_ID = Convert.ToInt32(Conn.Query("select DEVICE_ID.Nextval from dual")[0, 0]);
             
                tuple.APP_ID = ConnData.AppId;
                tuple.Insert();

                result.Success(tuple.DEVICE_ID);
            }
            catch(Exception e)
            {
                result.Failure(e.ToString());
            }
            return result;
        }
    }
}