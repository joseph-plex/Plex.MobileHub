using System;
using System.Data;
using System.Linq;
using Plex.MobileHub.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Oracle.DataAccess.Client;
using Plex.MobileHub.Data.Types;
using System.Reflection;
using System.Collections.Generic;
namespace Plex.MobileHub.Data.Tests
{
    [TestClass]
    public class DataIntegrity
    {
        const string User = "C##PMH";
        const string Pass = "!!!plex!!!sa";
        const string Source = "(DESCRIPTION = (ADDRESS_LIST = (ADDRESS = (PROTOCOL = TCP)(HOST = 10.0.1.96)(PORT = 1521)))(CONNECT_DATA = (SERVICE_NAME = PLE1LIVE)))";
        const string ConnectionString = "User Id=" + User + ";" + "Password=" + Pass + ";" + "Data Source=" + Source + ";";

        [TestMethod]
        [Description("Test to ensure that the connection string is working.")]
        public void TestConnection()
        {
            using (IDbConnection connection = new OracleConnection(ConnectionString).OpenConnection())
            {
                PlexQueryResult result = connection.Query("select 1 as value from dual");
                Assert.AreEqual<int>(Convert.ToInt32(result.Tuples.First()[0]), 1);
            }
        }


        [TestMethod]
        [Description("Test to ensure that all database tables habe an object rep")]
        public void TestTableRepresentation()
        {
            using (IDbConnection connection = new OracleConnection(ConnectionString).OpenConnection())
            {
                List<String> tables = new List<String>();
                var columns = connection.Query("select TABLE_NAME, COLUMN_NAME from user_tab_columns");
                foreach (var table in connection.Query("select TABLE_NAME from user_tables")["TABLE_NAME"])
                    tables.Add(Convert.ToString(table));

                var TIndex = columns.GetColumnIndex("TABLE_NAME");
                var CIndex = columns.GetColumnIndex("COLUMN_NAME");

                Assembly _assembly = Assembly.GetAssembly(typeof(CLIENTS));
                var types = _assembly.GetTypes().ToList();

                types = _assembly.GetTypes().Where(p => string.Equals(p.Namespace, typeof(CLIENTS).Namespace, StringComparison.Ordinal)).ToList();

                foreach (var table in tables)
                {
                    var index = types.FindIndex(p => p.Name == table);
                    if (index == -1) throw new Exception("The Table " + table + " Does not have a variable representation");

                    foreach (var row in columns.Tuples.Where(p => Convert.ToString(p[TIndex]) == table))
                        if (!types[index].GetProperties().Any(p => p.Name == Convert.ToString(row[CIndex])))
                            Assert.Fail("The Column " + row[CIndex] + " In Table " + table + " Does not have a variable representation");
                }
            }
        }
    }
}
