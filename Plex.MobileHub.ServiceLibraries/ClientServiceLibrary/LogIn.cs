using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Plex.MobileHub.Data;
using Plex.MobileHub.Data.Types;
namespace Plex.MobileHub.ServiceLibraries.ClientServiceLibrary
{
    public class LogIn : MethodStrategyBase<Boolean>
    {
        public IRepository<CLIENTS> ClientsRepository { get; set; }
        public IRepository<ClientInformation> ClientInfoRepository { get; set; }

        public Boolean Strategy(Int32 clientId, String clientKey, IClientCallback callback = null)
        {
            var client = ClientsRepository.Retrieve(p => p.CLIENT_ID == clientId);
            if (client == null)
                throw new Exception("Non Existant Client");
            if (client.CLIENT_KEY != clientKey)
                throw new Exception("Invalid ClientKey");

            //This is how we determine a client is online
            client.CLIENT_INSTANCE_ID = client.CLIENT_ID;
            ClientsRepository.Update(client);

            ClientInformation info = new ClientInformation()
            {
                ClientId = client.CLIENT_ID,
                Callback = callback
            };
            ClientInfoRepository.Insert(info);
            return true;
        }
    }
}
