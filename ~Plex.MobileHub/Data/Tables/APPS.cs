using System;
using System.Data;
using System.Collections.Generic;

namespace Plex.MobileHub.Data.Tables
{
    public class APPS : PlexxisDataTransferObjects
    {
        public static event EventHandler OnInsert;
        public static event EventHandler OnUpdate;
        public static event EventHandler OnDelete;

        public static IEnumerable<APPS> GetAll()
        {
            using (var Conn = Utilities.GetConnection(true))
                return GetAll(Conn);
        }
        public static IEnumerable<APPS> GetAll(IDbConnection Conn)
        {
            var collection = new List<APPS>();
            using (var Command = Conn.CreateCommand("SELECT * FROM APPS"))
            using (var reader = Command.ExecuteReader())
                while (reader.Read())
                    collection.Add(new APPS(reader));
            return collection;
        }

        public IEnumerable<APP_QUERIES> GetAPP_QUERIES()
        {
            using (var Conn = Utilities.GetConnection(true))
                return GetAPP_QUERIES(Conn);
        }
        public IEnumerable<APP_QUERIES> GetAPP_QUERIES(IDbConnection Conn)
        {
            var collection = new List<APP_QUERIES>();
            using (var Command = Conn.CreateCommand("SELECT * FROM APP_QUERIES where APP_ID = :a", APP_ID))
            using (var reader = Command.ExecuteReader())
                while (reader.Read())
                    collection.Add(new APP_QUERIES(reader));
            return collection;
        }

        public IEnumerable<APP_USER_TYPES> GetAPP_USER_TYPES()
        {
            using (var Conn = Utilities.GetConnection(true))
                return GetAPP_USER_TYPES(Conn);
        }
        public IEnumerable<APP_USER_TYPES> GetAPP_USER_TYPES(IDbConnection Conn)
        {
            var collection = new List<APP_USER_TYPES>();
            using (var Command = Conn.CreateCommand("SELECT * FROM APP_USER_TYPES where APP_ID = :a", APP_ID))
            using (var reader = Command.ExecuteReader())
                while (reader.Read())
                    collection.Add(new APP_USER_TYPES(reader));
            return collection;

        }

        public IEnumerable<APP_TABLES> GetAPP_TABLES()
        {
            using (var Conn = Utilities.GetConnection(true))
                return GetAPP_TABLES(Conn);
        }
        public IEnumerable<APP_TABLES> GetAPP_TABLES(IDbConnection Conn)
        {
            var collection = new List<APP_TABLES>();
            using (var Command = Conn.CreateCommand("SELECT * FROM APP_TABLES where APP_ID = :a", APP_ID))
            using (var reader = Command.ExecuteReader())
                while (reader.Read())
                    collection.Add(new APP_TABLES(reader));
            return collection;
        }

        public IEnumerable<CLIENT_APPS> GetCLIENT_APPS()
        {
            using (var Conn = Utilities.GetConnection(true))
                return GetCLIENT_APPS(Conn);
        }
        public IEnumerable<CLIENT_APPS> GetCLIENT_APPS(IDbConnection Conn)
        {
            var collection = new List<CLIENT_APPS>();
            using (var Command = Conn.CreateCommand("SELECT * FROM CLIENT_APPS where APP_ID = :a", APP_ID))
            using (var reader = Command.ExecuteReader())
                while (reader.Read())
                    collection.Add(new CLIENT_APPS(reader));
            return collection;
        }

        public IEnumerable<CLIENT_DB_COMPANY_USER_APPS> GetCLIENT_DB_COMPANY_USER_APPS()
        {
            using (var Conn = Utilities.GetConnection(true))
                return GetCLIENT_DB_COMPANY_USER_APPS(Conn);
        }
        public IEnumerable<CLIENT_DB_COMPANY_USER_APPS> GetCLIENT_DB_COMPANY_USER_APPS(IDbConnection Conn)
        {
            var collection = new List<CLIENT_DB_COMPANY_USER_APPS>();
            using (var Command = Conn.CreateCommand("SELECT * FROM CLIENT_DB_COMPANY_USER_APPS where APP_ID = :a", APP_ID))
            using (var reader = Command.ExecuteReader())
                while (reader.Read())
                    collection.Add(new CLIENT_DB_COMPANY_USER_APPS(reader));
            return collection;
        }

        public int APP_ID { get; set; }
        public string AUTH_KEY { get; set; }
        public string TITLE { get; set; }
        public string DESCRIPTION { get; set; }
        public int IS_CLIENT_CUSTOM_APP { get; set; }

        public APPS() : base() 
        {
            PrimaryKey.Add("APP_ID");
        }
        public APPS(IDataReader reader) : this()
        {
            AutoFill(reader, this);
        }
        public override bool Insert(IDbConnection Conn)
        {
            var r = base.Insert(Conn);
            if (OnInsert != null)
                OnInsert(this, EventArgs.Empty);
            return r;
        }

        public override bool Update(IDbConnection Conn)
        {
            var r = base.Update(Conn);
            if (OnUpdate != null)
                OnUpdate(this, EventArgs.Empty);
            return r;
        }

        public override bool Delete(IDbConnection Conn)
        {
            var r = base.Delete(Conn);
            if (OnDelete != null)
                OnDelete(this, EventArgs.Empty);
            return r;
        }
    }
}