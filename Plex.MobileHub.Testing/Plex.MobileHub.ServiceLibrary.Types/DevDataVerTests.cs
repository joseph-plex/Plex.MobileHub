using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Plex.MobileHub.ServiceLibrary;
using Plex.MobileHub.ServiceLibrary.Types;
using System.Data;
using Plex.Data;
namespace Plex.MobileHub.Testing.Plex.MobileHub.ServiceLibrary.Types
{
    [TestClass]
    public class DevDataVerTests
    {
        /// <summary>
        /// This Test ensures the DEV_DATA_VER SQL text for selecting is syntactically correct.
        /// </summary>
        [TestMethod]
        public void DevDataVerTest1()
        {
            var repo = new OracleRepository<DEV_DATA_VER>();
            using (IDbConnection connection = repo.Open())
                connection.NonQuery("EXPLAIN PLAN FOR " + repo.SelectText);
        }
        /// <summary>
        /// This Test ensures the DEV_DATA_VER SQL text for inserting is syntactically correct.
        /// </summary>
        [TestMethod]
        public void DevDataVerTest2()
        {
            var repo = new OracleRepository<DEV_DATA_VER>();
            using (IDbConnection connection = repo.Open())
                connection.NonQuery("EXPLAIN PLAN FOR " + repo.InsertText);
        }

        /// <summary>
        /// This Test ensures the DEV_DATA_VER SQL text for updating is syntactically correct.
        /// </summary>
        [TestMethod]
        public void DevDataVerTest3()
        {
            var repo = new OracleRepository<DEV_DATA_VER>();
            using (IDbConnection connection = repo.Open())
                connection.NonQuery("EXPLAIN PLAN FOR " + repo.UpdateText);
        }


        /// <summary>
        /// This Test ensures the DEV_DATA_VER SQL text for deleting is syntactically correct.
        /// </summary>
        [TestMethod]
        public void DevDataVerTest4()
        {
            var repo = new OracleRepository<DEV_DATA_VER>();
            using (IDbConnection connection = repo.Open())
                connection.NonQuery("EXPLAIN PLAN FOR " + repo.DeleteText);
        }
    }
}
