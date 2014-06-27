using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Plex.MobileHub.ServiceLibraries;
using Plex.MobileHub.Data;
using Plex.MobileHub.Data.Types;
using Oracle.DataAccess.Client;

namespace Plex.MobileHub.ServiceLibraries.Test.General
{
    [TestClass]
    public class OracleRepositorySqlTests
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
        public void APP_QUERIES_OracleRepoSqlTest()
        {
            OracleRepository<APP_QUERIES> Repo = new OracleRepository<APP_QUERIES>();
            using (var connection = Repo.GetConnection())
            {
                //As long as the query syntax is valid these should not throw an exception.
                connection.NonQuery("Explain Plan for " + Repo.InsertText);
                connection.NonQuery("Explain Plan for " + Repo.UpdateText);
                connection.NonQuery("Explain Plan for " + Repo.SelectText);
                connection.NonQuery("Explain Plan for " + Repo.DeleteText);
            }
        }

        [TestMethod]
        [Timeout(1000)]
        [Description("Ensure that the generated SQL is valid and can be generated quickly.")]
        public void APP_QUERY_COLUMNS_OracleRepoSqlTest()
        {
            OracleRepository<APP_QUERY_COLUMNS> Repo = new OracleRepository<APP_QUERY_COLUMNS>();
            using (var connection = Repo.GetConnection())
            {
                // As long as the query syntax is valid these should not throw an exception.
                connection.NonQuery("Explain Plan for " + Repo.InsertText);
                connection.NonQuery("Explain Plan for " + Repo.UpdateText);
                connection.NonQuery("Explain Plan for " + Repo.SelectText);
                connection.NonQuery("Explain Plan for " + Repo.DeleteText);
            }
        }

        [TestMethod]
        [Timeout(1000)]
        [Description("Ensure that the generated SQL is valid and can be generated quickly.")]
        public void APP_QUERY_CONDITIONS_OracleRepoSqlTest()
        {
            OracleRepository<APP_QUERY_CONDITIONS> Repo = new OracleRepository<APP_QUERY_CONDITIONS>();
            using (var connection = Repo.GetConnection())
            {
                // As long as the query syntax is valid these should not throw an exception.
                connection.NonQuery("Explain Plan for " + Repo.InsertText);
                connection.NonQuery("Explain Plan for " + Repo.UpdateText);
                connection.NonQuery("Explain Plan for " + Repo.SelectText);
                connection.NonQuery("Explain Plan for " + Repo.DeleteText);
            }
        }

        [TestMethod]
        //[Timeout(1000)]
        [Description("Ensure that the generated SQL is valid and can be generated quickly.")]
        public void APP_TABLE_COLUMNS_OracleRepoSqlTest()
        {
            OracleRepository<APP_TABLE_COLUMNS> Repo = new OracleRepository<APP_TABLE_COLUMNS>();
            using (var connection = Repo.GetConnection())
            {
                // As long as the query syntax is valid these should not throw an exception.
                connection.NonQuery("Explain Plan for " + Repo.InsertText);
                connection.NonQuery("Explain Plan for " + Repo.UpdateText);
                connection.NonQuery("Explain Plan for " + Repo.SelectText);
                connection.NonQuery("Explain Plan for " + Repo.DeleteText);
            }
        }

        [TestMethod]
        [Timeout(1000)]
        [Description("Ensure that the generated SQL is valid and can be generated quickly.")]
        public void APP_TABLES_OracleRepoSqlTest()
        {
            OracleRepository<APP_TABLES> Repo = new OracleRepository<APP_TABLES>();
            using (var connection = Repo.GetConnection())
            {
                // As long as the query syntax is valid these should not throw an exception.
                connection.NonQuery("Explain Plan for " + Repo.InsertText);
                connection.NonQuery("Explain Plan for " + Repo.UpdateText);
                connection.NonQuery("Explain Plan for " + Repo.SelectText);
                connection.NonQuery("Explain Plan for " + Repo.DeleteText);
            }
        }

