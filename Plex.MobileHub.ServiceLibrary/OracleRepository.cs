using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plex.Data;
using System.Data;
using Oracle.DataAccess.Client;

namespace Plex.MobileHub.ServiceLibrary
{
    public class OracleRepository : IDisposable
    {
        private const string User = "C##PMH";
        private const string Pass = "!!!plex!!!sa";
        private const string Source = "(DESCRIPTION = (ADDRESS_LIST = (ADDRESS = (PROTOCOL = TCP)(HOST = 10.0.1.96)(PORT = 1521)))(CONNECT_DATA = (SERVICE_NAME = PLE1LIVE)))";
        private const string ConnectionString = "User Id=" + User + ";" + "Password=" + Pass + ";" + "Data Source=" + Source + ";";

        public static IDbConnection GetOpenIDbConnection()
        {
            return new OracleConnection(ConnectionString).OpenConnection();
        }

        protected static IEnumerable<String> GetEntryPropertyNames(Type type)
        {
            foreach (var propInfo in type.GetProperties())
                yield return propInfo.Name;
        }

        public Boolean Disposed { get; protected set; }
        protected IDbConnection dbConn { get; set; }

        public OracleRepository()
        {
            Disposed = false;
        }
        public IDbConnection Open()
        {
            Close();
            dbConn = GetOpenIDbConnection();
            return dbConn;
        }
        public IDbTransaction BeginTransaction()
        {
            return dbConn.BeginTransaction();
        }

        public void Close()
        {
            if (dbConn != null)
            {
                dbConn.Close();
                dbConn.Dispose();
                dbConn = null;
            }
        }

        #region IDisposable Pattern
        protected virtual void Dispose(Boolean disposing)
        {
            if (Disposed)
                if (disposing)
                    if (dbConn != null)
                        dbConn.Dispose();
            Disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
        #region Finalizer
        ~OracleRepository()
        {
            Dispose(false);
        }
        #endregion
    }

    public class OracleRepository<T> : OracleRepository, IDisposable, IRepository<T> where T : IRepositoryEntry, new()
    {


        #region Public EventHandlers
        public event EventHandler<RepositoryOperationEventArgs> InsertEvent;
        public event EventHandler<RepositoryOperationEventArgs> UpdateEvent;
        public event EventHandler<RepositoryOperationEventArgs> DeleteEvent;
        #endregion

        #region Public Properties
        public IList<String> PrimaryKeys { get { return primaryKeys.AsReadOnly(); } }
        public IList<String> Properties { get; private set; }
        public String InsertText
        {
            get
            {
                return (String.IsNullOrEmpty(insertText)) ? InsertText = GenerateInsertText() : insertText;
            }
            private set
            {
                insertText = value;
            }
        }
        public String UpdateText
        {
            get
            {
                return (String.IsNullOrEmpty(updateText)) ? updateText = GenerateUpdateText() : updateText;
            }
            private set
            {
                updateText = value;
            }
        }
        public String DeleteText
        {
            get
            {
                return (String.IsNullOrEmpty(deleteText)) ? deleteText = GenerateDeleteText() : deleteText;
            }
            private set
            {
                deleteText = value;
            }
        }
        public String SelectText
        {
            get
            {
                return (String.IsNullOrEmpty(selectText)) ? selectText = GenerateSelectText() : selectText;
            }
            private set
            {
                selectText = value;
            }
        }

        #endregion
        #region Private fields
        protected String insertText;
        protected String selectText;
        protected String updateText;
        protected String deleteText;
        List<String> primaryKeys;
        #endregion
        #region Constructor(s)
        public OracleRepository()
            : base()
        {
            primaryKeys = new List<String>(new T().GetPrimaryKeys());
            Properties = new List<String>(GetEntryPropertyNames(typeof(T))).AsReadOnly();
        }

