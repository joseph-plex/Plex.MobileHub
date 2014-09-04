using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Pmh.Client
{
    static class Program
    {
        static void Main()
        {
            MobileHubClient client = new MobileHubClient();
            //ServiceBase.Run(new ServiceBase[] { client });
            client.OnDebug(null);
        }
    }
}
