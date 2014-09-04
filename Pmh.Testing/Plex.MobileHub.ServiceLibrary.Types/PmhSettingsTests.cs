using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pmh.ServiceLibrary;
using Pmh.ServiceLibrary.Types;
using System.Data;
using Plex.Data;
namespace Pmh.Testing.Pmh.ServiceLibrary.Types
{
    [TestClass]
    public class PmhSettingsTests
    {
        /// <summary>
        /// This Test ensures the PMH_SETTINGS SQL text for selecting is syntactically correct.
        /// </summary>
        [TestMethod]
        public void PmhSettingsTest1()
        {
            var repo = new OracleRepository<PMH_SETTINGS>();
            using (IDbConnection connection = repo.Open())
                connection.NonQuery("EXPLAIN PLAN FOR " + repo.SelectText);
        }
        /// <summary>
        /// This Test ensures the PMH_SETTINGS SQL text for inserting is syntactically correct.
        /// </summary>
        [TestMethod]
        public void PmhSettingsTest2()
        {
            var repo = new OracleRepository<PMH_SETTINGS>();
            using (IDbConnection connection = repo.Open())
                connection.NonQuery("EXPLAIN PLAN FOR " + repo.InsertText);
        }

        /// <summary>
        /// This Test ensures the PMH_SETTINGS SQL text for updating is syntactically correct.
        /// </summary>
        [TestMethod]
        public void PmhSettingsTest3()
        {
            var repo = new OracleRepository<PMH_SETTINGS>();
            using (IDbConnection connection = repo.Open())
                connection.NonQuery("EXPLAIN PLAN FOR " + repo.UpdateText);
        }


        /// <summary>
        /// This Test ensures the PMH_SETTINGS SQL text for deleting is syntactically correct.
        /// </summary>
        [TestMethod]
        public void PmhSettingsTest4()
        {
            var repo = new OracleRepository<PMH_SETTINGS>();
            using (IDbConnection connection = repo.Open())
                connection.NonQuery("EXPLAIN PLAN FOR " + repo.DeleteText);
        }
    }
}
