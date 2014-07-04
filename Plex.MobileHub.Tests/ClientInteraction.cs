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
        public void TestMethod1()
        {
            using (ServiceHost host = new ServiceHost())
            {

            }
        }
    }
}
