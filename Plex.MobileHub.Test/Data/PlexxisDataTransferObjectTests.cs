using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using System.Linq;
using Plex.MobileHub.Data;
using Plex.MobileHub.Data.Types;
using Plex.MobileHub.Data.Tables;

namespace Plex.MobileHub.Test.Data
{
    [TestClass]
    public class PlexxisDataTransferObjectTests
    {

        #region APP_QUERIES' Tests
        /// <summary>
        /// Test to ensure that all APP_QUERY fields are actually columns in the database.
        /// </summary>
        [TestMethod]
        public void TableTestAPP_QUERY()
        {
            Type type = typeof(APP_QUERIES);
            using(IDbConnection connection = Utilities.GetConnection(true))
            {
                Result columnData = connection.Query("select TABLE_NAME, COLUMN_NAME from user_tab_columns");
                int tabIndex = columnData.GetColumnIndex("TABLE_NAME");
                int colIndex = columnData.GetColumnIndex("COLUMN_NAME");
                
                var tableColumns = columnData.Rows.FindAll(p => p[tabIndex].Equals(type.Name));
                foreach (var field in type.GetProperties())
                    Assert.IsTrue(tableColumns.Any(p => p[colIndex].Equals(field.Name)), "The Field " + field.Name + " in the type " + type.Name + " does not have a database representation");

            }
        }
        /// <summary>
        /// Ensure that the Text for Inserting APP_QUERIES works.
        /// </summary>
        [TestMethod]
        public void TableTestAPP_QUERY_INSERT_TEXT()
        {
            var table = new APP_QUERIES();
            using (IDbConnection connection = Utilities.GetConnection(true))
                connection.NonQuery("EXPLAIN PLAN FOR " + table.GetInsertText());
        }

        /// <summary>
        /// Ensure that the Text for Updating APP_QUERIES works.
        /// </summary>
        [TestMethod]
        public void TableTestAPP_QUERY_UPDATE_TEXT()
        {
            var table = new APP_QUERIES();
            using (IDbConnection connection = Utilities.GetConnection(true))
                connection.NonQuery("EXPLAIN PLAN FOR " + table.GetUpdateText());
        }
        /// <summary>
        /// Ensure that the Text for Deleting APP_QUERIES works.
        /// </summary>
        [TestMethod]
        public void TableTestAPP_QUERY_DELETE_TEXT()
        {
            var table = new APP_QUERIES();
            using (IDbConnection connection = Utilities.GetConnection(true))
                connection.NonQuery("EXPLAIN PLAN FOR " + table.GetDeleteText());
        }
        #endregion

        #region APP_QUERY_COLLUMNS' Tests
        /// <summary>
        /// Test to ensure that all APP_QUERY_COLUMNS fields are actually columns in the database.
        /// </summary>
        [TestMethod]
        public void TableTestAPP_QUERY_COLUMNS()
        {
            Type type = typeof(APP_QUERY_COLUMNS);
            using (IDbConnection connection = Utilities.GetConnection(true))
            {
                Result columnData = connection.Query("select TABLE_NAME, COLUMN_NAME from user_tab_columns");
                int tabIndex = columnData.GetColumnIndex("TABLE_NAME");
                int colIndex = columnData.GetColumnIndex("COLUMN_NAME");

                var tableColumns = columnData.Rows.FindAll(p => p[tabIndex].Equals(type.Name));
                foreach (var field in type.GetProperties())
                    if (!tableColumns.Any(p => p[colIndex].Equals(field.Name)))
                        throw new Exception("The Field " + field.Name + " in the type " + type.Name + " does not have a database representation");
            }
        }
        /// <summary>
        /// Ensure that the Text for Inserting APP_QUERIES works.
        /// </summary>
        [TestMethod]
        public void TableTestAPP_QUERY_COLUMNS_INSERT_TEXT()
        {
            var table = new APP_QUERY_COLUMNS();
            using (IDbConnection connection = Utilities.GetConnection(true))
                connection.NonQuery("EXPLAIN PLAN FOR " + table.GetInsertText());
        }
        /// <summary>
        /// Ensure that the Text for Updating APP_QUERIES works.
        /// </summary>
        [TestMethod]
        public void TableTestAPP_QUERY_COLUMNS_UPDATE_TEXT()
        {
            var table = new APP_QUERY_COLUMNS();
            using (IDbConnection connection = Utilities.GetConnection(true))
                connection.NonQuery("EXPLAIN PLAN FOR " + table.GetUpdateText());
        }
        /// <summary>
        /// Ensure that the Text for Deleting APP_QUERIES works.
        /// </summary>
        [TestMethod]
        public void TableTestAPP_QUERY_COLUMNS_DELETE_TEXT()
        {
            var table = new APP_QUERY_COLUMNS();
            using (IDbConnection connection = Utilities.GetConnection(true))
                connection.NonQuery("EXPLAIN PLAN FOR " + table.GetDeleteText());
        }
        #endregion

