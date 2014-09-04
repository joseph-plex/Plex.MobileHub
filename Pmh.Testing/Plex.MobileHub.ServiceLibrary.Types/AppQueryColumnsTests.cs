using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pmh.ServiceLibrary;
using Pmh.ServiceLibrary.Types;
using System.Data;
using Plex.Data;
namespace Pmh.Testing.Pmh.ServiceLibrary.Types
{
    [TestClass]
    public class AppQueryColumnsTests
    {
        /// <summary>
        /// This Test ensures the APP_QUERY_COLUMNS SQL text for selecting is syntactically correct.
        /// </summary>
        [TestMethod]
        public void AppQueryColumnsTest1()
        {
            var repo = new OracleRepository<APP_QUERY_COLUMNS>();
            using (IDbConnection connection = repo.Open())
                connection.NonQuery("EXPLAIN PLAN FOR " + repo.SelectText);
        }
        /// <summary>
        /// This Test ensures the APP_QUERY_COLUMNS SQL text for inserting is syntactically correct.
        /// </summary>
        [TestMethod]
        public void AppQueryColumnsTest2()
        {
            var repo = new OracleRepository<APP_QUERY_COLUMNS>();
            using (IDbConnection connection = repo.Open())
                connection.NonQuery("EXPLAIN PLAN FOR " + repo.InsertText);
        }

        /// <summary>
        /// This Test ensures the APP_QUERY_COLUMNS SQL text for updating is syntactically correct.
        /// </summary>
        [TestMethod]
        public void AppQueryColumnsTest3()
        {
            var repo = new OracleRepository<APP_QUERY_COLUMNS>();
            using (IDbConnection connection = repo.Open())
                connection.NonQuery("EXPLAIN PLAN FOR " + repo.UpdateText);
        }


        /// <summary>
        /// This Test ensures the APP_QUERY_COLUMNS SQL text for deleting is syntactically correct.
        /// </summary>
        [TestMethod]
        public void AppQueryColumnsTest4()
        {
            var repo = new OracleRepository<APP_QUERY_COLUMNS>();
            using (IDbConnection connection = repo.Open())
                connection.NonQuery("EXPLAIN PLAN FOR " + repo.DeleteText);
        }
    }
}
