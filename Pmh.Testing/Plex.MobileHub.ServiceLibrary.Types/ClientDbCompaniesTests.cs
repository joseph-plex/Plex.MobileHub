using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pmh.ServiceLibrary;
using Pmh.ServiceLibrary.Types;
using System.Data;
using Plex.Data;
namespace Pmh.Testing.Pmh.ServiceLibrary.Types
{
    [TestClass]
    public class ClientDbCompaniesTests
    {   /// <summary>
        /// This Test ensures the CLIENT_DB_COMPANIES SQL text for selecting is syntactically correct.
        /// </summary>
        [TestMethod]
        public void ClientDbCompaniesTest1()
        {
            var repo = new OracleRepository<CLIENT_DB_COMPANIES>();
            using (IDbConnection connection = repo.Open())
                connection.NonQuery("EXPLAIN PLAN FOR " + repo.SelectText);
        }
        /// <summary>
        /// This Test ensures the CLIENT_DB_COMPANIES SQL text for inserting is syntactically correct.
        /// </summary>
        [TestMethod]
        public void ClientDbCompaniesTest2()
        {
            var repo = new OracleRepository<CLIENT_DB_COMPANIES>();
            using (IDbConnection connection = repo.Open())
                connection.NonQuery("EXPLAIN PLAN FOR " + repo.InsertText);
        }

        /// <summary>
        /// This Test ensures the CLIENT_DB_COMPANIES SQL text for updating is syntactically correct.
        /// </summary>
        [TestMethod]
        public void ClientDbCompaniesTest3()
        {
            var repo = new OracleRepository<CLIENT_DB_COMPANIES>();
            using (IDbConnection connection = repo.Open())
                connection.NonQuery("EXPLAIN PLAN FOR " + repo.UpdateText);
        }


        /// <summary>
        /// This Test ensures the CLIENT_DB_COMPANIES SQL text for deleting is syntactically correct.
        /// </summary>
        [TestMethod]
        public void ClientDbCompaniesTest4()
        {
            var repo = new OracleRepository<CLIENT_DB_COMPANIES>();
            using (IDbConnection connection = repo.Open())
                connection.NonQuery("EXPLAIN PLAN FOR " + repo.DeleteText);
        }
    }
}
