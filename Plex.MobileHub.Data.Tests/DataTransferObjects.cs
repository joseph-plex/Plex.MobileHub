using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Oracle.DataAccess.Client;
using System.Data;
using Plex.MobileHub.Data.Types;
namespace Plex.MobileHub.Data.Tests
{
    /// <summary>
    /// Summary description for DataTransferObjects
    /// </summary>
    [TestClass]
    public class DataTransferObjects
    {
        const string User = "C##PMH";
        const string Pass = "!!!plex!!!sa";
        const string Source = "(DESCRIPTION = (ADDRESS_LIST = (ADDRESS = (PROTOCOL = TCP)(HOST = 10.0.1.96)(PORT = 1521)))(CONNECT_DATA = (SERVICE_NAME = PLE1LIVE)))";
        const string ConnectionString = "User Id=" + User + ";" + "Password=" + Pass + ";" + "Data Source=" + Source + ";";

        [TestMethod]
        [Description("A simple Test to ensure everything is working")]
        public void SimpleTest()
        {
            using (IDbConnection connection = new OracleConnection(ConnectionString).OpenConnection())
                if ("X".Equals(connection.Query("select * from dual")[0, 0]))
                    Assert.IsTrue(true);
                else
                    Assert.Inconclusive();
     
        }

        [TestMethod]
        public void RepoEntrybaseParseTest()
        {
            List<APPS> list = new List<APPS>();
            using(IDbConnection connect = new OracleConnection(ConnectionString).OpenConnection())
                foreach (var c in connect.Query("SELECT * FROM APPS").Tuples)
                    list.Add(new APPS(c));
        }
    }
}
