using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace Plex.MobileHub.Tests
{
    [ServiceBehavior(InstanceContextMode=InstanceContextMode.Single)]
    public class ClientTestService : ClientService
    {
        public ClientTestService() : base() { }

    }
}
