using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Plex.MobileHub.ServiceLibrary.Types;

namespace Plex.MobileHub.ServiceLibrary.ApiService

{
    public class DeviceRequestId : MethodStrategyBase<Object>
    {
        Func<Int32> KeyGenerator;

        public DeviceRequestId(Func<Int32> keyGenerator)
        {
            KeyGenerator = keyGenerator;
        }

        public MethodResult Strategy(Int32 connectionId)
        {
            try
            {
                DEV_DATA tuple = new DEV_DATA();
                var consumer = GetRepository<ConsumerInformation>().Retrieve(p => p.ConsumerId == connectionId);
                
                tuple.DEVICE_DATABASE_ID = KeyGenerator();
                tuple.CLIENT_ID = consumer.ClientId;
                tuple.USER_ID = consumer.UserId;
                tuple.APP_ID = consumer.AppId;

                GetRepository<DEV_DATA>().Insert(tuple);
                return new MethodResult().Success(tuple.DEVICE_DATABASE_ID);
            }
            catch (Exception e)
            {
                return new MethodResult().Failure(e);
            }
        }
    }
}
