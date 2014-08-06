using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plex.MobileHub.ServiceLibrary.ApiService

{
    public class ConnectionRelease : MethodStrategyBase<MethodResult>
    {
        public MethodResult Strategy(int connectionId)
        {
            try
            {
                GetRepository<ConsumerInformation>().Delete(p => p.ConsumerId == connectionId);
                return new MethodResult().Success();
            }
            catch(Exception e)
            {
                return new MethodResult().Failure(e);
            }
        }
    }
}
