using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Plex.MobileHub.ServiceLibrary;
using Plex.MobileHub.ServiceLibrary.Types;
using System.Data;
using Plex.Data;
namespace Plex.MobileHub.Testing.Plex.MobileHub.ServiceLibrary.Types
{
    [TestClass]
    public class DevDataVerQueriesTests
    {
        /// <summary>
        /// This Test ensures the DEV_DATA_VER_QUERIES SQL text for selecting is syntactically correct.
        /// </summary>
        [TestMethod]
        public void DevDataVerQueriesTest1()
        {
            var repo = new OracleRepository<DEV_DATA_VER_QUERIES>();
            using (IDbConnection connection = repo.Open())
                connection.NonQuery("EXPLAIN PLAN FOR " + repo.SelectText);
        }
        /// <summary>
        /// This Test ensures the DEV_DATA_VER_QUERIES SQL text for inserting is syntactically correct.
        /// </summary>
        [TestMethod]
        public void DevDataVerQueriesTest2()
        {
            var repo = new OracleRepository<DEV_DATA_VER_QUERIES>();
            using (IDbConnection connection = repo.Open())
                connection.NonQuery("EXPLAIN PLAN FOR " + repo.InsertText);
        }

        /// <summary>
        /// This Test ensures the DEV_DATA_VER_QUERIES SQL text for updating is syntactically correct.
        /// </summary>
        [TestMethod]
        public void DevDataVerQueriesTest3()
        {
            var repo = new OracleRepository<DEV_DATA_VER_QUERIES>();
            using (IDbConnection connection = repo.Open())
                connection.NonQuery("EXPLAIN PLAN FOR " + repo.UpdateText);
        }


        /// <summary>
        /// This Test ensures the DEV_DATA_VER_QUERIES SQL text for deleting is syntactically correct.
        /// </summary>
        [TestMethod]
        public void DevDataVerQueriesTest4()
        {
            var repo = new OracleRepository<DEV_DATA_VER_QUERIES>();
            using (IDbConnection connection = repo.Open())
                connection.NonQuery("EXPLAIN PLAN FOR " + repo.DeleteText);
        }
    }
}
