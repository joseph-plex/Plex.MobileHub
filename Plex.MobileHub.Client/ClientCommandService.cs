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
        public bool LoggedIn;
        public MobileHubClient Context { get; private set; }

        public ClientCommandService(MobileHubClient context)
        {
            Context = context;
            LoggedIn = false;
        }

        public void LogIn(int id, string key)
        {
            Context.consumer.LogIn(id, key);
            LoggedIn = true;
        }

        public void LogOff()
        {
            Context.consumer.LogOut();
            LoggedIn = false;
        }
    }
}