        #region APP_QUERY_CONDITIONS' Tests
        /// <summary>
        /// Test to ensure that all APP_QUERY_CONDITIONS fields are actually columns in the database.
        /// </summary>
        [TestMethod]
        public void TableTestAPP_QUERY_CONDITIONS()
        {
            Type type = typeof(APP_QUERY_CONDITIONS);
            using (IDbConnection connection = Utilities.GetConnection(true))
            {
                Result columnData = connection.Query("select TABLE_NAME, COLUMN_NAME from user_tab_columns");
                int tabIndex = columnData.GetColumnIndex("TABLE_NAME");
                int colIndex = columnData.GetColumnIndex("COLUMN_NAME");

                var tableColumns = columnData.Rows.FindAll(p => p[tabIndex].Equals(type.Name));
                foreach (var field in type.GetProperties())
                    if (!tableColumns.Any(p => p[colIndex].Equals(field.Name)))
                        throw new Exception("The Field " + field.Name + " in the type " + type.Name + " does not have a database representation");
            }
        }
        /// <summary>
        /// Ensure that the Text for Inserting APP_QUERY_CONDITIONS works.
        /// </summary>
        [TestMethod]
        public void TableTestAPP_QUERY_CONDITIONS_INSERT_TEXT()
        {
            var table = new APP_QUERY_CONDITIONS();
            using (IDbConnection connection = Utilities.GetConnection(true))
                connection.NonQuery("EXPLAIN PLAN FOR " + table.GetInsertText());
        }
        /// <summary>
        /// Ensure that the Text for Updating APP_QUERIES works.
        /// </summary>
        [TestMethod]
        public void TableTestAPP_QUERY_CONDITIONS_UPDATE_TEXT()
        {
            var table = new APP_QUERY_CONDITIONS();
            using (IDbConnection connection = Utilities.GetConnection(true))
                connection.NonQuery("EXPLAIN PLAN FOR " + table.GetUpdateText());
        }
        /// <summary>
        /// Ensure that the Text for Deleting APP_QUERIES works.
        /// </summary>
        [TestMethod]
        public void TableTestAPP_QUERY_CONDITIONS_DELETE_TEXT()
        {
            var table = new APP_QUERY_CONDITIONS();
            using (IDbConnection connection = Utilities.GetConnection(true))
                connection.NonQuery("EXPLAIN PLAN FOR " + table.GetDeleteText());
        }
        #endregion

        #region APP_TABLE_COLUMNS' Tests
        /// <summary>
        /// Test to ensure that all APP_TABLE_COLUMNS fields are actually columns in the database.
        /// </summary>
        [TestMethod]
        public void TableTestAPP_TABLE_COLUMNS()
        {
            Type type = typeof(APP_TABLE_COLUMNS);
            using (IDbConnection connection = Utilities.GetConnection(true))
            {
                Result columnData = connection.Query("select TABLE_NAME, COLUMN_NAME from user_tab_columns");
                int tabIndex = columnData.GetColumnIndex("TABLE_NAME");
                int colIndex = columnData.GetColumnIndex("COLUMN_NAME");

                var tableColumns = columnData.Rows.FindAll(p => p[tabIndex].Equals(type.Name));
                foreach (var field in type.GetProperties())
                    if (!tableColumns.Any(p => p[colIndex].Equals(field.Name)))
                        throw new Exception("The Field " + field.Name + " in the type " + type.Name + " does not have a database representation");
            }
        }
        /// <summary>
        /// Ensure that the Text for Inserting APP_TABLE_COLUMNS works.
        /// </summary>
        [TestMethod]
        public void TableTestAPP_TABLE_COLUMNS_INSERT_TEXT()
        {
            var table = new APP_TABLE_COLUMNS();
            using (IDbConnection connection = Utilities.GetConnection(true))
                connection.NonQuery("EXPLAIN PLAN FOR " + table.GetInsertText());
        }
        /// <summary>
        /// Ensure that the Text for Updating APP_TABLE_COLUMNS works.
        /// </summary>
        [TestMethod]
        public void TableTestAPP_TABLE_COLUMNS_UPDATE_TEXT()
        {
            var table = new APP_TABLE_COLUMNS();
            using (IDbConnection connection = Utilities.GetConnection(true))
                connection.NonQuery("EXPLAIN PLAN FOR " + table.GetUpdateText());
        }
        /// <summary>
        /// Ensure that the Text for Deleting APP_TABLE_COLUMNS works.
        /// </summary>
        [TestMethod]
        public void TableTestAPP_TABLE_COLUMNS_DELETE_TEXT()
        {
            var table = new APP_TABLE_COLUMNS();
            using (IDbConnection connection = Utilities.GetConnection(true))
                connection.NonQuery("EXPLAIN PLAN FOR " + table.GetDeleteText());
        }
        #endregion

        #region APP_TABLES' Tests
        /// <summary>
        /// Test to ensure that all APP_TABLES fields are actually columns in the database.
        /// </summary>
        [TestMethod]
        public void TableTestAPP_TABLES()
        {
            Type type = typeof(APP_TABLES);
            using (IDbConnection connection = Utilities.GetConnection(true))
            {
                Result columnData = connection.Query("select TABLE_NAME, COLUMN_NAME from user_tab_columns");
                int tabIndex = columnData.GetColumnIndex("TABLE_NAME");
                int colIndex = columnData.GetColumnIndex("COLUMN_NAME");

                var tableColumns = columnData.Rows.FindAll(p => p[tabIndex].Equals(type.Name));
                foreach (var field in type.GetProperties())
                    if (!tableColumns.Any(p => p[colIndex].Equals(field.Name)))
                        throw new Exception("The Field " + field.Name + " in the type " + type.Name + " does not have a database representation");
            }
        }

        /// <summary>
        /// Ensure that the Text for Inserting APP_TABLES works.
        /// </summary>
        [TestMethod]
        public void TableTestAPP_TABLES_INSERT_TEXT()
        {
            var table = new APP_TABLES();
            using (IDbConnection connection = Utilities.GetConnection(true))
                connection.NonQuery("EXPLAIN PLAN FOR " + table.GetInsertText());
        }

        /// <summary>
        /// Ensure that the Text for Updating APP_TABLES works.
        /// </summary>
        [TestMethod]
        public void TableTestAPP_TABLES_UPDATE_TEXT()
        {
            var table = new APP_TABLES();
            using (IDbConnection connection = Utilities.GetConnection(true))
                connection.NonQuery("EXPLAIN PLAN FOR " + table.GetUpdateText());
        }

