using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plex.MobileHub.ServiceLibrary.Types;
using System.Security.Cryptography;
using System.Net;

namespace Plex.MobileHub.ServiceLibrary.ClientService
{
    public class LogIn : MethodStrategyBase<Boolean>
    {
        //warning needs to be redone in entity framework.
        public String Strategy(Int32 clientId, String clientKey, String ipAddress, Int32 Port)
        {
            var token = Hash(clientId.ToString() + DateTime.Now);

            CLIENTS result = GetRepository<CLIENTS>().Retrieve(p => p.CLIENT_ID == clientId);

            if (result == default(CLIENTS))
                throw new Exception("Client Id does not exist");

            if (clientKey != result.CLIENT_KEY)
                throw new Exception("Invalid Client Key");

            result.CLIENT_IP_ADDRESS = ipAddress;
            result.CLIENT_TOKEN = token;
            result.CLIENT_PORT = Port;

            GetRepository<CLIENTS>().Update(result);
            return token;
        }

        public String Hash(String input)
        {
            using (MD5 hash = MD5.Create())
            {
                StringBuilder sBuilder = new StringBuilder();
                byte[] data = hash.ComputeHash(Encoding.UTF8.GetBytes(input));
                for (int i = 0; i < data.Length; i++)
                    sBuilder.Append(data[i].ToString("x2"));
                return sBuilder.ToString();
            }
        }
    }

}
