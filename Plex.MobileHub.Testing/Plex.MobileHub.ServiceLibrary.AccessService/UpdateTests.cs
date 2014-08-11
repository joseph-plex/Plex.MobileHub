using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Plex.MobileHub.Testing.Resources;
using Plex.MobileHub.ServiceLibrary.Types;
using Plex.MobileHub.ServiceLibrary.AccessService;
using System.Linq;
namespace Plex.MobileHub.Testing.Plex.MobileHub.ServiceLibrary.AccessService
{
    [TestClass]
    public class UpdateTests
    {
        [TestMethod]
        public void UpdateTest1()
        {
            var Update  = new Update{Repositories= new RepoTestFactory().GetRepositories()};
            var v = Update.GetRepository<CLIENTS>().RetrieveAll().FirstOrDefault();
            if (v == null)
                Assert.Inconclusive("There is nothing in the test database to edit");
            var count = Update.GetRepository<CLIENTS>().Count(p => p.CLIENT_KEY == "Chicken");
            var offset = (v.CLIENT_KEY == "Chicken") ? 0 : 1;
            v.CLIENT_KEY = "Chicken";
            Update.Strategy(typeof(CLIENTS).FullName, v);
            Assert.AreEqual(count + offset, Update.GetRepository<CLIENTS>().Count(p => p.CLIENT_KEY == "Chicken"));
        }
    }
}