        /// <summary>
        /// Ensure that the Text for Deleting APP_TABLES works.
        /// </summary>
        [TestMethod]
        public void TableTestAPP_TABLES_DELETE_TEXT()
        {
            var table = new APP_TABLES();
            using (IDbConnection connection = Utilities.GetConnection(true))
                connection.NonQuery("EXPLAIN PLAN FOR " + table.GetDeleteText());
        }
        #endregion

        #region APP_USER_TYPES' Tests
        /// <summary>
        /// Test to ensure that all APP_USER_TYPES fields are actually columns in the database.
        /// </summary>
        [TestMethod]
        public void TableTestAPP_USER_TYPES()
        {
            Type type = typeof(APP_USER_TYPES);
            using (IDbConnection connection = Utilities.GetConnection(true))
            {
                Result columnData = connection.Query("select TABLE_NAME, COLUMN_NAME from user_tab_columns");
                int tabIndex = columnData.GetColumnIndex("TABLE_NAME");
                int colIndex = columnData.GetColumnIndex("COLUMN_NAME");

                var tableColumns = columnData.Rows.FindAll(p => p[tabIndex].Equals(type.Name));
                foreach (var field in type.GetProperties())
                    if (!tableColumns.Any(p => p[colIndex].Equals(field.Name)))
                        throw new Exception("The Field " + field.Name + " in the type " + type.Name + " does not have a database representation");
            }
        }

        /// <summary>
        /// Ensure that the Text for Inserting APP_TABLES works.
        /// </summary>
        [TestMethod]
        public void TableTestAPP_USER_TYPES_INSERT_TEXT()
        {
            var table = new APP_USER_TYPES();
            using (IDbConnection connection = Utilities.GetConnection(true))
                connection.NonQuery("EXPLAIN PLAN FOR " + table.GetInsertText());
        }

        /// <summary>
        /// Ensure that the Text for Updating APP_USER_TYPES works.
        /// </summary>
        [TestMethod]
        public void TableTestAPP_USER_TYPES_UPDATE_TEXT()
        {
            var table = new APP_USER_TYPES();
            using (IDbConnection connection = Utilities.GetConnection(true))
                connection.NonQuery("EXPLAIN PLAN FOR " + table.GetUpdateText());
        }

        /// <summary>
        /// Ensure that the Text for Deleting APP_USER_TYPES works.
        /// </summary>
        [TestMethod]
        public void TableTestAPP_USER_TYPES_DELETE_TEXT()
        {
            var table = new APP_USER_TYPES();
            using (IDbConnection connection = Utilities.GetConnection(true))
                connection.NonQuery("EXPLAIN PLAN FOR " + table.GetDeleteText());
        }

        #endregion 
        
        #region APPS' Tests

        /// <summary>
        /// Test to ensure that all APPS fields are actually columns in the database.
        /// </summary>
        [TestMethod]
        public void TableTestAPPS()
        {
            Type type = typeof(APPS);
            using (IDbConnection connection = Utilities.GetConnection(true))
            {
                Result columnData = connection.Query("select TABLE_NAME, COLUMN_NAME from user_tab_columns");
                int tabIndex = columnData.GetColumnIndex("TABLE_NAME");
                int colIndex = columnData.GetColumnIndex("COLUMN_NAME");

                var tableColumns = columnData.Rows.FindAll(p => p[tabIndex].Equals(type.Name));
                foreach (var field in type.GetProperties())
                    if (!tableColumns.Any(p => p[colIndex].Equals(field.Name)))
                        throw new Exception("The Field " + field.Name + " in the type " + type.Name + " does not have a database representation");
            }
        }

        /// <summary>
        /// Ensure that the Text for Inserting APPS works.
        /// </summary>
        [TestMethod]
        public void TableTestAPPS_INSERT_TEXT()
        {
            var table = new APPS();
            using (IDbConnection connection = Utilities.GetConnection(true))
                connection.NonQuery("EXPLAIN PLAN FOR " + table.GetInsertText());
        }

        /// <summary>
        /// Ensure that the Text for Updating APPS works.
        /// </summary>
        [TestMethod]
        public void TableTestAPPS_UPDATE_TEXT()
        {
            var table = new APPS();
            using (IDbConnection connection = Utilities.GetConnection(true))
                connection.NonQuery("EXPLAIN PLAN FOR " + table.GetUpdateText());
        }

        /// <summary>
        /// Ensure that the Text for Deleting APPS works.
        /// </summary>
        [TestMethod]
        public void TableTestAPPS_DELETE_TEXT()
        {
            var table = new APPS();
            using (IDbConnection connection = Utilities.GetConnection(true))
                connection.NonQuery("EXPLAIN PLAN FOR " + table.GetDeleteText());
        }
        #endregion 

        #region CLIENT_APPS' Tests
        /// <summary>
        /// Test to ensure that all CLIENT_APPS fields are actually columns in the database.
        /// </summary>
        [TestMethod]
        public void TableTestCLIENT_APPS()
        {
            Type type = typeof(CLIENT_APPS);
            using (IDbConnection connection = Utilities.GetConnection(true))
            {
                Result columnData = connection.Query("select TABLE_NAME, COLUMN_NAME from user_tab_columns");
                int tabIndex = columnData.GetColumnIndex("TABLE_NAME");
                int colIndex = columnData.GetColumnIndex("COLUMN_NAME");

                var tableColumns = columnData.Rows.FindAll(p => p[tabIndex].Equals(type.Name));
                foreach (var field in type.GetProperties())
                    if (!tableColumns.Any(p => p[colIndex].Equals(field.Name)))
                        throw new Exception("The Field " + field.Name + " in the type " + type.Name + " does not have a database representation");
            }
        }
        [TestMethod]
        public void TableTestCLIENT_APPS_INSERT_TEXT()
        {   
            var table = new CLIENT_APPS();
            using (IDbConnection connection = Utilities.GetConnection(true))
                connection.NonQuery("EXPLAIN PLAN FOR " + table.GetInsertText());
        }

