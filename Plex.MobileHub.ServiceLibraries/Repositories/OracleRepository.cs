using System;
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
    public  class  OracleRepository<T> : IDisposable, IRepository<T> where T : RepositoryEntryBase, IRepositoryEntry, new()
    {
        const string User = "C##PMH";
        const string Pass = "!!!plex!!!sa";
        const string Source = "(DESCRIPTION = (ADDRESS_LIST = (ADDRESS = (PROTOCOL = TCP)(HOST = 10.0.1.96)(PORT = 1521)))(CONNECT_DATA = (SERVICE_NAME = PLE1LIVE)))";
        const string ConnectionString = "User Id=" + User + ";" + "Password=" + Pass + ";" + "Data Source=" + Source + ";";


        /// <summary>
        /// Gets all property names from a type.
        /// </summary>
        /// <returns>IEnumerable of strings</returns>
        static IEnumerable<String> GetEntryPropertyNames(Type type)
        {
            foreach (var propInfo in type.GetProperties())
                yield return propInfo.Name;
        }

        public event RepoEventHandler InsertEvent;
        public event RepoEventHandler UpdateEvent;
        public event RepoEventHandler DeleteEvent;

        #region Properties
        public IList<String> PrimaryKeys
        {
            get
            {
                return primaryKeys.AsReadOnly();
            }
            private set
            {
                primaryKeys = new List<String>(value);
            }
        }
        public IList<String> Properties { get; private set; }

        public String InsertText { get; private set; }
        public String UpdateText { get; private set; }
        public String DeleteText { get; private set; }
        public String SelectText { get; private set; }
        #endregion

        #region Fields
        IDbConnection connection;
        IDbTransaction transaction;
        List<String> primaryKeys;
        #endregion

        #region Constructors
        public OracleRepository()
        {
            PrimaryKeys = new List<String>(new T().GetPrimaryKeys());
            Properties = new List<String>(GetEntryPropertyNames(typeof(T))).AsReadOnly();
            InsertText = GenerateInsertText();
            UpdateText = GenerateUpdateText();
            SelectText = GenerateSelectText();
            DeleteText = GenerateDeleteText();
            connection = GetConnection();
        }
        #endregion

        #region Interface Implementations
        public void Insert(T Entry)
        {
            Insert(connection, Entry);
        }
        public void Update(T Entry)
        {
            Update(connection, Entry);
        }
        public void Delete(Predicate<T> predicate)
        {
            Delete(connection, predicate);
        }

        public T Retrieve(Predicate<T> predicate)
        {
            return Retrieve(connection, predicate);
        }
        public bool Exists( Predicate<T> predicate)
        {

            return Exists(connection, predicate);
        }

        public IEnumerable<T> RetrieveAll()
        {
            return RetrieveAll(connection);
        }

        public void Dispose()
        {
            if (transaction != null)
                transaction.Dispose();
            connection.Dispose();
        }
        #endregion

        #region Public Methods

        public void StartTransaction()
        {
            if (transaction != null)
                throw new InvalidOperationException("Transaction is already set. Please Rollback or commit transaction");
            transaction = connection.BeginTransaction();
        }
        public void CommitTransaction()
        {
            transaction.Commit();
            transaction.Dispose();
            transaction = null;
        }
        public void RollbackTransaction()
        {

            transaction.Rollback();
            transaction.Dispose();
            transaction = null;
        }

        public IDbConnection GetConnection()
        {
            IDbConnection connection = new OracleConnection(ConnectionString).OpenConnection();
            return connection;
        }

        public void Insert(IDbConnection connection, T Entry)
        {
            Type type = typeof(T);
            List<Object> args = new List<Object>();
            for (int i = 0; i < Properties.Count; i++)
                args.Add(type.GetProperty(Properties[i]).GetValue(Entry));
            connection.NonQuery(InsertText, args.ToArray());
        }
   
        public void Update(IDbConnection connection, T Entry)
        {
            Type type = typeof(T);
            List<Object> args = new List<Object>();
            
            foreach (var propertyName in Properties.Where(p => !PrimaryKeys.Any(k => k == p)))
                args.Add(type.GetProperty(propertyName).GetValue(Entry));
            foreach (var PropertyName in PrimaryKeys)
                args.Add(type.GetProperty(PropertyName).GetValue(Entry));

            connection.NonQuery(UpdateText, args.ToArray());
        }

  
        public void Delete(IDbConnection connection, Predicate<T> predicate)
        {
            var entryList = RetrieveAll(connection).Where(new Func<T, bool>(predicate));
            Type type = typeof(T);

            foreach(var entry in entryList)
            {
                List<Object> args = new List<Object>();
                foreach (var PropertyName in PrimaryKeys)
                    args.Add(type.GetProperty(PropertyName).GetValue(entry));
                connection.NonQuery(DeleteText, args.ToArray());
            }
        }

        public T Retrieve(IDbConnection connection, Predicate<T> predicate)
        {
            return RetrieveAll(connection).FirstOrDefault(new Func<T, bool>(predicate));
        }
        public bool Exists(IDbConnection connection, Predicate<T> predicate)
        {
            return RetrieveAll(connection).Any(new Func<T, bool>(predicate));
        }
  
        public IEnumerable<T> RetrieveAll(IDbConnection connection)
        {
            List<T> collection = new List<T>();
            var result = connection.Query(SelectText);
            foreach (var row in result.Tuples)
                collection.Add(RepositoryEntryBase.FromPlexQueryResultTuple(new T(), row) as T);
            return collection;
        }
        #endregion

        #region Private Methods

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

        String GenerateDeleteText()
        {
            String bph = ":a";
            String cvpTemplate = "{0} = {1}";
            String statement = "DELETE FROM {0} WHERE {1}";
            String condition = String.Format(cvpTemplate, PrimaryKeys.First(), bph + 0);

            for (int i =1; i < PrimaryKeys.Count; i++)
                condition += ", " + String.Format(cvpTemplate, PrimaryKeys[i], bph + i);

            statement = String.Format(statement, typeof(T).Name, condition);
            return statement;
        }

        String GenerateSelectText()
        {
            String statement = "SELECT * FROM {0}";
            statement = String.Format(statement, typeof(T).Name);
            return statement;
        }
        #endregion
    }
}
