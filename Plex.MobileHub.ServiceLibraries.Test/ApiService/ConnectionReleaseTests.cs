using System;
using Plex.MobileHub.ServiceLibraries;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Plex.MobileHub.ServiceLibraries.APIServiceLibrary;

using Plex.MobileHub.Data;

namespace Plex.MobileHub.ServiceLibraries.Test.ApiService
{
    [TestClass]
    public class ConnectionReleaseTests
    {
        [TestMethod]
        public void ConnectionReleaseSuccessConnectionExists()
        {
            //Init
            IRepository<Consumer> ConsRepo = new InMemoryRepository<Consumer>();
            ConsRepo.Insert(new Consumer(){ConsumerId = 1, AppId = 0,ClientId = 0, UserId = 0,  DatabaseCode = "Code"});
            ConnectionRelease connectionRelease = new ConnectionRelease();
            connectionRelease.ConsumerRepository = ConsRepo;
            Assert.AreEqual(0, connectionRelease.Strategy(1).Response);
        }

        [TestMethod]
        public void ConnectionReleaseSuccessConnectionExistFalse()
        {
            //Init
            IRepository<Consumer> ConsRepo = new InMemoryRepository<Consumer>();
            ConsRepo.Insert(new Consumer() { ConsumerId = 1, AppId = 0, ClientId = 0, UserId = 0, DatabaseCode = "Code" });
            ConnectionRelease connectionRelease = new ConnectionRelease();
            connectionRelease.ConsumerRepository = ConsRepo;
            Assert.AreEqual(0, connectionRelease.Strategy(1).Response);
        }

        [TestMethod]
        public void ConnectionReleaseRepoFailure()
        {
            ConnectionRelease connectionRelease = new ConnectionRelease();
            Assert.AreEqual(-9999, connectionRelease.Strategy(0).Response);
        }
    }
}