        [TestMethod]
        [Timeout(1000)]
        [Description("Ensure that the generated SQL is valid and can be generated quickly.")]
        public void APP_USER_TYPES_OracleRepoSqlTest()
        {
            OracleRepository<APP_USER_TYPES> Repo = new OracleRepository<APP_USER_TYPES>();
            using (var connection = Repo.GetConnection())
            {
                // As long as the query syntax is valid these should not throw an exception.
                connection.NonQuery("Explain Plan for " + Repo.InsertText);
                connection.NonQuery("Explain Plan for " + Repo.UpdateText);
                connection.NonQuery("Explain Plan for " + Repo.SelectText);
                connection.NonQuery("Explain Plan for " + Repo.DeleteText);
            }
        }
 
        [TestMethod]
        [Timeout(1000)]
        [Description("Ensure that the generated SQL is valid and can be generated quickly.")]
        public void CLIENT_APPS_OracleRepoSqlTest()
        {
            OracleRepository<CLIENT_APPS> Repo = new OracleRepository<CLIENT_APPS>();
            using (var connection = Repo.GetConnection())
            {
                // As long as the query syntax is valid these should not throw an exception.
                connection.NonQuery("Explain Plan for " + Repo.InsertText);
                connection.NonQuery("Explain Plan for " + Repo.UpdateText);
                connection.NonQuery("Explain Plan for " + Repo.SelectText);
                connection.NonQuery("Explain Plan for " + Repo.DeleteText);
            }
        }

        [TestMethod]
        [Timeout(1000)]
        [Description("Ensure that the generated SQL is valid and can be generated quickly.")]
        public void CLIENT_DB_COMPANIES_OracleRepoSqlTest()
        {
            OracleRepository<CLIENT_DB_COMPANIES> Repo = new OracleRepository<CLIENT_DB_COMPANIES>();
            using (var connection = Repo.GetConnection())
            {
                // As long as the query syntax is valid these should not throw an exception.
                connection.NonQuery("Explain Plan for " + Repo.InsertText);
                connection.NonQuery("Explain Plan for " + Repo.UpdateText);
                connection.NonQuery("Explain Plan for " + Repo.SelectText);
                connection.NonQuery("Explain Plan for " + Repo.DeleteText);
            }
        }

        [TestMethod]
        [Timeout(1000)]
        [Description("Ensure that the generated SQL is valid and can be generated quickly.")]
        public void CLIENT_DB_COMPANY_USER_APPS_OracleRepoSqlTest()
        {
            OracleRepository<CLIENT_DB_COMPANY_USER_APPS> Repo = new OracleRepository<CLIENT_DB_COMPANY_USER_APPS>();
            using (var connection = Repo.GetConnection())
            {
                // As long as the query syntax is valid these should not throw an exception.
                connection.NonQuery("Explain Plan for " + Repo.InsertText);
                connection.NonQuery("Explain Plan for " + Repo.UpdateText);
                connection.NonQuery("Explain Plan for " + Repo.SelectText);
                connection.NonQuery("Explain Plan for " + Repo.DeleteText);
            }
        }

        [TestMethod]
        [Timeout(1000)]
        [Description("Ensure that the generated SQL is valid and can be generated quickly.")]
        public void CLIENT_DB_COMPANY_USERS_OracleRepoSqlTest()
        {
            OracleRepository<CLIENT_DB_COMPANY_USERS> Repo = new OracleRepository<CLIENT_DB_COMPANY_USERS>();
            using (var connection = Repo.GetConnection())
            {
                // As long as the query syntax is valid these should not throw an exception.
                connection.NonQuery("Explain Plan for " + Repo.InsertText);
                connection.NonQuery("Explain Plan for " + Repo.UpdateText);
                connection.NonQuery("Explain Plan for " + Repo.SelectText);
                connection.NonQuery("Explain Plan for " + Repo.DeleteText);
            }
        }

