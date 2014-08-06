using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Plex.MobileHub.ServiceLibrary;
using Plex.MobileHub.ServiceLibrary.Types;
using System.Data;
using Plex.Data;
namespace Plex.MobileHub.Testing.Plex.MobileHub.ServiceLibrary.Types
{
    [TestClass]
    public class ClientUsersTests
    {
        /// <summary>
        /// This Test ensures the CLIENT_USERS SQL text for selecting is syntactically correct.
        /// </summary>
        [TestMethod]
        public void ClientUsersTest1()
        {
            var repo = new OracleRepository<CLIENT_USERS>();
            using (IDbConnection connection = repo.Open())
                connection.NonQuery("EXPLAIN PLAN FOR " + repo.SelectText);
        }
        /// <summary>
        /// This Test ensures the CLIENT_USERS SQL text for inserting is syntactically correct.
        /// </summary>
        [TestMethod]
        public void ClientUsersTest2()
        {
            var repo = new OracleRepository<CLIENT_USERS>();
            using (IDbConnection connection = repo.Open())
                connection.NonQuery("EXPLAIN PLAN FOR " + repo.InsertText);
        }

        /// <summary>
        /// This Test ensures the CLIENT_USERS SQL text for updating is syntactically correct.
        /// </summary>
        [TestMethod]
        public void ClientUsersTest3()
        {
            var repo = new OracleRepository<CLIENT_USERS>();
            using (IDbConnection connection = repo.Open())
                connection.NonQuery("EXPLAIN PLAN FOR " + repo.UpdateText);
        }


        /// <summary>
        /// This Test ensures the CLIENT_USERS SQL text for deleting is syntactically correct.
        /// </summary>
        [TestMethod]
        public void ClientUsersTest4()
        {
            var repo = new OracleRepository<CLIENT_USERS>();
            using (IDbConnection connection = repo.Open())
                connection.NonQuery("EXPLAIN PLAN FOR " + repo.DeleteText);
        }
    }
}