        [TestMethod]
        public void TableTestCLIENT_APPS_UPDATE_TEXT()
        {
            var table = new CLIENT_APPS();
            using (IDbConnection connection = Utilities.GetConnection(true))
                connection.NonQuery("EXPLAIN PLAN FOR " + table.GetUpdateText());
        }
        [TestMethod]
        public void TableTestCLIENT_APPS_DELETE_TEXT()
        {
            var table = new CLIENT_APPS();
            using (IDbConnection connection = Utilities.GetConnection(true))
                connection.NonQuery("EXPLAIN PLAN FOR " + table.GetDeleteText());
        }
        #endregion

        #region CLIENT_DB_COMPANIES' Tests

        /// <summary>
        /// Test to ensure that all CLIENT_DB_COMPANIES fields are actually columns in the database.
        /// </summary>
        [TestMethod]
        public void TableTestCLIENT_DB_COMPANIES()
        {
            Type type = typeof(CLIENT_DB_COMPANIES);
            using (IDbConnection connection = Utilities.GetConnection(true))
            {
                Result columnData = connection.Query("select TABLE_NAME, COLUMN_NAME from user_tab_columns");
                int tabIndex = columnData.GetColumnIndex("TABLE_NAME");
                int colIndex = columnData.GetColumnIndex("COLUMN_NAME");

                var tableColumns = columnData.Rows.FindAll(p => p[tabIndex].Equals(type.Name));
                foreach (var field in type.GetProperties())
                    if (!tableColumns.Any(p => p[colIndex].Equals(field.Name)))
                        throw new Exception("The Field " + field.Name + " in the type " + type.Name + " does not have a database representation");
            }
        }
        [TestMethod]
        public void TableTestCLIENT_DB_COMPANIESS_INSERT_TEXT()
        {
            var table = new CLIENT_DB_COMPANIES();
            using (IDbConnection connection = Utilities.GetConnection(true))
                connection.NonQuery("EXPLAIN PLAN FOR " + table.GetInsertText());
        }

        [TestMethod]
        public void TableTestCLIENT_DB_COMPANIES_UPDATE_TEXT()
        {
            var table = new CLIENT_DB_COMPANIES();
            using (IDbConnection connection = Utilities.GetConnection(true))
                connection.NonQuery("EXPLAIN PLAN FOR " + table.GetUpdateText());
        }
        [TestMethod]
        public void TableTestCLIENT_DB_COMPANIES_DELETE_TEXT()
        {
            var table = new CLIENT_DB_COMPANIES();
            using (IDbConnection connection = Utilities.GetConnection(true))
                connection.NonQuery("EXPLAIN PLAN FOR " + table.GetDeleteText());
        }
        #endregion

        #region CLIENT_DB_COMPANY_USER_APPS' Tests

        /// <summary>
        /// Test to ensure that all CLIENT_DB_COMPANY_USER_APPS fields are actually columns in the database.
        /// </summary>
        [TestMethod]
        public void TableTestCLIENT_DB_COMPANY_USER_APPS()
        {
            Type type = typeof(CLIENT_DB_COMPANY_USER_APPS);
            using (IDbConnection connection = Utilities.GetConnection(true))
            {
                Result columnData = connection.Query("select TABLE_NAME, COLUMN_NAME from user_tab_columns");
                int tabIndex = columnData.GetColumnIndex("TABLE_NAME");
                int colIndex = columnData.GetColumnIndex("COLUMN_NAME");

                var tableColumns = columnData.Rows.FindAll(p => p[tabIndex].Equals(type.Name));
                foreach (var field in type.GetProperties())
                    if (!tableColumns.Any(p => p[colIndex].Equals(field.Name)))
                        throw new Exception("The Field " + field.Name + " in the type " + type.Name + " does not have a database representation");
            }
        }
        [TestMethod]
        public void TableTestCLIENT_DB_COMPANY_USER_APPS_INSERT_TEXT()
        {
            var table = new CLIENT_DB_COMPANY_USER_APPS();
            using (IDbConnection connection = Utilities.GetConnection(true))
                connection.NonQuery("EXPLAIN PLAN FOR " + table.GetInsertText());
        }

        [TestMethod]
        public void TableTestCLIENT_DB_COMPANY_USER_APPS_UPDATE_TEXT()
        {
            var table = new CLIENT_DB_COMPANY_USER_APPS();
            using (IDbConnection connection = Utilities.GetConnection(true))
                connection.NonQuery("EXPLAIN PLAN FOR " + table.GetUpdateText());
        }
        [TestMethod]
        public void TableTestCLIENT_DB_COMPANY_USER_APPS_DELETE_TEXT()
        {
            var table = new CLIENT_DB_COMPANY_USER_APPS();
            using (IDbConnection connection = Utilities.GetConnection(true))
                connection.NonQuery("EXPLAIN PLAN FOR " + table.GetDeleteText());
        }

        #endregion

