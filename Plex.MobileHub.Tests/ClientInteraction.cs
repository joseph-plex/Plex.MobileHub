using System;
using System.ServiceModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Plex.MobileHub.ServiceLibraries.APIServiceLibrary;

namespace Plex.MobileHub.Tests
{
  
    [TestClass]
    public class ClientInteraction
    {
        const string myPipeService = "net.pipe://localhost";
        const string myPipeServiceName = "ApiPipe";

        [TestMethod]
        public void ApiInitTest()
        {
            Api api = new Api();
            ServiceHost host = new ServiceHost(api,new Uri(myPipeService));
            host.AddServiceEndpoint(typeof(IApiService), new NetNamedPipeBinding(), myPipeServiceName);
            host.BeginOpen(OnOpen, host);
            host.BeginClose(OnClose, host);
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
