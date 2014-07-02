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

                using (var connection = OracleRepository.GetIDbConnection())
                {

                }

                return null;
            }
            catch (Exception e)
            {
                return new MethodResult().Failure(e);
            }
        }
    }
}
