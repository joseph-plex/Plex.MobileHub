﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Plex.MobileHub.ServiceLibrary;
using Plex.MobileHub.ServiceLibrary.Types;
using System.Data;
using Plex.Data;
namespace Plex.MobileHub.Testing.Plex.MobileHub.ServiceLibrary.Types
{
    [TestClass]
    public class LogsTests
    {
        /// <summary>
        /// This Test ensures the LOGS SQL text for selecting is syntactically correct.
        /// </summary>
        [TestMethod]
        public void LogsTest1()
        {
            var repo = new OracleRepository<LOGS>();
            using (IDbConnection connection = repo.Open())
                connection.NonQuery("EXPLAIN PLAN FOR " + repo.SelectText);
        }
        /// <summary>
        /// This Test ensures the LOGS SQL text for inserting is syntactically correct.
        /// </summary>
        [TestMethod]
        public void LogsTest2()
        {
            var repo = new OracleRepository<LOGS>();
            using (IDbConnection connection = repo.Open())
                connection.NonQuery("EXPLAIN PLAN FOR " + repo.InsertText);
        }

        /// <summary>
        /// This Test ensures the LOGS SQL text for updating is syntactically correct.
        /// </summary>
        [TestMethod]
        public void LogsTest3()
        {
            var repo = new OracleRepository<LOGS>();
            using (IDbConnection connection = repo.Open())
                connection.NonQuery("EXPLAIN PLAN FOR " + repo.UpdateText);
        }


        /// <summary>
        /// This Test ensures the LOGS SQL text for deleting is syntactically correct.
        /// </summary>
        [TestMethod]
        public void LogsTest4()
        {
            var repo = new OracleRepository<LOGS>();
            using (IDbConnection connection = repo.Open())
                connection.NonQuery("EXPLAIN PLAN FOR " + repo.DeleteText);
        }
    }
}
