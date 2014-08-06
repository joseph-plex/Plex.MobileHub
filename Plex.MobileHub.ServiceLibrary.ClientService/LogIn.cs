using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plex.MobileHub.ServiceLibrary.Types;

namespace Plex.MobileHub.ServiceLibrary.ClientService

{
    public class LogIn : MethodStrategyBase<Boolean>
    {
        public Boolean Strategy(Int32 clientId, String clientKey, IClientCallback callback = null)
        {
            var client = GetRepository<CLIENTS>().Retrieve(p => p.CLIENT_ID == clientId);
            if (client == null)
                throw new Exception("Non Existant Client");
            if (client.CLIENT_KEY != clientKey)
                throw new Exception("Invalid ClientKey");

            //This is how we determine a client is online
            client.CLIENT_INSTANCE_ID = client.CLIENT_ID;
            GetRepository<CLIENTS>().Update(client);

            ClientInformation info = new ClientInformation()
            {
                ClientId = client.CLIENT_ID,
                Callback = callback
            };
            GetRepository<ClientInformation>().Insert(info);
            return true;
        }
    }
}
