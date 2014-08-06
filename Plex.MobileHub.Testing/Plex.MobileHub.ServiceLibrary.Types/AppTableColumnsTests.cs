using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Plex.MobileHub.ServiceLibrary;
using Plex.MobileHub.ServiceLibrary.Types;
using System.Data;
using Plex.Data;
namespace Plex.MobileHub.Testing.Plex.MobileHub.ServiceLibrary.Types
{
    [TestClass]
    public class AppTableColumnsTests
    {
        /// <summary>
        /// This Test ensures the APP_TABLE_COLUMNS SQL text for selecting is syntactically correct.
        /// </summary>
        [TestMethod]
        public void AppTableColumnsTest1()
        {
            var repo = new OracleRepository<APP_TABLE_COLUMNS>();
            using (IDbConnection connection = repo.Open())
                connection.NonQuery("EXPLAIN PLAN FOR " + repo.SelectText);
        }
        /// <summary>
        /// This Test ensures the APP_TABLE_COLUMNS SQL text for inserting is syntactically correct.
        /// </summary>
        [TestMethod]
        public void AppTableColumnsTest2()
        {
            var repo = new OracleRepository<APP_TABLE_COLUMNS>();
            using (IDbConnection connection = repo.Open())
                connection.NonQuery("EXPLAIN PLAN FOR " + repo.InsertText);
        }

        /// <summary>
        /// This Test ensures the APP_TABLE_COLUMNS SQL text for updating is syntactically correct.
        /// </summary>
        [TestMethod]
        public void AppTableColumnsTest3()
        {
            var repo = new OracleRepository<APP_TABLE_COLUMNS>();
            using (IDbConnection connection = repo.Open())
                connection.NonQuery("EXPLAIN PLAN FOR " + repo.UpdateText);
        }


        /// <summary>
        /// This Test ensures the APP_TABLE_COLUMNS SQL text for deleting is syntactically correct.
        /// </summary>
        [TestMethod]
        public void AppTableColumnsTest4()
        {
            var repo = new OracleRepository<APP_TABLE_COLUMNS>();
            using (IDbConnection connection = repo.Open())
                connection.NonQuery("EXPLAIN PLAN FOR " + repo.DeleteText);
        }
    }
}
