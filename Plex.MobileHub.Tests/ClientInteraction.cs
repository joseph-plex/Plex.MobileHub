using System;
using System.ServiceModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using Plex.MobileHub.ServiceLibraries.APIServiceLibrary;

namespace Plex.MobileHub.Tests
{
  
    [TestClass]
    public class ClientInteraction
    {
        const string myPipeService = "net.pipe://localhost";
        const string myPipeServiceName = "ApiPipe";

        [TestMethod]
        [Timeout(2000)]
        public void ApiInitTest()
        {
            bool Success , SuccessSet = Success = false;
            ServiceHost host = new ServiceHost(typeof(Api),new Uri(myPipeService));
            host.AddServiceEndpoint(typeof(IApiService), new NetNamedPipeBinding(), myPipeServiceName);
            host.Opened += (s, e) => 
            { 
                host.BeginClose(OnClose, host);
                SuccessSet = Success =  true;
            };
            host.BeginOpen(OnOpen, host);

            while (!SuccessSet)
                Thread.Yield();

            Assert.IsTrue(Success);
        }



        void OnOpen(IAsyncResult ar)
        {
            ServiceHost service = (ServiceHost)ar.AsyncState;
            service.EndOpen(ar);
        }
        void OnClose(IAsyncResult ar)
        {
            ServiceHost service = (ServiceHost)ar.AsyncState;
            service.EndClose(ar);
        }
    }
}
