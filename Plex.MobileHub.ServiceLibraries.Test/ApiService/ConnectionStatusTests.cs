using System;
using Plex.MobileHub.Data;
using Plex.MobileHub.ServiceLibraries;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Plex.MobileHub.ServiceLibraries.APIServiceLibrary;

namespace Plex.MobileHub.ServiceLibraries.Test.ApiService
{
    [TestClass]
    public class ConnectionStatusTests
    {
        [TestMethod]
        public void ConnectionStatusSuccessTest()
        {
            IRepository<Consumer> ConsRepo = new InMemoryRepository<Consumer>();
            ConsRepo.Insert(new Consumer() { ConsumerId = 1, AppId = 0, ClientId = 0, UserId = 0, DatabaseCode = "Code" });

            ConnectionStatus connectionStatus = new ConnectionStatus();
            connectionStatus.ConsumerRepository = ConsRepo;

            Assert.AreEqual(1,connectionStatus.Strategy(1).Response);
        }
        [TestMethod]
        public void ConnectionStatusConnectionExistsFalse()
        {
            IRepository<Consumer> ConsRepo = new InMemoryRepository<Consumer>();
            ConsRepo.Insert(new Consumer() { ConsumerId = 1, AppId = 0, ClientId = 0, UserId = 0, DatabaseCode = "Code" });

            ConnectionStatus connectionStatus = new ConnectionStatus();
            connectionStatus.ConsumerRepository = ConsRepo;

            Assert.AreEqual(3, connectionStatus.Strategy(2).Response);
        }


    }
}
