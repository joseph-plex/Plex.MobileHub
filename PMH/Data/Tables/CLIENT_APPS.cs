using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.DataAccess.Client;

namespace Plex.PMH.Data.Tables
{
    public class CLIENT_APPS : PlexxisDataTransferObjects
    {
        public static event Subscriber OnInsert;
        public static event Subscriber OnUpdate;
        public static event Subscriber OnDelete;

        public int CLIENT_APP_ID;//	NUMBER(10)	N			
        public int APP_ID;//	NUMBER(10)	N			
        public int CLIENT_ID;//	NUMBER(10)	N	

        public static IEnumerable<CLIENT_APPS> GetAll()
        {
            using (var Conn = Utilities.GetConnection(true))
                return GetAll(Conn);
        }

        public static IEnumerable<CLIENT_APPS> GetAll(OracleConnection Conn)
        {
            var collection = new List<CLIENT_APPS>();
            using (var Command = new OracleCommand("SELECT * FROM CLIENT_APPS", Conn))
            using (var reader = Command.ExecuteReader())
                while (reader.Read())
                    collection.Add(new CLIENT_APPS(reader));
            return collection;
        }

        public CLIENT_APPS()
        {
        }

        public CLIENT_APPS(OracleDataReader reader)
        {
            CLIENT_APP_ID = pCLIENT_APP_ID(reader);
            APP_ID = pAPP_ID(reader);
            CLIENT_ID = pCLIENT_ID(reader);
        }

        int pCLIENT_APP_ID(OracleDataReader reader)
        {
            return (reader["CLIENT_APP_ID"] != DBNull.Value) ? Convert.ToInt32(reader["CLIENT_APP_ID"]) : new int();
        }
        int pAPP_ID(OracleDataReader reader)
        {
            return (reader["APP_ID"] != DBNull.Value) ? Convert.ToInt32(reader["APP_ID"]) : new int();
        }
        int pCLIENT_ID(OracleDataReader reader)
        {
            return (reader["CLIENT_ID"] != DBNull.Value) ? Convert.ToInt32(reader["CLIENT_ID"]) : new int();
        }

        public override bool Insert(OracleConnection Conn)
        {
            string sql = string.Empty;
            sql += "INSERT INTO APP_CLIENTS (CLIENT_APP_ID,APP_ID,CLIENT_ID)";
            sql += "VALUES(:a, :b, :c)";

            using (var Command = new OracleCommand(sql, Conn))
            {
                CLIENT_APP_ID = Utilities.SequenceNextValue(Sequences.ID_GEN);
                Command.Parameters.Add(":a", ResolveType(CLIENT_APP_ID)).Value = CLIENT_APP_ID;
                Command.Parameters.Add(":b", ResolveType(APP_ID)).Value = APP_ID;
                Command.Parameters.Add(":c", ResolveType(CLIENT_ID)).Value = CLIENT_ID;

                var r = Convert.ToBoolean(Command.ExecuteNonQuery());
                if (OnInsert != null) OnInsert(this, EventArgs.Empty);
                return r;
            }
        }

        public override bool Update(OracleConnection Conn)
        {
            string sql = string.Empty;
            sql += "UPDATE APP_CLIENTS set ";
            sql += "APP_ID = :b, CLIENT_ID = :c ";
            sql += "WHERE CLIENT_APP_ID = :a";

            using (var Command = new OracleCommand(sql, Conn))
            {
                Command.Parameters.Add(":b", ResolveType(APP_ID)).Value = APP_ID;
                Command.Parameters.Add(":c", ResolveType(CLIENT_ID)).Value = CLIENT_ID;
                Command.Parameters.Add(":a", ResolveType(CLIENT_APP_ID)).Value = CLIENT_APP_ID;

                var r = Convert.ToBoolean(Command.ExecuteNonQuery());
                if (OnUpdate != null) OnUpdate(this, EventArgs.Empty);
                return r;
            }
        }

        public override bool Delete(OracleConnection Conn)
        {

            string sql = string.Empty;
            sql += "DELETE FROM APP_CLIENTS WHERE CLIENT_APP_ID = :a";

            using (var Command = new OracleCommand(sql, Conn))
            {
                Command.Parameters.Add(":a", ResolveType(CLIENT_APP_ID)).Value = CLIENT_APP_ID;
                
                var r = Convert.ToBoolean(Command.ExecuteNonQuery());
                if (OnDelete != null) OnDelete(this, EventArgs.Empty);
                return r;
            }
        }
    }
}