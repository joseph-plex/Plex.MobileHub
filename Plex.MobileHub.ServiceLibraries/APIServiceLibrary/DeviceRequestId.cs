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
        Func<Int32> KeyGenerator;
        public IRepository<Consumer> ConsumerRepository { get; set; }
        public IRepository<DEV_DATA> DevDataRepository { get; set; }

        public DeviceRequestId(Func<int> keyGenerator)
        {
            KeyGenerator = keyGenerator;
        }

        public MethodResult Strategy(int connectionId)
        {
            try
            {
                DEV_DATA tuple = new DEV_DATA();
                var consumer = ConsumerRepository.Retrieve(p=> p.ConsumerId == connectionId);
                
                tuple.DEVICE_DATABASE_ID = KeyGenerator();
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
