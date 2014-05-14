using System;
using System.ServiceProcess;
using MobileHubClient.Core;
namespace MobileHubClient
{
    static class Program
    {
        static void Main()
        {
            ClientService myService = new ClientService();
            
            //#if(!DEBUG)
            ServiceBase.Run(myService);
            //#else
            //myService.OnDebug();
            //Console.WriteLine("Press Enter to terminate ...");
            //Console.ReadLine();
            //#endif
        }
    }
}