        #region CLIENT_DB_COMPANY_USERS Tests'
        /// <summary>
        /// Test to ensure that all CLIENT_DB_COMPANY_USERS fields are actually columns in the database.
        /// </summary>
        [TestMethod]
        public void TableTestCLIENT_DB_COMPANY_USERS()
        {
            Type type = typeof(CLIENT_DB_COMPANY_USERS);
            using (IDbConnection connection = Utilities.GetConnection(true))
            {
                Result columnData = connection.Query("select TABLE_NAME, COLUMN_NAME from user_tab_columns");
                int tabIndex = columnData.GetColumnIndex("TABLE_NAME");
                int colIndex = columnData.GetColumnIndex("COLUMN_NAME");

                var tableColumns = columnData.Rows.FindAll(p => p[tabIndex].Equals(type.Name));
                foreach (var field in type.GetProperties())
                    if (!tableColumns.Any(p => p[colIndex].Equals(field.Name)))
                        throw new Exception("The Field " + field.Name + " in the type " + type.Name + " does not have a database representation");
            }
        }
        [TestMethod]
        public void TableTestCLIENT_DB_COMPANY_USERS_INSERT_TEXT()
        {
            var table = new CLIENT_DB_COMPANY_USERS();
            using (IDbConnection connection = Utilities.GetConnection(true))
                connection.NonQuery("EXPLAIN PLAN FOR " + table.GetInsertText());
        }

        [TestMethod]
        public void TableTestCLIENT_DB_COMPANY_USERS_UPDATE_TEXT()
        {
            var table = new CLIENT_DB_COMPANY_USERS();
            using (IDbConnection connection = Utilities.GetConnection(true))
                connection.NonQuery("EXPLAIN PLAN FOR " + table.GetUpdateText());
        }
        [TestMethod]
        public void TableTestCLIENT_DB_COMPANY_USERS_DELETE_TEXT()
        {
            var table = new CLIENT_DB_COMPANY_USERS();
            using (IDbConnection connection = Utilities.GetConnection(true))
                connection.NonQuery("EXPLAIN PLAN FOR " + table.GetDeleteText());
        }

        #endregion

        #region CLIENT_USERS Tests'
        /// <summary>
        /// Test to ensure that all CLIENT_USERS fields are actually columns in the database.
        /// </summary>
        [TestMethod]
        public void TableTestCLIENT_USERS()
        {
            Type type = typeof(CLIENT_USERS);
            using (IDbConnection connection = Utilities.GetConnection(true))
            {
                Result columnData = connection.Query("select TABLE_NAME, COLUMN_NAME from user_tab_columns");
                int tabIndex = columnData.GetColumnIndex("TABLE_NAME");
                int colIndex = columnData.GetColumnIndex("COLUMN_NAME");

                var tableColumns = columnData.Rows.FindAll(p => p[tabIndex].Equals(type.Name));
                foreach (var field in type.GetProperties())
                    if (!tableColumns.Any(p => p[colIndex].Equals(field.Name)))
                        throw new Exception("The Field " + field.Name + " in the type " + type.Name + " does not have a database representation");
            }
        }
        [TestMethod]
        public void TableTesCLIENT_USERS_INSERT_TEXT()
        {
            var table = new CLIENT_USERS();
            using (IDbConnection connection = Utilities.GetConnection(true))
                connection.NonQuery("EXPLAIN PLAN FOR " + table.GetInsertText());
        }

        [TestMethod]
        public void TableTestCLIENT_USERS_UPDATE_TEXT()
        {
            var table = new CLIENT_USERS();
            using (IDbConnection connection = Utilities.GetConnection(true))
                connection.NonQuery("EXPLAIN PLAN FOR " + table.GetUpdateText());
        }
        [TestMethod]
        public void TableTestCLIENT_USERS_DELETE_TEXT()
        {
            var table = new CLIENT_USERS();
            using (IDbConnection connection = Utilities.GetConnection(true))
                connection.NonQuery("EXPLAIN PLAN FOR " + table.GetDeleteText());
        }

        #endregion

        #region CLIENTS Tests'

        /// <summary>
        /// Test to ensure that all CLIENTS fields are actually columns in the database.
        /// </summary>
        [TestMethod]
        public void TableTestCLIENTS()
        {
            Type type = typeof(CLIENTS);
            using (IDbConnection connection = Utilities.GetConnection(true))
            {
                Result columnData = connection.Query("select TABLE_NAME, COLUMN_NAME from user_tab_columns");
                int tabIndex = columnData.GetColumnIndex("TABLE_NAME");
                int colIndex = columnData.GetColumnIndex("COLUMN_NAME");

                var tableColumns = columnData.Rows.FindAll(p => p[tabIndex].Equals(type.Name));
                foreach (var field in type.GetProperties())
                    if (!tableColumns.Any(p => p[colIndex].Equals(field.Name)))
                        throw new Exception("The Field " + field.Name + " in the type " + type.Name + " does not have a database representation");
            }
        }
        [TestMethod]
        public void TableTestCLIENTS_INSERT_TEXT()
        {
            var table = new CLIENTS();
            using (IDbConnection connection = Utilities.GetConnection(true))
                connection.NonQuery("EXPLAIN PLAN FOR " + table.GetInsertText());
        }

        [TestMethod]
        public void TableTestCLIENTS_UPDATE_TEXT()
        {
            var table = new CLIENTS();
            using (IDbConnection connection = Utilities.GetConnection(true))
                connection.NonQuery("EXPLAIN PLAN FOR " + table.GetUpdateText());
        }
        [TestMethod]
        public void TableTestCLIENTS_DELETE_TEXT()
        {
            var table = new CLIENTS();
            using (IDbConnection connection = Utilities.GetConnection(true))
                connection.NonQuery("EXPLAIN PLAN FOR " + table.GetDeleteText());
        }
        #endregion

