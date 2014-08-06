using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Plex.MobileHub.ServiceLibrary;
using Plex.MobileHub.ServiceLibrary.Types;
using System.Data;
using Plex.Data;
namespace Plex.MobileHub.Testing.Plex.MobileHub.ServiceLibrary.Types
{
    [TestClass]
    public class AppTablesTests
    {
        /// <summary>
        /// This Test ensures the APP_TABLES SQL text for selecting is syntactically correct.
        /// </summary>
        [TestMethod]
        public void AppTablesTest1()
        {
            var repo = new OracleRepository<APP_TABLES>();
            using (IDbConnection connection = repo.Open())
                connection.NonQuery("EXPLAIN PLAN FOR " + repo.SelectText);
        }
        /// <summary>
        /// This Test ensures the APP_TABLES SQL text for inserting is syntactically correct.
        /// </summary>
        [TestMethod]
        public void AppTablesTest2()
        {
            var repo = new OracleRepository<APP_TABLES>();
            using (IDbConnection connection = repo.Open())
                connection.NonQuery("EXPLAIN PLAN FOR " + repo.InsertText);
        }

        /// <summary>
        /// This Test ensures the APP_TABLES SQL text for updating is syntactically correct.
        /// </summary>
        [TestMethod]
        public void AppTablesTest3()
        {
            var repo = new OracleRepository<APP_TABLES>();
            using (IDbConnection connection = repo.Open())
                connection.NonQuery("EXPLAIN PLAN FOR " + repo.UpdateText);
        }


        /// <summary>
        /// This Test ensures the APP_TABLES SQL text for deleting is syntactically correct.
        /// </summary>
        [TestMethod]
        public void AppTablesTest4()
        {
            var repo = new OracleRepository<APP_TABLES>();
            using (IDbConnection connection = repo.Open())
                connection.NonQuery("EXPLAIN PLAN FOR " + repo.DeleteText);
        }
    }
}
