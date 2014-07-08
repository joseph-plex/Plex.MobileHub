using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
namespace Plex.MobileHub.Client
{
    [ServiceBehavior(InstanceContextMode=InstanceContextMode.Single)]
    public class ClientCommandService : IClientCommandService
    {
        public void LogIn(int id, string key)
        {
            throw new NotImplementedException();
        }

        public void LogOff()
        {
            throw new NotImplementedException();
        }
    }
}
