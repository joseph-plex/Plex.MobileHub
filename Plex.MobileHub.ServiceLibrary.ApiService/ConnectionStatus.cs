using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Plex.MobileHub.ServiceLibrary.ApiService

{
    public class ConnectionStatus : MethodStrategyBase<MethodResult>
    {
        public MethodResult Strategy(int connectionId)
        {
            try
            {
                //Todo Finish Implementing this Function along with Test cases
                //This is liable to change.
                /*
               CONSTANT CONN_STATUS_OK = 1
               CONSTANT CONN_STATUS_PMH_ONLY = 2 //not implemented
               CONSTANT CONN_STATUS_INVALID_ID = 3
               CONSTANT CONN_STATUS_CLIENT_DB_OFFLINE = 4 //not implemented
                **/
                if (GetRepository<ConsumerInformation>().Exists(p => p.ConsumerId == connectionId))
                    return new MethodResult().Success(1," Connection Id is Okay");
                return new MethodResult().Success(3, "Connection Id is Invalid");
            }
            catch(Exception e)
            {
                return new MethodResult().Failure(e);
            }
        }
    }
}
