using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pmh.ServiceLibrary;
using Pmh.ServiceLibrary.Types;
using System.Data;
using Plex.Data;
namespace Pmh.Testing.Pmh.ServiceLibrary.Types
{
    [TestClass]
    public class DevDataTests
    {
        /// <summary>
        /// This Test ensures the DEV_DATA SQL text for selecting is syntactically correct.
        /// </summary>
        [TestMethod]
        public void DevDataTest1()
        {
            var repo = new OracleRepository<DEV_DATA>();
            using (IDbConnection connection = repo.Open())
                connection.NonQuery("EXPLAIN PLAN FOR " + repo.SelectText);
        }
        /// <summary>
        /// This Test ensures the DEV_DATA SQL text for inserting is syntactically correct.
        /// </summary>
        [TestMethod]
        public void DevDataTest2()
        {
            var repo = new OracleRepository<DEV_DATA>();
            using (IDbConnection connection = repo.Open())
                connection.NonQuery("EXPLAIN PLAN FOR " + repo.InsertText);
        }

        /// <summary>
        /// This Test ensures the DEV_DATA SQL text for updating is syntactically correct.
        /// </summary>
        [TestMethod]
        public void DevDataTest3()
        {
            var repo = new OracleRepository<DEV_DATA>();
            using (IDbConnection connection = repo.Open())
                connection.NonQuery("EXPLAIN PLAN FOR " + repo.UpdateText);
        }


        /// <summary>
        /// This Test ensures the DEV_DATA SQL text for deleting is syntactically correct.
        /// </summary>
        [TestMethod]
        public void DevDataTest4()
        {
            var repo = new OracleRepository<DEV_DATA>();
            using (IDbConnection connection = repo.Open())
                connection.NonQuery("EXPLAIN PLAN FOR " + repo.DeleteText);
        }
    }
}
