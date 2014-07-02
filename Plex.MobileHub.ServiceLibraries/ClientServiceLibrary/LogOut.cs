using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Plex.MobileHub.Data;
using Plex.MobileHub.Data.Types;

namespace Plex.MobileHub.ServiceLibraries.ClientServiceLibrary
{
    public class LogOut : MethodStrategyBase<Boolean>
    {
        public IRepository<CLIENTS> ClientsRepository { get; set; }
        public IRepository<ClientInformation> ClientInfoRepository { get; set; }
        public Boolean Strategy(Int32 clientId)
        {
            if (ClientInfoRepository.Exists(p => p.ClientId == clientId))
                ClientInfoRepository.Delete(p => p.ClientId == clientId);

            var client = ClientsRepository.Retrieve(p => p.CLIENT_ID == clientId);
            client.CLIENT_INSTANCE_ID = client.CLIENT_PORT = 0;
            client.CLIENT_IP_ADDRESS = String.Empty;
            ClientsRepository.Update(client);

            return true;
        }
    }
}
