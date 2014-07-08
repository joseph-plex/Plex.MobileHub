using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MobileHubClient.Misc;
using MobileHubClient.Properties;
using Plex.Logs;
using MobileHubClient.PMH;
using MobileHubClient.Data;
using Oracle.DataAccess.Client;
namespace MobileHubClient.Core
{
    class ClientStateConnected : IClientStateBehaviour
    {
        public ClientService Context
        {
            get;
            set;
        }

        public IReadOnlyCollection<CLIENT_DB_COMPANIES> DbConnections
        {
            get
            {
                return WebService.ClientDbCompaniesRetrieve(Context.clientInstanceId);
            }
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
                ClientService.Logs.Add("Checked Logged out of Web Service");

            }
            catch (Exception e)
            {
                ClientService.Logs.Add(e.ToString());
                throw;
            }
        }

        public IDbConnection GetOpenConnection(String Code)
        {
            foreach (var Db in DbConnections.Where(p => p.COMPANY_CODE == Code))
            {
                try
                {
                    return new OracleConnection(Db.DATABASE_CSTRING).GetOpenConnection();
                }
                catch (Exception e)
                {
                    ClientService.Logs.Add(e);
                    continue;
                }
            }
            throw new Exception("No Open Connection Is Available");
        }
    }
}