        [TestMethod]
        [Timeout(1000)]
        [Description("Ensure that the generated SQL is valid and can be generated quickly.")]
        public void CLIENT_USERS_OracleRepoSqlTest()
        {
            OracleRepository<CLIENT_USERS> Repo = new OracleRepository<CLIENT_USERS>();
            using (var connection = Repo.GetConnection())
            {
                // As long as the query syntax is valid these should not throw an exception.
                connection.NonQuery("Explain Plan for " + Repo.InsertText);
                connection.NonQuery("Explain Plan for " + Repo.UpdateText);
                connection.NonQuery("Explain Plan for " + Repo.SelectText);
                connection.NonQuery("Explain Plan for " + Repo.DeleteText);
            }
        }

        [TestMethod]
        [Timeout(1000)]
        [Description("Ensure that the generated SQL is valid and can be generated quickly.")]
        public void CLIENTS_OracleRepoSqlTest()
        {
            OracleRepository<CLIENTS> Repo = new OracleRepository<CLIENTS>();
            using (var connection = Repo.GetConnection())
            {
                // As long as the query syntax is valid these should not throw an exception.
                connection.NonQuery("Explain Plan for " + Repo.InsertText);
                connection.NonQuery("Explain Plan for " + Repo.UpdateText);
                connection.NonQuery("Explain Plan for " + Repo.SelectText);
                connection.NonQuery("Explain Plan for " + Repo.DeleteText);
            }
        }

        [TestMethod]
        [Timeout(1000)]
        [Description("Ensure that the generated SQL is valid and can be generated quickly.")]
        public void DEV_DATA_OracleRepoSqlTest()
        {
            OracleRepository<DEV_DATA> Repo = new OracleRepository<DEV_DATA>();
            using (var connection = Repo.GetConnection())
            {
                // As long as the query syntax is valid these should not throw an exception.
                connection.NonQuery("Explain Plan for " + Repo.InsertText);
                connection.NonQuery("Explain Plan for " + Repo.UpdateText);
                connection.NonQuery("Explain Plan for " + Repo.SelectText);
                connection.NonQuery("Explain Plan for " + Repo.DeleteText);
            }
        }

        [TestMethod]
        [Timeout(1000)]
        [Description("Ensure that the generated SQL is valid and can be generated quickly.")]
        public void DEV_DATA_VER_OracleRepoSqlTest()
        {
            OracleRepository<DEV_DATA_VER> Repo = new OracleRepository<DEV_DATA_VER>();
            using (var connection = Repo.GetConnection())
            {
                // As long as the query syntax is valid these should not throw an exception.
                connection.NonQuery("Explain Plan for " + Repo.InsertText);
                connection.NonQuery("Explain Plan for " + Repo.UpdateText);
                connection.NonQuery("Explain Plan for " + Repo.SelectText);
                connection.NonQuery("Explain Plan for " + Repo.DeleteText);
            }
        }

        [TestMethod]
        [Timeout(1000)]
        [Description("Ensure that the generated SQL is valid and can be generated quickly.")]
        public void DEV_DATA_VER_QUERIES_OracleRepoSqlTest()
        {
            OracleRepository<DEV_DATA_VER_QUERIES> Repo = new OracleRepository<DEV_DATA_VER_QUERIES>();
            using (var connection = Repo.GetConnection())
            {
                // As long as the query syntax is valid these should not throw an exception.
                connection.NonQuery("Explain Plan for " + Repo.InsertText);
                connection.NonQuery("Explain Plan for " + Repo.UpdateText);
                connection.NonQuery("Explain Plan for " + Repo.SelectText);
                connection.NonQuery("Explain Plan for " + Repo.DeleteText);
            }
        }