        #region DEV_DATA Tests'
        /// <summary>
        /// Test to ensure that all DEV_DATA fields are actually columns in the database.
        /// </summary>
        [TestMethod]
        public void TableTestDEV_DATA()
        {
            Type type = typeof(DEV_DATA);
            using (IDbConnection connection = Utilities.GetConnection(true))
            {
                Result columnData = connection.Query("select TABLE_NAME, COLUMN_NAME from user_tab_columns");
                int tabIndex = columnData.GetColumnIndex("TABLE_NAME");
                int colIndex = columnData.GetColumnIndex("COLUMN_NAME");

                var tableColumns = columnData.Rows.FindAll(p => p[tabIndex].Equals(type.Name));
                foreach (var field in type.GetProperties())
                    if (!tableColumns.Any(p => p[colIndex].Equals(field.Name)))
                        throw new Exception("The Field " + field.Name + " in the type " + type.Name + " does not have a database representation");
            }
        }
        [TestMethod]
        public void TableTestDEV_DATA_INSERT_TEXT()
        {
            var table = new DEV_DATA();
            using (IDbConnection connection = Utilities.GetConnection(true))
                connection.NonQuery("EXPLAIN PLAN FOR " + table.GetInsertText());
        }

        [TestMethod]
        public void TableTestDEV_DATA_UPDATE_TEXT()
        {
            var table = new DEV_DATA();
            using (IDbConnection connection = Utilities.GetConnection(true))
                connection.NonQuery("EXPLAIN PLAN FOR " + table.GetUpdateText());
        }
        [TestMethod]
        public void TableTestDEV_DATA_DELETE_TEXT()
        {
            var table = new DEV_DATA();
            using (IDbConnection connection = Utilities.GetConnection(true))
                connection.NonQuery("EXPLAIN PLAN FOR " + table.GetDeleteText());
        }
        #endregion

        #region DEV_DATA_VER Tests'
        /// <summary>
        /// Test to ensure that all DEV_DATA_VER fields are actually columns in the database.
        /// </summary>
        [TestMethod]
        public void TableTestDEV_DATA_VER()
        {
            Type type = typeof(DEV_DATA_VER);
            using (IDbConnection connection = Utilities.GetConnection(true))
            {
                Result columnData = connection.Query("select TABLE_NAME, COLUMN_NAME from user_tab_columns");
                int tabIndex = columnData.GetColumnIndex("TABLE_NAME");
                int colIndex = columnData.GetColumnIndex("COLUMN_NAME");

                var tableColumns = columnData.Rows.FindAll(p => p[tabIndex].Equals(type.Name));
                foreach (var field in type.GetProperties())
                    if (!tableColumns.Any(p => p[colIndex].Equals(field.Name)))
                        throw new Exception("The Field " + field.Name + " in the type " + type.Name + " does not have a database representation");
            }
        }
        [TestMethod]
        public void TableTestDEV_DATA_VER_INSERT_TEXT()
        {
            var table = new DEV_DATA_VER();
            using (IDbConnection connection = Utilities.GetConnection(true))
                connection.NonQuery("EXPLAIN PLAN FOR " + table.GetInsertText());
        }

        [TestMethod]
        public void TableTestDEV_DATA_VER_UPDATE_TEXT()
        {
            var table = new DEV_DATA_VER();
            using (IDbConnection connection = Utilities.GetConnection(true))
                connection.NonQuery("EXPLAIN PLAN FOR " + table.GetUpdateText());
        }
        [TestMethod]
        public void TableTestDEV_DATA_VER_DELETE_TEXT()
        {
            var table = new DEV_DATA_VER();
            using (IDbConnection connection = Utilities.GetConnection(true))
                connection.NonQuery("EXPLAIN PLAN FOR " + table.GetDeleteText());
        }
        #endregion

        #region DEV_DATA_VER_QUERIES Tests'
        /// <summary>
        /// Test to ensure that all DEV_DATA_VER_QUERIES fields are actually columns in the database.
        /// </summary>
        [TestMethod]
        public void TableTestDEV_DATA_VER_QUERIES()
        {
            Type type = typeof(DEV_DATA_VER_QUERIES);
            using (IDbConnection connection = Utilities.GetConnection(true))
            {
                Result columnData = connection.Query("select TABLE_NAME, COLUMN_NAME from user_tab_columns");
                int tabIndex = columnData.GetColumnIndex("TABLE_NAME");
                int colIndex = columnData.GetColumnIndex("COLUMN_NAME");

                var tableColumns = columnData.Rows.FindAll(p => p[tabIndex].Equals(type.Name));
                foreach (var field in type.GetProperties())
                    if (!tableColumns.Any(p => p[colIndex].Equals(field.Name)))
                        throw new Exception("The Field " + field.Name + " in the type " + type.Name + " does not have a database representation");
            }
        }
        [TestMethod]
        public void TableTestDEV_DATA_VER_QUERIES_INSERT_TEXT()
        {
            var table = new DEV_DATA_VER_QUERIES();
            using (IDbConnection connection = Utilities.GetConnection(true))
                connection.NonQuery("EXPLAIN PLAN FOR " + table.GetInsertText());
        }

        [TestMethod]
        public void TableTestDEV_DATA_VER_QUERIES_UPDATE_TEXT()
        {
            var table = new DEV_DATA_VER_QUERIES();
            using (IDbConnection connection = Utilities.GetConnection(true))
                connection.NonQuery("EXPLAIN PLAN FOR " + table.GetUpdateText());
        }
        [TestMethod]
        public void TableTestDEV_DATA_VER_QUERIES_DELETE_TEXT()
        {
            var table = new DEV_DATA_VER_QUERIES();
            using (IDbConnection connection = Utilities.GetConnection(true))
                connection.NonQuery("EXPLAIN PLAN FOR " + table.GetDeleteText());
        }
        #endregion

