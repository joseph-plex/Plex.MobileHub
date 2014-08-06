using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Plex.MobileHub.ServiceLibrary;
using Plex.MobileHub.ServiceLibrary.Types;
using System.Data;
using Plex.Data;
namespace Plex.MobileHub.Testing.Plex.MobileHub.ServiceLibrary.Types
{
    [TestClass]
    public class DeviceUserDataTests
    {
        /// <summary>
        /// This Test ensures the DEVICE_USER_DATA SQL text for selecting is syntactically correct.
        /// </summary>
        [TestMethod]
        public void DeviceUserDataTest1()
        {
            var repo = new OracleRepository<DEVICE_USER_DATA>();
            using (IDbConnection connection = repo.Open())
                connection.NonQuery("EXPLAIN PLAN FOR " + repo.SelectText);
        }
        /// <summary>
        /// This Test ensures the DEVICE_USER_DATA SQL text for inserting is syntactically correct.
        /// </summary>
        [TestMethod]
        public void DeviceUserDataTest2()
        {
            var repo = new OracleRepository<DEVICE_USER_DATA>();
            using (IDbConnection connection = repo.Open())
                connection.NonQuery("EXPLAIN PLAN FOR " + repo.InsertText);
        }

        /// <summary>
        /// This Test ensures the DEVICE_USER_DATA SQL text for updating is syntactically correct.
        /// </summary>
        [TestMethod]
        public void DeviceUserDataTest3()
        {
            var repo = new OracleRepository<DEVICE_USER_DATA>();
            using (IDbConnection connection = repo.Open())
                connection.NonQuery("EXPLAIN PLAN FOR " + repo.UpdateText);
        }


        /// <summary>
        /// This Test ensures the DEVICE_USER_DATA SQL text for deleting is syntactically correct.
        /// </summary>
        [TestMethod]
        public void DeviceUserDataTest4()
        {
            var repo = new OracleRepository<DEVICE_USER_DATA>();
            using (IDbConnection connection = repo.Open())
                connection.NonQuery("EXPLAIN PLAN FOR " + repo.DeleteText);
        }
    }
}
