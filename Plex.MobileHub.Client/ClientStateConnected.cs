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
                var port = ClientSettings.Default.Port;
                var address = ClientSettings.Default.Address;
                var clientId = ClientSettings.Default.ClientId;
                var clientKey = ClientSettings.Default.ClientKey;

                Context.checkInTimer.Stop();
                Context.checkInTimer.Start();

                Context.clientInstanceId = WebService.LogOn(clientId, clientKey, address, port);
                LogManager.Instance.Add("Checked into Web Service");
            }
            catch (Exception e)
            {
                LogManager.Instance.Add(e);
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
                LogManager.Instance.Add(e);
                throw;
            }
        }
    }
}
