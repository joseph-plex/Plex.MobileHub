using System;
using System.Data;
using System.Linq;
using Plex.MobileHub.Data;
using Plex.MobileHub.Data.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Plex.MobileHub.Test.Data
{
    [TestClass]
    public class TestGeneral
    {
        /// <summary>
        /// Test to ensure that the connection string is working.
        /// </summary>
        [TestMethod]
        public void TestConnection()
        {
            using (IDbConnection connection = Utilities.GetConnection(true))
            {
                Result result = connection.Query("select 1 as value from dual");
                Assert.AreEqual<int>(Convert.ToInt32(result.Rows.First()[0]), 1);
            }
        }
    }
}
