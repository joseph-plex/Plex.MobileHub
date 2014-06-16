using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileHubClient.Misc;
using MobileHubClient.Properties;
using Plex.Logs;
using MobileHubClient.PMH;
using System.Data;
using MobileHubClient.Data;
using Oracle.DataAccess.Client;
//using MobileHubClient.Misc;

namespace MobileHubClient.Core
{
    class ClientStateDisconnected : IClientStateBehaviour
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
                return WebService.ClientDbCompaniesRetrieve(ClientSettings.Instance.ClientId);
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

        public void LogOff()
        {

        }
    }
}
