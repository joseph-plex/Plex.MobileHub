using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Plex.MobileHub.ServiceLibrary.AccessService;
using Plex.MobileHub.ServiceLibrary;
using Plex.Data;
using Plex.MobileHub.ServiceLibrary.Types;
using Plex.MobileHub.Testing.Resources;

namespace Plex.MobileHub.Testing.Plex.MobileHub.ServiceLibrary.AccessService
{
    [TestClass]
    public class DeleteTests
    {
        [TestMethod]
        public void DeleteTest1()
        {
            var delete = new Delete { Repositories = new RepoTestFactory().GetRepositories() };
            var repo = delete.GetRepository<CLIENTS>();
            var oldCount = repo.Count();
            var client = repo.Retrieve(p => true);
            delete.Strategy(typeof(CLIENTS).FullName, new Object[]{ client} );
            Assert.AreEqual(oldCount - 1, repo.Count());
        }
    }
}