        #region DEVICE_USER_DATA Tests'
        /// <summary>
        /// Test to ensure that all DEVICE_USER_DATA fields are actually columns in the database.
        /// </summary>
        [TestMethod]
        public void TableTestDEVICE_USER_DATA()
        {
            Type type = typeof(DEVICE_USER_DATA);
            using (IDbConnection connection = Utilities.GetConnection(true))
            {
                Result columnData = connection.Query("select TABLE_NAME, COLUMN_NAME from user_tab_columns");
                int tabIndex = columnData.GetColumnIndex("TABLE_NAME");
                int colIndex = columnData.GetColumnIndex("COLUMN_NAME");

                var tableColumns = columnData.Rows.FindAll(p => p[tabIndex].Equals(type.Name));
                foreach (var field in type.GetProperties())
                    if (!tableColumns.Any(p => p[colIndex].Equals(field.Name)))
                        throw new Exception("The Field " + field.Name + " in the type " + type.Name + " does not have a database representation");
            }
        }
        [TestMethod]
        public void TableTestDEVICE_USER_DATA_INSERT_TEXT()
        {
            var table = new DEVICE_USER_DATA();
            using (IDbConnection connection = Utilities.GetConnection(true))
                connection.NonQuery("EXPLAIN PLAN FOR " + table.GetInsertText());
        }

        [TestMethod]
        public void TableTestDEVICE_USER_DATA_UPDATE_TEXT()
        {
            var table = new DEVICE_USER_DATA();
            using (IDbConnection connection = Utilities.GetConnection(true))
                connection.NonQuery("EXPLAIN PLAN FOR " + table.GetUpdateText());
        }
        [TestMethod]
        public void TableTestDEVICE_USER_DATA_DELETE_TEXT()
        {
            var table = new DEVICE_USER_DATA();
            using (IDbConnection connection = Utilities.GetConnection(true))
                connection.NonQuery("EXPLAIN PLAN FOR " + table.GetDeleteText());
        }
        #endregion

        #region DEVICE_USER_DATA_QUERIES Tests'
        /// <summary>
        /// Test to ensure that all DEVICE_USER_DATA_QUERIES fields are actually columns in the database.
        /// </summary>
        [TestMethod]
        public void TableTestDEVICE_USER_DATA_QUERIES()
        {
            Type type = typeof(DEVICE_USER_DATA_QUERIES);
            using (IDbConnection connection = Utilities.GetConnection(true))
            {
                Result columnData = connection.Query("select TABLE_NAME, COLUMN_NAME from user_tab_columns");
                int tabIndex = columnData.GetColumnIndex("TABLE_NAME");
                int colIndex = columnData.GetColumnIndex("COLUMN_NAME");

                var tableColumns = columnData.Rows.FindAll(p => p[tabIndex].Equals(type.Name));
                foreach (var field in type.GetProperties())
                    if (!tableColumns.Any(p => p[colIndex].Equals(field.Name)))
                        throw new Exception("The Field " + field.Name + " in the type " + type.Name + " does not have a database representation");
            }
        }
        [TestMethod]
        public void TableTestDEVICE_USER_DATA_QUERIES_INSERT_TEXT()
        {
            var table = new DEVICE_USER_DATA_QUERIES();
            using (IDbConnection connection = Utilities.GetConnection(true))
                connection.NonQuery("EXPLAIN PLAN FOR " + table.GetInsertText());
        }

        [TestMethod]
        public void TableTestDEVICE_USER_DATA_QUERIES_UPDATE_TEXT()
        {
            var table = new DEVICE_USER_DATA_QUERIES();
            using (IDbConnection connection = Utilities.GetConnection(true))
                connection.NonQuery("EXPLAIN PLAN FOR " + table.GetUpdateText());
        }
        [TestMethod]
        public void TableTestDEVICE_USER_DATA_QUERIES_DELETE_TEXT()
        {
            var table = new DEVICE_USER_DATA_QUERIES();
            using (IDbConnection connection = Utilities.GetConnection(true))
                connection.NonQuery("EXPLAIN PLAN FOR " + table.GetDeleteText());
        }

        #endregion

        #region DEVICES Tests'
        /// <summary>
        /// Test to ensure that all DEVICES fields are actually columns in the database.
        /// </summary>
        [TestMethod]
        public void TableTestDEVICES()
        {
            Type type = typeof(DEVICES);
            using (IDbConnection connection = Utilities.GetConnection(true))
            {
                Result columnData = connection.Query("select TABLE_NAME, COLUMN_NAME from user_tab_columns");
                int tabIndex = columnData.GetColumnIndex("TABLE_NAME");
                int colIndex = columnData.GetColumnIndex("COLUMN_NAME");

                var tableColumns = columnData.Rows.FindAll(p => p[tabIndex].Equals(type.Name));
                foreach (var field in type.GetProperties())
                    if (!tableColumns.Any(p => p[colIndex].Equals(field.Name)))
                        throw new Exception("The Field " + field.Name + " in the type " + type.Name + " does not have a database representation");
            }
        }
        [TestMethod]
        public void TableTestDEVICES_INSERT_TEXT()
        {
            var table = new DEVICES();
            using (IDbConnection connection = Utilities.GetConnection(true))
                connection.NonQuery("EXPLAIN PLAN FOR " + table.GetInsertText());
        }

        [TestMethod]
        public void TableTestDEVICES_UPDATE_TEXT()
        {
            var table = new DEVICES();
            using (IDbConnection connection = Utilities.GetConnection(true))
                connection.NonQuery("EXPLAIN PLAN FOR " + table.GetUpdateText());
        }
        [TestMethod]
        public void TableTestDEVICES_DELETE_TEXT()
        {
            var table = new DEVICES();
            using (IDbConnection connection = Utilities.GetConnection(true))
                connection.NonQuery("EXPLAIN PLAN FOR " + table.GetDeleteText());
        }
        #endregion

