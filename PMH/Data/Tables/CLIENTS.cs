using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.DataAccess.Client;

namespace Plex.PMH.Data.Tables
{
    public class CLIENTS : PlexxisDataTransferObjects
    {
        //Events 
        public static event Subscriber OnInsert;
        public static event Subscriber OnUpdate;
        public static event Subscriber OnDelete;

        public static IEnumerable<CLIENTS> GetAll()
        {
            using (var Conn = Utilities.GetConnection(true))
                return GetAll(Conn);
        }

        public static IEnumerable<CLIENTS> GetAll(OracleConnection Conn)
        {
            var collection = new List<CLIENTS>();
            using (var Command = new OracleCommand("SELECT * FROM CLIENTS", Conn))
            using (var reader = Command.ExecuteReader())
                while (reader.Read())
                    collection.Add(new CLIENTS(reader));
            return collection;
        }

        public IEnumerable<CLIENT_USERS> GetCLIENT_USERS()
        {
            using (var Conn = Utilities.GetConnection(true))
                return GetCLIENT_USERS(Conn);
        }

        public IEnumerable<CLIENT_USERS> GetCLIENT_USERS(OracleConnection Conn)
        {
            //todo test this function
            var collection = new List<CLIENT_USERS>();
            using (var Command = new OracleCommand("SELECT * FROM CLIENT_USERS WHERE CLIENT_ID  = :a", Conn))
            {
                Command.Parameters.Add(":a", ResolveType(CLIENT_ID)).Value = CLIENT_ID;
                using (var reader = Command.ExecuteReader())
                    while (reader.Read())
                        collection.Add(new CLIENT_USERS(reader));
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
            //todo test this function
            var collection = new List<CLIENT_APPS>();
            using (var Command = new OracleCommand("SELECT * FROM CLIENT_APPS  WHERE CLIENT_ID  = :a", Conn))
            {
                Command.Parameters.Add(":a", ResolveType(CLIENT_ID)).Value = CLIENT_ID;
                using (var reader = Command.ExecuteReader())
                    while (reader.Read())
                        collection.Add(new CLIENT_APPS(reader));
            }
            return collection;
        }

        public IEnumerable<CLIENT_DB_COMPANIES> GetCLIENT_DB_COMPANIES()
        {
            using (var Conn = Utilities.GetConnection(true))
                return GetCLIENT_DB_COMPANIES(Conn);
        }

        public IEnumerable<CLIENT_DB_COMPANIES> GetCLIENT_DB_COMPANIES(OracleConnection Conn)
        {  
            //todo test this function
            var collection = new List<CLIENT_DB_COMPANIES>();
            using (var Command = new OracleCommand("SELECT * FROM CLIENT_DB_COMPANIES  WHERE CLIENT_ID  = :a", Conn))
            {
                Command.Parameters.Add(":a", ResolveType(CLIENT_ID)).Value = CLIENT_ID;
                using (var reader = Command.ExecuteReader())
                    while (reader.Read())
                        collection.Add(new CLIENT_DB_COMPANIES(reader));
            }
            return collection;
        }

        public int CLIENT_ID;			
        public string CLIENT_KEY;		
        public string DESCRIPTION;

        public CLIENTS() { }
        public CLIENTS(OracleDataReader reader)
        {
            CLIENT_ID = pCLIENT_ID(reader);
            DESCRIPTION = pDDESCRIPTION(reader);
            CLIENT_KEY = pCLIENT_KEY(reader);
        }

        int pCLIENT_ID(OracleDataReader reader)
        {
            return (reader["CLIENT_ID"] != DBNull.Value)?Convert.ToInt32(reader["CLIENT_ID"]): new int();
        }
        string pCLIENT_KEY(OracleDataReader reader)
        {
            return (reader["CLIENT_KEY"] != DBNull.Value) ? Convert.ToString(reader["CLIENT_KEY"]) : string.Empty;
        }
        string pDDESCRIPTION(OracleDataReader reader)
        {
            return (reader["DESCRIPTION"] != DBNull.Value) ? Convert.ToString(reader["DESCRIPTION"]) : string.Empty;
        }

        //Methods
        public override bool Insert(OracleConnection Conn)
        {
            string sql = string.Empty;
            sql += "INSERT INTO CLIENTS (CLIENT_ID, DESCRIPTION, CLIENT_KEY) VALUES (:a, :b, :c) ";

            using (var Command = new OracleCommand(sql, Conn))
            {
                CLIENT_ID = Utilities.SequenceNextValue(Sequences.ID_GEN);
                Command.Parameters.Add(":a", ResolveType(CLIENT_ID)).Value = CLIENT_ID;
                Command.Parameters.Add(":b", ResolveType(DESCRIPTION)).Value = DESCRIPTION;
                Command.Parameters.Add(":c", ResolveType(CLIENT_KEY)).Value = CLIENT_KEY;
                var r = Convert.ToBoolean(Command.ExecuteNonQuery());
                if (OnInsert != null) OnInsert(this, EventArgs.Empty);
                return r;
            }
        }
        public override bool Update(OracleConnection Conn)
        {
            string sql = string.Empty;
            sql += "UPDATE CLIENTS SET ";
            sql += "DESCRIPTION = :b, CLIENT_KEY = :c ";
            sql += "WHERE CLIENT_ID = :a";

            using (var Command = new OracleCommand(sql, Conn))
            {
                Command.Parameters.Add(":b", ResolveType(DESCRIPTION)).Value = DESCRIPTION;
                Command.Parameters.Add(":c", ResolveType(CLIENT_KEY)).Value = CLIENT_KEY;
                Command.Parameters.Add(":a", ResolveType(CLIENT_ID)).Value = CLIENT_ID;

                var r = Convert.ToBoolean(Command.ExecuteNonQuery());
                if (OnUpdate != null) OnUpdate(this, EventArgs.Empty);
                return r;
            }
        }
        public override bool Delete(OracleConnection Conn)
        {
            string sql = string.Empty;
            sql += "DELETE FROM CLIENTS ";
            sql += "WHERE CLIENT_ID = :a";

            using (var Command = new OracleCommand(sql, Conn))
            {
                Command.Parameters.Add(":a", ResolveType(CLIENT_ID)).Value = CLIENT_ID;
                var r = Convert.ToBoolean(Command.ExecuteNonQuery());
                if (OnDelete != null) OnDelete(this, EventArgs.Empty);
                return r;
            }
        }
    }
}