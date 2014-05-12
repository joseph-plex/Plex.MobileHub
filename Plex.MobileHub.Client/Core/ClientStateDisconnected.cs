using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileHubClient.Misc;
using MobileHubClient.Properties;
using Plex.Logs;
namespace MobileHubClient.Core
{
    class ClientStateDisconnected : IClientStateBehaviour
    {
        public ClientService Context
        {
            get;
            set;
        }

        public void LogOn()
        {
            try
            {
                var port = ClientSettings.Instance.Port;
                var address = ClientSettings.Instance.Address;
                var clientId = ClientSettings.Instance.ClientId;
                var clientKey = ClientSettings.Instance.ClientKey;

                Context.clientInstanceId = WebService.LogOn(clientId, clientKey, address, port);
                Context.checkInTimer.Start();
                ClientService.Logs.Add("Log On To Web Service");
            }
            catch(Exception e)
            {
                ClientService.Logs.Add(e.ToString());
                throw;
            }
        }

        public void LogOff()
        {

        }
    }
}
