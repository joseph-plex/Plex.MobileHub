using System;
using System.ServiceProcess;

namespace MobileHubClient
{
    static class Program
    {
        static void Main()
        {
            ServiceBase.Run(new ClientService());
        }
    }
}
