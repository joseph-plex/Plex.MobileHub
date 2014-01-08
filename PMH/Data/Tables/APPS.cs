using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.DataAccess.Client;

namespace Plex.PMH.Data.Tables
{
    public class APPS : PlexxisDataTransferObjects
    {
        public static event Subscriber OnInsert;
        public static event Subscriber OnUpdate;
        public static event Subscriber OnDelete;

        public static IEnumerable<APPS> GetAll()
        {
            using (var Conn = Utilities.GetConnection(true))
                return GetAll(Conn);
        }
        public static IEnumerable<APPS> GetAll(OracleConnection Conn)
        {
            var collection = new List<APPS>();
            using (var Command = new OracleCommand("SELECT * FROM APPS", Conn))
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
        public IEnumerable<APP_QUERIES> GetAPP_QUERIES(OracleConnection Conn)
        {
            var collection = new List<APP_QUERIES>();
            using (var Command = new OracleCommand("SELECT * FROM APP_QUERIES where APP_ID = :a", Conn))
            {
                Command.Parameters.Add(":a", ResolveType(APP_ID)).Value = APP_ID;
                using (var reader = Command.ExecuteReader())
                    while (reader.Read())
                        collection.Add(new APP_QUERIES(reader));
            }
            return collection;
        }

        public IEnumerable<APP_USER_TYPES> GetAPP_USER_TYPES()
        {
            using (var Conn = Utilities.GetConnection(true))
                return GetAPP_USER_TYPES(Conn);
        }
        public IEnumerable<APP_USER_TYPES> GetAPP_USER_TYPES(OracleConnection Conn)
        {
            var collection = new List<APP_USER_TYPES>();
            using (var Command = new OracleCommand("SELECT * FROM APP_USER_TYPES where APP_ID = :a", Conn))
            {
                Command.Parameters.Add(":a", ResolveType(APP_ID)).Value = APP_ID;
                using (var reader = Command.ExecuteReader())
                    while (reader.Read())
                        collection.Add(new APP_USER_TYPES(reader));
            }
            return collection;

        }

        public IEnumerable<APP_TABLES> GetAPP_TABLES()
        {
            using (var Conn = Utilities.GetConnection(true))
                return GetAPP_TABLES(Conn);
        }
        public IEnumerable<APP_TABLES> GetAPP_TABLES(OracleConnection Conn)
        {
            var collection = new List<APP_TABLES>();
            //todo test this
            using (var Command = new OracleCommand("SELECT * FROM APP_TABLES where APP_ID = :a", Conn))
            {
                Command.Parameters.Add(":a", ResolveType(APP_ID)).Value = APP_ID;
                using (var reader = Command.ExecuteReader())
                    while (reader.Read())
                        collection.Add(new APP_TABLES(reader));
            }
            return collection;
        }

        public IEnumerable<CLIENT_APPS> GetCLIENT_APPS()
        {
            using (var Conn = Utilities.GetConnection(true))
                return GetCLIENT_APPS(Conn);
        }
        public IEnumerable<CLIENT_APPS> GetCLIENT_APPS(OracleConnection Conn)
        {
            var collection = new List<CLIENT_APPS>();
            //todo test this
            using (var Command = new OracleCommand("SELECT * FROM CLIENT_APPS where APP_ID = :a", Conn))
            {
                Command.Parameters.Add(":a", ResolveType(APP_ID)).Value = APP_ID;
                using (var reader = Command.ExecuteReader())
                    while (reader.Read())
                        collection.Add(new CLIENT_APPS(reader));
            }
            return collection;
        }

        public IEnumerable<CLIENT_DB_COMPANY_USER_APPS> GetCLIENT_DB_COMPANY_USER_APPS()
        {
            using (var Conn = Utilities.GetConnection(true))
                return GetCLIENT_DB_COMPANY_USER_APPS(Conn);
        }
        public IEnumerable<CLIENT_DB_COMPANY_USER_APPS> GetCLIENT_DB_COMPANY_USER_APPS(OracleConnection Conn)
        {
            var collection = new List<CLIENT_DB_COMPANY_USER_APPS>();
            //todo test this
            using (var Command = new OracleCommand("SELECT * FROM CLIENT_DB_COMPANY_USER_APPS where APP_ID = :a", Conn))
            {
                Command.Parameters.Add(":a", ResolveType(APP_ID)).Value = APP_ID;
                using (var reader = Command.ExecuteReader())
                    while (reader.Read())
                        collection.Add(new CLIENT_DB_COMPANY_USER_APPS(reader));
            }
            return collection;
        }

        public int APP_ID;
        public string AUTH_KEY;
        public string TITLE;
        public string DESCRIPTION;
        public int IS_CLIENT_CUSTOM_APP;

        public APPS(){ }
        public APPS(OracleDataReader reader)
        {
            APP_ID = pAPP_ID(reader);
            AUTH_KEY = pAUTH_KEY(reader);
            TITLE = pTITLE(reader);
            DESCRIPTION = pDESCRIPTION(reader);
            IS_CLIENT_CUSTOM_APP = pIS_CLIENT_CUSTOM_APP(reader);
        }

        int pAPP_ID(OracleDataReader reader)
        {
            return (reader["APP_ID"] != DBNull.Value) ? Convert.ToInt32(reader["APP_ID"]) : new int();
        }
        string pAUTH_KEY(OracleDataReader reader)
        {
            return (reader["AUTH_KEY"] != DBNull.Value) ? Convert.ToString(reader["AUTH_KEY"]) : string.Empty;
        }
        string pTITLE(OracleDataReader reader)
        {
            return (reader["TITLE"] != DBNull.Value) ? Convert.ToString(reader["TITLE"]) : string.Empty;
        }
        string pDESCRIPTION(OracleDataReader reader)
        {
            return (reader["DESCRIPTION"] != DBNull.Value) ? Convert.ToString(reader["DESCRIPTION"]) : string.Empty;
        }
        int pIS_CLIENT_CUSTOM_APP(OracleDataReader reader)
        {
            return (reader["IS_CLIENT_CUSTOM_APP"] != DBNull.Value) ? Convert.ToInt32(reader["IS_CLIENT_CUSTOM_APP"]) : new int();
        }

        public override bool Insert(OracleConnection Conn)
        {
            string sql = string.Empty;
            sql += "INSERT INTO APPS (APP_ID, AUTH_KEY, TITLE, DESCRIPTION, IS_CLIENT_CUSTOM_APP)";
            sql += "VALUES(:a, :b, :c, :d, :e)";

            using (var Command = new OracleCommand(sql, Conn))
            {
                APP_ID = Utilities.SequenceNextValue(Sequences.ID_GEN);
                Command.Parameters.Add(":a", ResolveType(APP_ID)).Value = APP_ID;
                Command.Parameters.Add(":b", ResolveType(AUTH_KEY)).Value = AUTH_KEY;
                Command.Parameters.Add(":c", ResolveType(TITLE)).Value = TITLE;
                Command.Parameters.Add(":d", ResolveType(DESCRIPTION)).Value = DESCRIPTION;
                Command.Parameters.Add(":e", ResolveType(IS_CLIENT_CUSTOM_APP)).Value = IS_CLIENT_CUSTOM_APP;

                var r = Convert.ToBoolean(Command.ExecuteNonQuery());
                if (OnInsert != null) OnInsert(this, EventArgs.Empty);
                return r;
            }
        }
        public override bool Update(OracleConnection Conn)
        {
            string sql = string.Empty;
            sql += "UPDATE APPS SET ";
            sql += "AUTH_KEY = :b, TITLE = :c, DESCRIPTION = :d, IS_CLIENT_CUSTOM_APP = :e ";
            sql += "WHERE APP_ID = :a";

            using (var Command = new OracleCommand(sql, Conn))
            {
                Command.Parameters.Add(":b", ResolveType(AUTH_KEY)).Value = AUTH_KEY;
                Command.Parameters.Add(":c", ResolveType(TITLE)).Value = TITLE;
                Command.Parameters.Add(":d", ResolveType(DESCRIPTION)).Value = DESCRIPTION;
                Command.Parameters.Add(":e", ResolveType(IS_CLIENT_CUSTOM_APP)).Value = IS_CLIENT_CUSTOM_APP;
                Command.Parameters.Add(":a", ResolveType(APP_ID)).Value = APP_ID;

                var r = Convert.ToBoolean(Command.ExecuteNonQuery());
                if (OnUpdate != null) OnUpdate(this, EventArgs.Empty);
                return r;
            }
        }
        public override bool Delete(OracleConnection Conn)
        {
            string sql = string.Empty;
            sql += "DELETE FROM APPS ";
            sql += "WHERE APP_ID = :a";

            using (var Command = new OracleCommand(sql, Conn))
            {
                Command.Parameters.Add(":a", ResolveType(APP_ID)).Value = APP_ID;

                var r = Convert.ToBoolean(Command.ExecuteNonQuery());
                if (OnDelete != null) OnDelete(this, EventArgs.Empty);
                return r;
            }
        }
    }
}