        #region LOGS Tests'
        /// <summary>
        /// Test to ensure that all LOGS fields are actually columns in the database.
        /// </summary>
        [TestMethod]
        public void TableTestLOGS()
        {
            Type type = typeof(LOGS);
            using (IDbConnection connection = Utilities.GetConnection(true))
            {
                Result columnData = connection.Query("select TABLE_NAME, COLUMN_NAME from user_tab_columns");
                int tabIndex = columnData.GetColumnIndex("TABLE_NAME");
                int colIndex = columnData.GetColumnIndex("COLUMN_NAME");

                var tableColumns = columnData.Rows.FindAll(p => p[tabIndex].Equals(type.Name));
                foreach (var field in type.GetProperties())
                    if (!tableColumns.Any(p => p[colIndex].Equals(field.Name)))
                        throw new Exception("The Field " + field.Name + " in the type " + type.Name + " does not have a database representation");
            }
        }
        [TestMethod]
        public void TableTestLOGS_INSERT_TEXT()
        {
            var table = new LOGS();
            using (IDbConnection connection = Utilities.GetConnection(true))
                connection.NonQuery("EXPLAIN PLAN FOR " + table.GetInsertText());
        }

        [TestMethod]
        public void TableTestLOGS_UPDATE_TEXT()
        {
            var table = new LOGS();
            using (IDbConnection connection = Utilities.GetConnection(true))
                connection.NonQuery("EXPLAIN PLAN FOR " + table.GetUpdateText());
        }
        [TestMethod]
        public void TableTestLOGS_DELETE_TEXT()
        {
            var table = new LOGS();
            using (IDbConnection connection = Utilities.GetConnection(true))
                connection.NonQuery("EXPLAIN PLAN FOR " + table.GetDeleteText());
        }
        #endregion

        #region PMH_SETTINGS Tests'
        /// <summary>
        /// Test to ensure that all PMH_SETTINGS fields are actually columns in the database.
        /// </summary>
        [TestMethod]
        public void TableTestPMH_SETTINGS()
        {
            Type type = typeof(PMH_SETTINGS);
            using (IDbConnection connection = Utilities.GetConnection(true))
            {
                Result columnData = connection.Query("select TABLE_NAME, COLUMN_NAME from user_tab_columns");
                int tabIndex = columnData.GetColumnIndex("TABLE_NAME");
                int colIndex = columnData.GetColumnIndex("COLUMN_NAME");

                var tableColumns = columnData.Rows.FindAll(p => p[tabIndex].Equals(type.Name));
                foreach (var field in type.GetProperties())
                    if (!tableColumns.Any(p => p[colIndex].Equals(field.Name)))
                        throw new Exception("The Field " + field.Name + " in the type " + type.Name + " does not have a database representation");
            }
        }
        [TestMethod]
        public void TableTesPMH_SETTINGS_TEXT()
        {
            var table = new PMH_SETTINGS();
            using (IDbConnection connection = Utilities.GetConnection(true))
                connection.NonQuery("EXPLAIN PLAN FOR " + table.GetUpdateText());
        }

        [TestMethod]
        public void TableTestPMH_SETTINGS_UPDATE_TEXT()
        {
            var table = new PMH_SETTINGS();
            using (IDbConnection connection = Utilities.GetConnection(true))
                connection.NonQuery("EXPLAIN PLAN FOR " + table.GetUpdateText());
        }
        [TestMethod]
        public void TableTestPMH_SETTINGS_DELETE_TEXT()
        {
            var table = new PMH_SETTINGS();
            using (IDbConnection connection = Utilities.GetConnection(true))
                connection.NonQuery("EXPLAIN PLAN FOR " + table.GetDeleteText());
        }
        #endregion

        #region QUERY_SEQUENCE_REQUESTS Tests'
        /// <summary>
        /// Test to ensure that all QUERY_SEQUENCE_REQUESTS fields are actually columns in the database.
        /// </summary>
        [TestMethod]
        public void TableTestQUERY_SEQUENCE_REQUESTS()
        {
            Type type = typeof(QUERY_SEQUENCE_REQUESTS);
            using (IDbConnection connection = Utilities.GetConnection(true))
            {
                Result columnData = connection.Query("select TABLE_NAME, COLUMN_NAME from user_tab_columns");
                int tabIndex = columnData.GetColumnIndex("TABLE_NAME");
                int colIndex = columnData.GetColumnIndex("COLUMN_NAME");

                var tableColumns = columnData.Rows.FindAll(p => p[tabIndex].Equals(type.Name));
                foreach (var field in type.GetProperties())
                    if (!tableColumns.Any(p => p[colIndex].Equals(field.Name)))
                        throw new Exception("The Field " + field.Name + " in the type " + type.Name + " does not have a database representation");
            }
        }
        [TestMethod]
        public void TableTestQUERY_SEQUENCE_REQUESTS_INSERT_TEXT()
        {
            var table = new QUERY_SEQUENCE_REQUESTS();
            using (IDbConnection connection = Utilities.GetConnection(true))
                connection.NonQuery("EXPLAIN PLAN FOR " + table.GetInsertText());
        }

        [TestMethod]
        public void TableTestQUERY_SEQUENCE_REQUESTS_UPDATE_TEXT()
        {
            var table = new QUERY_SEQUENCE_REQUESTS();
            using (IDbConnection connection = Utilities.GetConnection(true))
                connection.NonQuery("EXPLAIN PLAN FOR " + table.GetUpdateText());
        }
        [TestMethod]
        public void TableTestQUERY_SEQUENCE_REQUESTS_DELETE_TEXT()
        {
            var table = new QUERY_SEQUENCE_REQUESTS();
            using (IDbConnection connection = Utilities.GetConnection(true))
                connection.NonQuery("EXPLAIN PLAN FOR " + table.GetDeleteText());
        }
        #endregion
    }
}
