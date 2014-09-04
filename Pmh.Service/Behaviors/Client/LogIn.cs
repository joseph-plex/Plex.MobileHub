using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pmh.ServiceLibrary.Types;
using System.Security.Cryptography;
using System.Net;
using Plex.Data;
using Pmh.ServiceLibrary;

namespace Pmh.ServiceLibrary.Behaviors.Client
{
    public class LogIn : MethodStrategyBase<Boolean>
    {
        const string callbackAddressTemplate = "http://*a:*p/MobileHubClientCallback.svc";
        
        //warning needs to be redone in entity framework.
        public String Strategy(Int32 clientId, String clientKey, String ipAddress, Int32 port)
        {
            var token = Hash(clientId.ToString() + DateTime.Now);

            CLIENTS result = GetRepository<CLIENTS>().Retrieve(p => p.CLIENT_ID == clientId);

            if (result == default(CLIENTS))
                throw new Exception("Client Id does not exist");
            if (clientKey != result.CLIENT_KEY)
                throw new Exception("Invalid Client Key");
            IPAddress.Parse(ipAddress);
            //Valid port numbers only range from 1 - 65535;
            if (port < 0 || port > 65535)
                throw new Exception("Invalid Port Exception");

            //no point entering it in the system it connection is impossible.
            var ccb = new ClientCallback(UriConstruct(ipAddress, port));
            ccb.Heartbeat(); 

            result.CLIENT_IP_ADDRESS = ccb.IpAddress =  ipAddress;
            result.CLIENT_TOKEN = ccb.Token = token;
            result.CLIENT_PORT = ccb.Port = port;
            ccb.ClientId = clientId;

            GetRepository<ClientCallback>().Insert(ccb);
            GetRepository<CLIENTS>().Update(result);
            
            //Give the Client back its token.
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

        public string UriConstruct(String IPAddress, Int32 Port)
        {
            var cba = callbackAddressTemplate;
            cba = cba.Replace("*a", IPAddress);
            cba = cba.Replace("*p", Port.ToString());
            return cba;
        }
    }

}