        [TestMethod]
        [Timeout(1000)]
        [Description("Ensure that the generated SQL is valid and can be generated quickly.")]
        public void DEVICE_USER_DATA_OracleRepoSqlTest()
        {
            OracleRepository<DEVICE_USER_DATA> Repo = new OracleRepository<DEVICE_USER_DATA>();
            using (var connection = Repo.GetConnection())
            {
                // As long as the query syntax is valid these should not throw an exception.
                connection.NonQuery("Explain Plan for " + Repo.InsertText);
                connection.NonQuery("Explain Plan for " + Repo.UpdateText);
                connection.NonQuery("Explain Plan for " + Repo.SelectText);
                connection.NonQuery("Explain Plan for " + Repo.DeleteText);
            }
        }

        [TestMethod]
        [Timeout(1000)]
        [Description("Ensure that the generated SQL is valid and can be generated quickly.")]
        public void DEVICE_USER_DATA_QUERIES_OracleRepoSqlTest()
        {
            OracleRepository<DEVICE_USER_DATA_QUERIES> Repo = new OracleRepository<DEVICE_USER_DATA_QUERIES>();
            using (var connection = Repo.GetConnection())
            {
                // As long as the query syntax is valid these should not throw an exception.
                connection.NonQuery("Explain Plan for " + Repo.InsertText);
                connection.NonQuery("Explain Plan for " + Repo.UpdateText);
                connection.NonQuery("Explain Plan for " + Repo.SelectText);
                connection.NonQuery("Explain Plan for " + Repo.DeleteText);
            }
        }

        [TestMethod]
        [Timeout(1000)]
        [Description("Ensure that the generated SQL is valid and can be generated quickly.")]
        public void DEVICES_OracleRepoSqlTest()
        {
            OracleRepository<DEVICES> Repo = new OracleRepository<DEVICES>();
            using (var connection = Repo.GetConnection())
            {
                // As long as the query syntax is valid these should not throw an exception.
                connection.NonQuery("Explain Plan for " + Repo.InsertText);
                connection.NonQuery("Explain Plan for " + Repo.UpdateText);
                connection.NonQuery("Explain Plan for " + Repo.SelectText);
                connection.NonQuery("Explain Plan for " + Repo.DeleteText);
            }
        }

        [TestMethod]
        [Timeout(1000)]
        [Description("Ensure that the generated SQL is valid and can be generated quickly.")]
        public void LOGS_OracleRepoSqlTest()
        {
            OracleRepository<LOGS> Repo = new OracleRepository<LOGS>();
            using (var connection = Repo.GetConnection())
            {
                // As long as the query syntax is valid these should not throw an exception.
                connection.NonQuery("Explain Plan for " + Repo.InsertText);
                connection.NonQuery("Explain Plan for " + Repo.UpdateText);
                connection.NonQuery("Explain Plan for " + Repo.SelectText);
                connection.NonQuery("Explain Plan for " + Repo.DeleteText);
            }
        }

        [TestMethod]
        [Timeout(1000)]
        [Description("Ensure that the generated SQL is valid and can be generated quickly.")]
        public void PMH_SETTINGS_OracleRepoSqlTest()
        {
            OracleRepository<PMH_SETTINGS> Repo = new OracleRepository<PMH_SETTINGS>();
            using (var connection = Repo.GetConnection())
            {
                // As long as the query syntax is valid these should not throw an exception.
                connection.NonQuery("Explain Plan for " + Repo.InsertText);
                connection.NonQuery("Explain Plan for " + Repo.UpdateText);
                connection.NonQuery("Explain Plan for " + Repo.SelectText);
                connection.NonQuery("Explain Plan for " + Repo.DeleteText);
            }
        }

        [TestMethod]
        [Timeout(1000)]
        [Description("Ensure that the generated SQL is valid and can be generated quickly.")]
        public void QUERY_SEQUENCE_REQUESTS_OracleRepoSqlTest()
        {
            OracleRepository<QUERY_SEQUENCE_REQUESTS> Repo = new OracleRepository<QUERY_SEQUENCE_REQUESTS>();
            using (var connection = Repo.GetConnection())
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
