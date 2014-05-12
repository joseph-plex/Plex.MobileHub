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
    class ClientStateConnected : IClientStateBehaviour
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

                Context.checkInTimer.Stop();
                Context.checkInTimer.Start();

                Context.clientInstanceId = WebService.LogOn(clientId, clientKey, address, port);
                ClientService.Logs.Add("Checked into Web Service");
            }
            catch (Exception e)
            {
                ClientService.Logs.Add(e.ToString());
                throw;
            }
        }

        public void LogOff()
        {
            try
            {
                Context.checkInTimer.Stop();
                WebService.LogOff(Context.clientInstanceId);
            }
            catch (Exception e)
            {
                ClientService.Logs.Add(e.ToString());
                throw;
            }
        }
    }
}
