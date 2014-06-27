using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plex.MobileHub.Data;
using Plex.MobileHub.Data.Types;
using Plex.MobileHub.ServiceLibraries;

namespace Plex.MobileHub.ServiceLibraries.APIServiceLibrary
{
    public class ConnectionRelease : MethodStrategyBase<MethodResult>
    {
        public IRepository<Consumer> ConsumerRepository { get; set; }
        public MethodResult Strategy(int connectionId)
        {
            try
            {
                ConsumerRepository.Delete(p => p.ConsumerId == connectionId);
                return new MethodResult().Success();
            }
            catch(Exception e)
            {
                return new MethodResult().Failure(e);
            }
        }
    }
}
