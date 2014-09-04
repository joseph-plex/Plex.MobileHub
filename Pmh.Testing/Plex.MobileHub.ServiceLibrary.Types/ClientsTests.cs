using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pmh.ServiceLibrary;
using Pmh.ServiceLibrary.Types;
using System.Data;
using Plex.Data;
namespace Pmh.Testing.Pmh.ServiceLibrary.Types
{
    [TestClass]
    public class ClientsTests
    {
        /// <summary>
        /// This Test ensures the CLIENTS SQL text for selecting is syntactically correct.
        /// </summary>
        [TestMethod]
        public void ClientsTest1()
        {
            var repo = new OracleRepository<CLIENTS>();
            using (IDbConnection connection = repo.Open())
                connection.NonQuery("EXPLAIN PLAN FOR " + repo.SelectText);
        }
        /// <summary>
        /// This Test ensures the CLIENTS SQL text for inserting is syntactically correct.
        /// </summary>
        [TestMethod]
        public void ClientsTest2()
        {
            var repo = new OracleRepository<CLIENTS>();
            using (IDbConnection connection = repo.Open())
                connection.NonQuery("EXPLAIN PLAN FOR " + repo.InsertText);
        }

        /// <summary>
        /// This Test ensures the CLIENTS SQL text for updating is syntactically correct.
        /// </summary>
        [TestMethod]
        public void ClientsTest3()
        {
            var repo = new OracleRepository<CLIENTS>();
            using (IDbConnection connection = repo.Open())
                connection.NonQuery("EXPLAIN PLAN FOR " + repo.UpdateText);
        }


        /// <summary>
        /// This Test ensures the CLIENTS SQL text for deleting is syntactically correct.
        /// </summary>
        [TestMethod]
        public void ClientsTest4()
        {
            var repo = new OracleRepository<CLIENTS>();
            using (IDbConnection connection = repo.Open())
                connection.NonQuery("EXPLAIN PLAN FOR " + repo.DeleteText);
        }
    }
}
