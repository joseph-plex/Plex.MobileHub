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
        /*warning if we ever use this code we CLIENTS.CLIENT_INSTANCE_ID will become obsolete.
        //I've left it presently for backwards compatibility */
        public Boolean Strategy(String token)
        {
            if(String.IsNullOrWhiteSpace(token))
                throw new Exception("Invalid Token");
            //warning this assumes the client token is nullable and is unique. Needs to be implemented in the database
            var client = GetRepository<CLIENTS>().Retrieve(p => p.CLIENT_TOKEN == token);

            client.CLIENT_TOKEN = null;
            client.CLIENT_PORT = null;
            client.CLIENT_IP_ADDRESS = null;

            GetRepository<CLIENTS>().Update(client);
            return true;
        }
    }
}
