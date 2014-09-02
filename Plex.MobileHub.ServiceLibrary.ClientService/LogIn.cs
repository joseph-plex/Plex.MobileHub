using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plex.MobileHub.ServiceLibrary.Types;
using System.Security.Cryptography;

namespace Plex.MobileHub.ServiceLibrary.ClientService
{
    public class LogIn : MethodStrategyBase<Boolean>
    {
        public String Strategy(Int32 clientId, String clientKey, String ipAddress, Int32 Port)
        {
            var token = Hash(clientId.ToString() + DateTime.Now);

            //todo check to see if clientKey and clientId are valid
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
