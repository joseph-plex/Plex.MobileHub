using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Plex.MobileHub.Testing.Resources;
using Plex.MobileHub.ServiceLibrary.Types;
using Plex.MobileHub.ServiceLibrary.AccessService;
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
    }
}
