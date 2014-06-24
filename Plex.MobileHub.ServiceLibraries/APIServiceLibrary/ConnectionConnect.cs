using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plex.MobileHub.ServiceLibraries.APIServiceLibrary
{
    class ConnectionConnect : MethodStrategyBase<Object> //todo change this to method result
    {
        public Object Strategy(int clientId, int appId, string database, string user, string password)
        {
            return new Object();
        }
    }
}
