using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Plex.MobileHub.Testing.Resources;
using Plex.MobileHub.ServiceLibrary.Types;
using Plex.MobileHub.ServiceLibrary.AccessService;
using Plex.MobileHub.Service;
using System.Threading;
using System.ServiceModel;
using Plex.MobileHub.Consumer;
namespace Plex.MobileHub.Testing.Plex.MobileHub.ServiceLibrary.AccessService
{
    [TestClass]
    public class InsertTests
    {
        [TestMethod]
        public void InsertTest1()
        {
            var insert = new Insert { Repositories = new RepoTestFactory().GetRepositories() };
            var oldCount = insert.GetRepository<CLIENTS>().Count();
            insert.Strategy(typeof(CLIENTS).FullName, new CLIENTS());
            Assert.AreEqual(oldCount + 1, insert.GetRepository<CLIENTS>().Count());
        }

        [TestMethod]
        public void InsertTest2()
        {
            var pipeLocation = "net.pipe://localhost";
            var pipeName = "MyInsertPipe";
            var pipeUri = pipeLocation + @"/" + pipeName;
            var service = new AccessTestService();
            var host = new ServiceHost(service, new Uri(pipeLocation));
            
            host.AddServiceEndpoint(typeof(IAccessService).FullName, new NetNamedPipeBinding(), pipeName);
            Boolean trigger = false;
            host.Opened +=(s,e)=> trigger = !trigger;
            host.BeginOpen((ar) => host.EndOpen(ar), host);

            while (trigger)
                Thread.Yield();
            
            NetNamedPipeBinding binding = new NetNamedPipeBinding();
            EndpointAddress ep = new EndpointAddress(pipeUri);
            var client = new ChannelFactory<IAccessService>(binding, ep);
            
            client.BeginOpen((ar) => client.EndOpen(ar), client);
            
            var r = new NetworkRepository<CLIENTS>(client);
            var oldcount = service.GetRepository<CLIENTS>().Count();
            Assert.AreEqual(oldcount, r.Count());
            r.Insert(new CLIENTS());
            Assert.AreEqual(oldcount + 1, r.Count());

            client.BeginClose((ar) => client.EndClose(ar), host);
            host.BeginClose((ar) => host.EndClose(ar), host);
        }

        static void OnClose(IAsyncResult ar)
        {
            ServiceHost service = (ServiceHost)ar.AsyncState;
            service.EndClose(ar);
        }
    }
}
