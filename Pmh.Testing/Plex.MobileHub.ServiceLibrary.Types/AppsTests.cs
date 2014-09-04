using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pmh.ServiceLibrary;
using Pmh.ServiceLibrary.Types;
using System.Data;
using Plex.Data;
namespace Pmh.Testing.Pmh.ServiceLibrary.Types
{
    [TestClass]
    public class AppsTests
    {
        /// <summary>
        /// This Test ensures the APPS SQL text for selecting is syntactically correct.
        /// </summary>
        [TestMethod]
        public void AppsTest1()
        {
            var repo = new OracleRepository<APPS>();
            using (IDbConnection connection = repo.Open())
                connection.NonQuery("EXPLAIN PLAN FOR " + repo.SelectText);
        }
        /// <summary>
        /// This Test ensures the APPS SQL text for inserting is syntactically correct.
        /// </summary>
        [TestMethod]
        public void AppsTest2()
        {
            var repo = new OracleRepository<APPS>();
            using (IDbConnection connection = repo.Open())
                connection.NonQuery("EXPLAIN PLAN FOR " + repo.InsertText);
        }

        /// <summary>
        /// This Test ensures the APPS SQL text for updating is syntactically correct.
        /// </summary>
        [TestMethod]
        public void AppsTest3()
        {
            var repo = new OracleRepository<APPS>();
            using (IDbConnection connection = repo.Open())
                connection.NonQuery("EXPLAIN PLAN FOR " + repo.UpdateText);
        }


        /// <summary>
        /// This Test ensures the APPS SQL text for deleting is syntactically correct.
        /// </summary>
        [TestMethod]
        public void AppsTest4()
        {
            var repo = new OracleRepository<APPS>();
            using (IDbConnection connection = repo.Open())
                connection.NonQuery("EXPLAIN PLAN FOR " + repo.DeleteText);
        }
    }
}
