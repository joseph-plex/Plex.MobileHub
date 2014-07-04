using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using Plex.MobileHub.Data;
using Plex.MobileHub.Data.Types;
using Plex.MobileHub.Data.Tables;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Plex.MobileHub.Test.Data
{
    [TestClass]
    public class TestGeneral
    {
        [TestMethod]
        [Description("Test to ensure that the connection string is working.")]
        public void TestConnection()
        {
            using (IDbConnection connection = Utilities.GetConnection(true))
            {
                Result result = connection.Query("select 1 as value from dual");
                Assert.AreEqual<int>(Convert.ToInt32(result.Rows.First()[0]), 1);
            }
        }

        [TestMethod]
        [Description("Test to ensure that all database tables habe an object rep")]
        public void TestTableRepresentation()
        {
            using (IDbConnection connection = Utilities.GetConnection(true))
            {
                List<String> tables = new List<String>();
                var columns = connection.Query("select TABLE_NAME, COLUMN_NAME from user_tab_columns"); 
                foreach (var table in connection.Query("select TABLE_NAME from user_tables")["TABLE_NAME"])
                    tables.Add(Convert.ToString(table));

                var TIndex = columns.GetColumnIndex("TABLE_NAME");
                var CIndex = columns.GetColumnIndex("COLUMN_NAME");

                Assembly _assembly = Assembly.GetAssembly(typeof(CLIENTS));
                var types = _assembly.GetTypes().ToList();
                types = _assembly.GetTypes().Where(p => string.Equals(p.Namespace, "Plex.MobileHub.Data.Tables", StringComparison.Ordinal)).ToList();

                foreach(var table in tables)
                {
                    var index = types.FindIndex(p => p.Name == table);
                    if (index == -1) throw new Exception("The Table " + table + " Does not have a variable representation");

                    foreach (var row in columns.Rows.FindAll(p => Convert.ToString(p[TIndex]) == table))
                        if (!types[index].GetProperties().Any(p => p.Name == Convert.ToString(row[CIndex])))
                            Assert.Fail("The Column " + row[CIndex] + " In Table " + table + " Does not have a variable representation");
                }
            }
        }
    }
}
