using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Plex.MobileHub.ServiceLibrary;
using Plex.MobileHub.ServiceLibrary.Types;
using System.Data;
using Plex.Data;
namespace Plex.MobileHub.Testing.Plex.MobileHub.ServiceLibrary.Types
{
    [TestClass]
    public class AppUserTypesTests
    {
        /// <summary>
        /// This Test ensures the APP_USER_TYPES SQL text for selecting is syntactically correct.
        /// </summary>
        [TestMethod]
        public void AppUserTypesTest1()
        {
            var repo = new OracleRepository<APP_USER_TYPES>();
            using (IDbConnection connection = repo.Open())
                connection.NonQuery("EXPLAIN PLAN FOR " + repo.SelectText);
        }
        /// <summary>
        /// This Test ensures the APP_USER_TYPES SQL text for inserting is syntactically correct.
        /// </summary>
        [TestMethod]
        public void AppUserTypesTest2()
        {
            var repo = new OracleRepository<APP_USER_TYPES>();
            using (IDbConnection connection = repo.Open())
                connection.NonQuery("EXPLAIN PLAN FOR " + repo.InsertText);
        }

        /// <summary>
        /// This Test ensures the APP_USER_TYPES SQL text for updating is syntactically correct.
        /// </summary>
        [TestMethod]
        public void AppUserTypesTest3()
        {
            var repo = new OracleRepository<APP_USER_TYPES>();
            using (IDbConnection connection = repo.Open())
                connection.NonQuery("EXPLAIN PLAN FOR " + repo.UpdateText);
        }


        /// <summary>
        /// This Test ensures the APP_USER_TYPES SQL text for deleting is syntactically correct.
        /// </summary>
        [TestMethod]
        public void AppUserTypesTest4()
        {
            var repo = new OracleRepository<APP_USER_TYPES>();
            using (IDbConnection connection = repo.Open())
                connection.NonQuery("EXPLAIN PLAN FOR " + repo.DeleteText);
        }
    }
}
