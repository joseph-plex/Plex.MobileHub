using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Plex.MobileHub.ServiceLibrary;
using Plex.MobileHub.ServiceLibrary.Types;
using System.Data;
using Plex.Data;
namespace Plex.MobileHub.Testing.Plex.MobileHub.ServiceLibrary.Types
{
    [TestClass]
    public class ClientAppsTests
    {
        /// <summary>
        /// This Test ensures the CLIENT_APPS SQL text for selecting is syntactically correct.
        /// </summary>
        [TestMethod]
        public void ClientAppsTest1()
        {
            var repo = new OracleRepository<CLIENT_APPS>();
            using (IDbConnection connection = repo.Open())
                connection.NonQuery("EXPLAIN PLAN FOR " + repo.SelectText);
        }
        /// <summary>
        /// This Test ensures the CLIENT_APPS SQL text for inserting is syntactically correct.
        /// </summary>
        [TestMethod]
        public void ClientAppsTest2()
        {
            var repo = new OracleRepository<CLIENT_APPS>();
            using (IDbConnection connection = repo.Open())
                connection.NonQuery("EXPLAIN PLAN FOR " + repo.InsertText);
        }

        /// <summary>
        /// This Test ensures the CLIENT_APPS SQL text for updating is syntactically correct.
        /// </summary>
        [TestMethod]
        public void ClientAppsTest3()
        {
            var repo = new OracleRepository<CLIENT_APPS>();
            using (IDbConnection connection = repo.Open())
                connection.NonQuery("EXPLAIN PLAN FOR " + repo.UpdateText);
        }


        /// <summary>
        /// This Test ensures the CLIENT_APPS SQL text for deleting is syntactically correct.
        /// </summary>
        [TestMethod]
        public void ClientAppsTest4()
        {
            var repo = new OracleRepository<CLIENT_APPS>();
            using (IDbConnection connection = repo.Open())
                connection.NonQuery("EXPLAIN PLAN FOR " + repo.DeleteText);
        }
    }
}