        #endregion
        #region Public Behavior(s)
        public void Insert(IDbConnection connection, T entry)
        {
            connection.NonQuery(InsertText, Properties.Select(p => typeof(T).GetProperty(p).GetValue(entry)).ToArray());
            if (InsertEvent != null) InsertEvent(this, new RepositoryOperationEventArgs() { Entry = entry });
        }
        public void Update(IDbConnection connection, T entry)
        {
            if (entry == null)
                throw new NullReferenceException();

            connection.NonQuery(UpdateText, Properties.Where(p => !primaryKeys.Any(k => k == p)).Concat(primaryKeys).Select(p => typeof(T).GetProperty(p).GetValue(entry)).ToArray());
            if (UpdateEvent != null) UpdateEvent(this, new RepositoryOperationEventArgs() { Entry = entry });
        }
        public void Delete(IDbConnection connection, Predicate<T> predicate)
        {
            foreach (var entry in RetrieveAll(connection).Where(new Func<T, bool>(predicate)))
            {
                connection.NonQuery(DeleteText, primaryKeys.Select(p => typeof(T).GetProperty(p).GetValue(entry)).ToArray());
                if (DeleteEvent != null) DeleteEvent(this, new RepositoryOperationEventArgs() { Entry = null });
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
            return connection.Query(SelectText).Tuples.Select(p => RepositoryEntryBase.FromPlexQueryResultTuple(new T(), p)).Cast<T>();
        }
        #endregion
        #region IRepository Behavior(s)
        public void Insert(T entry)
        {

            Insert((dbConn == null) ? Open() : dbConn, entry);
        }
        public void Update(T entry)
        {
            Update((dbConn == null) ? Open() : dbConn, entry);
        }

        public void Delete(Predicate<T> predicate)
        {
            Delete((dbConn == null) ? Open() : dbConn, predicate);
        }

        public T Retrieve(Predicate<T> predicate)
        {
            return Retrieve((dbConn == null) ? Open() : dbConn, predicate);
        }
        public bool Exists(Predicate<T> predicate)
        {
            return Exists((dbConn == null) ? Open() : dbConn, predicate);
        }

        public IEnumerable<T> RetrieveAll()
        {
            return RetrieveAll((dbConn == null) ? Open() : dbConn);
        }

        public int Count()
        {
            return RetrieveAll().Count();
        }

        public int Count(Predicate<T> predicate)
        {
            return RetrieveAll().Count(new Func<T, Boolean>(predicate));
        }
        #endregion

        #region Private Behavior(s)
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
            String cvp = String.Empty;
            String condition = String.Empty;
            //Can only set Cols that are not a primary Keys, Get those Columns
            var Settables = Properties.Where(p => !PrimaryKeys.Any(k => k == p)).ToList();

            try
            {
                cvp = String.Format(cvpTemplate, Settables.First(), bph + 0);
                condition = String.Format(cvpTemplate, PrimaryKeys.First(), bph + Settables.Count);
            }
            catch (Exception)
            {
                return String.Empty;
            }

            //These are the values to be set | Start @ 1 since first entry is done above.
            for (int i = 1; i < Settables.Count; i++)
                cvp += ", " + String.Format(cvpTemplate, Settables[i], bph + i);

            //This creates the conditions under which the values are set. | Start @ 1 since first entry is done above.
            for (int i = Settables.Count + 1; i < Properties.Count; i++)
                condition += " AND " + String.Format(cvpTemplate, PrimaryKeys[i - Settables.Count], bph + i);

            statement = String.Format(statement, typeof(T).Name, cvp, condition);
            return statement;
        }
        String GenerateDeleteText()
        {
            String bph = ":a";
            String cvpTemplate = "{0} = {1}";
            String statement = "DELETE FROM {0} WHERE {1}";
            String condition = String.Format(cvpTemplate, PrimaryKeys.First(), bph + 0);

            for (int i = 1; i < PrimaryKeys.Count; i++)
                condition += " AND " + String.Format(cvpTemplate, PrimaryKeys[i], bph + i);

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


        public IList<string> GetPrimaryKeys()
        {
            return PrimaryKeys;
        }
    }
}
