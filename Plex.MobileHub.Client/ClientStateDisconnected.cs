using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileHubClient.Misc;
using MobileHubClient.Properties;
using MobileHubClient.Logs;

namespace MobileHubClient
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
                var port = ClientSettings.Default.Port;
                var address = ClientSettings.Default.Address;
                var clientId = ClientSettings.Default.ClientId;
                var clientKey = ClientSettings.Default.ClientKey;

                Context.clientInstanceId = WebService.LogOn(clientId, clientKey, address, port);
                Context.checkInTimer.Start();
                LogManager.Instance.Add("Log On To Web Service");
            }
            catch(Exception e)
            {
                LogManager.Instance.Add(e);
                throw;
            }
        }

        public void LogOff()
        {

        }
    }
}
