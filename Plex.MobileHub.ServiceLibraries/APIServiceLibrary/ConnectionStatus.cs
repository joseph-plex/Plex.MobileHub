using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Plex.MobileHub.Data.Types;
using Plex.MobileHub.Data;

namespace Plex.MobileHub.ServiceLibraries.APIServiceLibrary
{
    public class ConnectionStatus : MethodStrategyBase<MethodResult>
    {
        public IRepository<Consumer> ConsumerRepository { get; set; }
        public MethodResult Strategy(int connectionId)
        {
            try
            {
                /*
               CONSTANT CONN_STATUS_OK = 1
               CONSTANT CONN_STATUS_PMH_ONLY = 2 //not implemented
               CONSTANT CONN_STATUS_INVALID_ID = 3
               CONSTANT CONN_STATUS_CLIENT_DB_OFFLINE = 4 //not implemented
            **/
                if(ConsumerRepository.Exists(p => p.ConsumerId == connectionId))
                    return new MethodResult().Success(3," Connection Id is Invalid");
                return new MethodResult().Success(1, "Connection is Okay");
            }
            catch(Exception e)
            {
                return new MethodResult().Failure(e);
            }
        }
    }
}
