using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Plex.MobileHub.Data;
using Plex.MobileHub.Data.Types;

namespace Plex.MobileHub.ServiceLibraries.APIServiceLibrary
{
    public class DeviceRequestId : MethodStrategyBase<Object>
    {
        public IRepository<Consumer> ConsumerRepository { get; set; }
        public IRepository<DEV_DATA> DevDataRepository { get; set; }

        public MethodResult Strategy(int connectionId)
        {
            try
            {
                DEV_DATA tuple = new DEV_DATA();
                var consumer = ConsumerRepository.Retrieve(p=> p.ConsumerId == connectionId);
                //Do something different.
                using (var connection = OracleRepository.GetIDbConnection())
                    tuple.DEVICE_DATABASE_ID = Convert.ToInt32(connection.Query("select DEVICE_ID.nextval from dual")[0, 0]);

                tuple.CLIENT_ID = consumer.ClientId;
                tuple.USER_ID = consumer.UserId;
                tuple.APP_ID = consumer.AppId;

                DevDataRepository.Insert(tuple);
                return new MethodResult().Success(tuple.DEVICE_DATABASE_ID);
            }
            catch (Exception e)
            {
                return new MethodResult().Failure(e);
            }
        }
    }
}
