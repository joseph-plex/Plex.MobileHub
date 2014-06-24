using System;
//using System.da
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Plex.MobileHub.ServiceLibraries.Repositories;
using Plex.MobileHub.Data;
using Plex.MobileHub.Data.Types;
using Oracle.DataAccess.Client;
namespace Plex.MobileHub.ServiceLibraries.Test
{
    [TestClass]
    public class OracleRepositoryTest
    {

        [TestMethod]
        [ExpectedException(typeof(OracleException))]
        [Description("We want to ensure that if the query is invalid NonQuery will throw exceptions for invalid queries")]
        public void OracleRepositoryFailTest()
        {
            OracleRepository<APPS> Repo = new OracleRepository<APPS>();
            using (var connection = Repo.GetConnection())
                connection.NonQuery("EXPLAIN PLAN FOR (select * from chickenmonkey");
        }

        [TestMethod]
        [Timeout(1000)]
        [Description("Ensure that the generated SQL is valid and can be generated quickly.")]
        public void APPS_OracleRepoSqlTest()
        {
            OracleRepository<APPS> Repo = new OracleRepository<APPS>();
            using(var connection = Repo.GetConnection())
            {
                // As long as the query syntax is valid these should not throw an exception.
                connection.NonQuery("Explain Plan for " + Repo.InsertText);
                connection.NonQuery("Explain Plan for " + Repo.UpdateText);
                connection.NonQuery("Explain Plan for " + Repo.SelectText);
                connection.NonQuery("Explain Plan for " + Repo.DeleteText);
            }
        }


    }
}
