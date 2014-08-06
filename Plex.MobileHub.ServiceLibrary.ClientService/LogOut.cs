using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plex.Data;
using Plex.MobileHub.ServiceLibrary.Types;
namespace Plex.MobileHub.ServiceLibrary.ClientService

{
    public class LogOut : MethodStrategyBase<Boolean>
    {
        public Boolean Strategy(Int32 clientId)
        {
            if (GetRepository<ClientInformation>().Exists(p => p.ClientId == clientId))
                GetRepository<ClientInformation>().Delete(p => p.ClientId == clientId);


            var client = GetRepository<CLIENTS>().Retrieve(p => p.CLIENT_ID == clientId);
            client.CLIENT_INSTANCE_ID = client.CLIENT_PORT = 0;
            client.CLIENT_IP_ADDRESS = String.Empty;
            GetRepository<CLIENTS>().Update(client);

            return true;
        }
    }
}
