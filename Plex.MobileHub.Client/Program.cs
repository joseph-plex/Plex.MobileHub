using System;
using System.ServiceProcess;
using MobileHubClient.Core;
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
