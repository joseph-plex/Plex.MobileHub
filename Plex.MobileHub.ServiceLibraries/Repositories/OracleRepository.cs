﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plex.MobileHub.Data;
using System.Reflection;
using Oracle.DataAccess.Client;
using System.Data;

namespace Plex.MobileHub.ServiceLibraries.Repositories
{
    /// <summary>
    /// This Class represnts the Pmh Oracle database
    /// </summary>
    /// <typeparam name="T">This Parameter represents a table in the database</typeparam>
    public class OracleRepository<T> : IRepository<T> where T : IRepositoryEntry, new()
    {
        const string User = "C##PMH";
        const string Pass = "!!!plex!!!sa";
        const string Source = "(DESCRIPTION = (ADDRESS_LIST = (ADDRESS = (PROTOCOL = TCP)(HOST = 10.0.1.96)(PORT = 1521)))(CONNECT_DATA = (SERVICE_NAME = PLE1LIVE)))";
        const string ConnectionString = "User Id=" + User + ";" + "Password=" + Pass + ";" + "Data Source=" + Source + ";";

        public IDbConnection GetConnection()
        {
            IDbConnection connection = new OracleConnection(ConnectionString).OpenConnection();
            if (!commit)
                connection.BeginTransaction(); 
            return connection;
        }

        public IList<String> PrimaryKeys
        {
            get
            {
                return primaryKeys.AsReadOnly();
            }
            protected set
            {
                primaryKeys = new List<String>(value);
            }
        }
        public IList<String> Properties { get; protected set; }

        public String InsertText { get; protected set; }
        public String UpdateText { get; protected set; }
        public String DeleteText { get; protected set; }
        public String SelectText { get; protected set; }

        List<String> primaryKeys;
        bool commit;

        public OracleRepository()
        {
            PrimaryKeys = new List<String>(new T().GetPrimaryKeys());
            Properties = new List<String>(GetEntryPropertyNames(typeof(T))).AsReadOnly();
            InsertText = GenerateInsertText();
            UpdateText = GenerateUpdateText();
            SelectText = GenerateSelectText();
            DeleteText = GenerateDeleteText();
        }

        public OracleRepository(bool commitCommands) : this()
        {
            commit = commitCommands;
        }

        public void Insert(T Entry)
        {
            throw new NotImplementedException();
        }

        public void Update(Predicate<T> predicate, T Entry)
        {
            throw new NotImplementedException();
        }

        public void Delete(Predicate<T> predicate)
        {
            throw new NotImplementedException();
        }

        public T Retrieve(Predicate<T> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> RetrieveAll()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets all property names from a type.
        /// </summary>
        /// <returns>IEnumerable of strings</returns>
        static IEnumerable<String> GetEntryPropertyNames(Type type)
        {
            foreach (var propInfo in type.GetProperties())
                yield return propInfo.Name;
        }
        
        String GenerateInsertText()
        {
            String statement = "INSERT INTO {0}({1}) VALUES ({2})";
            //Do first entry here becasse its unique input.
            String columnNames = Properties.First();
            
            String delimiter = ", ";
            String bph = ":a"; 
            
            String placeHolders = bph + 0;

            //Start @ 1 since first entry is already done
            for (int i = 1; i < Properties.Count; i++)
            {
                columnNames += delimiter + Properties[i];
                placeHolders += delimiter + bph + i;
            }

            statement = String.Format(statement, typeof(T).Name, columnNames, placeHolders);
            return statement;
        }
        String GenerateUpdateText()
        {
            String bph = ":a"; 
            String cvpTemplate = "{0} = {1}";
            String statement = "UPDATE {0} SET {1} WHERE {2}";
            //Can only set Cols that are not a primary Keys, Get those Columns
            var Settables = Properties.Where(p => !PrimaryKeys.Any(k => k == p)).ToList();

            String cvp = String.Format(cvpTemplate, Settables.First() , bph + 0 );
            String condition = String.Format(cvpTemplate, PrimaryKeys.First(), bph + Settables.Count);

            //These are the values to be set | Start @ 1 since first entry is done above.
            for (int i = 1; i < Settables.Count; i++) 
                cvp += ", " + String.Format(cvpTemplate, Settables[i], bph + i);

            //This creates the conditions under which the values are set. | Start @ 1 since first entry is done above.
            for (int i = Settables.Count + 1; i < Properties.Count; i++)
                condition += ", " + String.Format(cvpTemplate, PrimaryKeys[i - Settables.Count], bph + i);

            statement = String.Format(statement, typeof(T).Name, cvp, condition);
            return statement;
        }
        String GenerateSelectText()
        {
            String statement = "SELECT * FROM {0}";
            statement = String.Format(statement, typeof(T).Name);
            return statement;
        }
        String GenerateDeleteText()
        {
            String statement = "DELETE FROM {0} WHERE {1}";
            String bph = ":a";
            String cvpTemplate = "{0} = {1}";
            //var Settables = Properties.Where(p => PrimaryKeys.Any(k => k == p)).ToList();
            String condition = String.Format(cvpTemplate, PrimaryKeys.First(), bph + 0);

            for (int i =1; i < PrimaryKeys.Count; i++)
                condition += ", " + String.Format(cvpTemplate, PrimaryKeys[i], bph + i);

            statement = String.Format(statement, typeof(T).Name, condition);
            return statement;



        }
    }
}
