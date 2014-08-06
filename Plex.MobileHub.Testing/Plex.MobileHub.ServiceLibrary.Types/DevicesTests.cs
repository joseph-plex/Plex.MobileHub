using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Plex.MobileHub.ServiceLibrary;
using Plex.MobileHub.ServiceLibrary.Types;
using System.Data;
using Plex.Data;
namespace Plex.MobileHub.Testing.Plex.MobileHub.ServiceLibrary.Types
{
    [TestClass]
    public class DevicesTests
    {
        /// <summary>
        /// This Test ensures the DEVICES SQL text for selecting is syntactically correct.
        /// </summary>
        [TestMethod]
        public void DevicesTest1()
        {
            var repo = new OracleRepository<DEVICES>();
            using (IDbConnection connection = repo.Open())
                connection.NonQuery("EXPLAIN PLAN FOR " + repo.SelectText);
        }
        /// <summary>
        /// This Test ensures the DEVICES SQL text for inserting is syntactically correct.
        /// </summary>
        [TestMethod]
        public void DevicesTest2()
        {
            var repo = new OracleRepository<DEVICES>();
            using (IDbConnection connection = repo.Open())
                connection.NonQuery("EXPLAIN PLAN FOR " + repo.InsertText);
        }

        /// <summary>
        /// This Test ensures the DEVICES SQL text for updating is syntactically correct.
        /// </summary>
        [TestMethod]
        public void DevicesTest3()
        {
            var repo = new OracleRepository<DEVICES>();
            using (IDbConnection connection = repo.Open())
                connection.NonQuery("EXPLAIN PLAN FOR " + repo.UpdateText);
        }


        /// <summary>
        /// This Test ensures the DEVICES SQL text for deleting is syntactically correct.
        /// </summary>
        [TestMethod]
        public void DevicesTest4()
        {
            var repo = new OracleRepository<DEVICES>();
            using (IDbConnection connection = repo.Open())
                connection.NonQuery("EXPLAIN PLAN FOR " + repo.DeleteText);
        }
    }
}
