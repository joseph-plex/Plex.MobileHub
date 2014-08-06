using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Plex.MobileHub.ServiceLibrary;
using Plex.MobileHub.ServiceLibrary.Types;
using System.Data;
using Plex.Data;
namespace Plex.MobileHub.Testing.Plex.MobileHub.ServiceLibrary.Types
{
    [TestClass]
    public class QuerySequenceRequestsTests
    {
        /// <summary>
        /// This Test ensures the QUERY_SEQUENCE_REQUESTS SQL text for selecting is syntactically correct.
        /// </summary>
        [TestMethod]
        public void QuerySequenceRequestsTest1()
        {
            var repo = new OracleRepository<QUERY_SEQUENCE_REQUESTS>();
            using (IDbConnection connection = repo.Open())
                connection.NonQuery("EXPLAIN PLAN FOR " + repo.SelectText);
        }
        /// <summary>
        /// This Test ensures the QUERY_SEQUENCE_REQUESTS SQL text for inserting is syntactically correct.
        /// </summary>
        [TestMethod]
        public void QuerySequenceRequestsTest2()
        {
            var repo = new OracleRepository<QUERY_SEQUENCE_REQUESTS>();
            using (IDbConnection connection = repo.Open())
                connection.NonQuery("EXPLAIN PLAN FOR " + repo.InsertText);
        }

        /// <summary>
        /// This Test ensures the QUERY_SEQUENCE_REQUESTS SQL text for updating is syntactically correct.
        /// </summary>
        [TestMethod]
        public void QuerySequenceRequestsTest3()
        {
            var repo = new OracleRepository<QUERY_SEQUENCE_REQUESTS>();
            using (IDbConnection connection = repo.Open())
                connection.NonQuery("EXPLAIN PLAN FOR " + repo.UpdateText);
        }


        /// <summary>
        /// This Test ensures the QUERY_SEQUENCE_REQUESTS SQL text for deleting is syntactically correct.
        /// </summary>
        [TestMethod]
        public void QuerySequenceRequestsTest4()
        {
            var repo = new OracleRepository<QUERY_SEQUENCE_REQUESTS>();
            using (IDbConnection connection = repo.Open())
                connection.NonQuery("EXPLAIN PLAN FOR " + repo.DeleteText);
        }
    }
}
