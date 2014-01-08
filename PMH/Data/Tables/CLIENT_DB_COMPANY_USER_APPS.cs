using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.DataAccess.Client;

namespace Plex.PMH.Data.Tables
{
    public class CLIENT_DB_COMPANY_USER_APPS : PlexxisDataTransferObjects
    {
        public static event Subscriber OnInsert;
        public static event Subscriber OnUpdate;
        public static event Subscriber OnDelete;


        public static IEnumerable<CLIENT_DB_COMPANY_USER_APPS> GetAll()
        {
            using (var Conn = Utilities.GetConnection(true))
                return GetAll(Conn);
        }

        public static IEnumerable<CLIENT_DB_COMPANY_USER_APPS> GetAll(OracleConnection Conn)
        {
            var collection = new List<CLIENT_DB_COMPANY_USER_APPS>();
            using (var Command = new OracleCommand("SELECT * FROM CLIENT_DB_COMPANY_USER_APPS", Conn))
            using (var reader = Command.ExecuteReader())
                while (reader.Read())
                    collection.Add(new CLIENT_DB_COMPANY_USER_APPS(reader));
            return collection;
        }

        public int DB_COMPANY_USER_APP_ID;//	NUMBER(10)	N			
        public int DB_COMPANY_USER_ID;//	NUMBER(10)	N			
        public int APP_ID;//	NUMBER(10)	N			
        public int APP_USER_TYPE_ID;//	NUMBER(10)	Y			

        public CLIENT_DB_COMPANY_USER_APPS()
        {
        }

        public CLIENT_DB_COMPANY_USER_APPS(OracleDataReader reader)
        {
            DB_COMPANY_USER_APP_ID = pDB_COMPANY_USER_APP_ID(reader);
            DB_COMPANY_USER_ID = pDB_COMPANY_USER_ID(reader);
            APP_ID = pAPP_ID(reader);
            APP_USER_TYPE_ID = pAPP_USER_TYPE_ID(reader);
        }

        int pDB_COMPANY_USER_APP_ID(OracleDataReader reader)
        {
            return (reader["DB_COMPANY_USER_APP_ID"] != DBNull.Value) ? Convert.ToInt32(reader["DB_COMPANY_USER_APP_ID"]) : new int();
        }
        int pDB_COMPANY_USER_ID(OracleDataReader reader)
        {
            return (reader["DB_COMPANY_USER_ID"] != DBNull.Value) ? Convert.ToInt32(reader["DB_COMPANY_USER_ID"]) : new int();
        }
        int pAPP_ID(OracleDataReader reader)
        {
            return (reader["APP_ID"] != DBNull.Value) ? Convert.ToInt32(reader["APP_ID"]) : new int();
        }
        int pAPP_USER_TYPE_ID(OracleDataReader reader)
        {
            return (reader["APP_USER_TYPE_ID"] != DBNull.Value) ? Convert.ToInt32(reader["APP_USER_TYPE_ID"]) : new int();
        }

        public override bool Insert(OracleConnection Conn)
        {
            string sql = string.Empty;
            sql += "insert into CLIENT_DB_COMPANY_USER_APPS ";
            sql += "(DB_COMPANY_USER_APP_ID, DB_COMPANY_USER_ID, APP_ID, APP_USER_TYPE_ID) ";
            sql += "values (:a, :b, :c, :d)";

            using (var Command = new OracleCommand(sql, Conn))
            {
                DB_COMPANY_USER_APP_ID = Utilities.SequenceNextValue(Sequences.ID_GEN);
                Command.Parameters.Add(":a", OracleDbType.Int32).Value = DB_COMPANY_USER_APP_ID;
                Command.Parameters.Add(":b", OracleDbType.Int32).Value = DB_COMPANY_USER_ID;
                Command.Parameters.Add(":c", OracleDbType.Int32).Value = APP_ID;
                Command.Parameters.Add(":d", OracleDbType.Int32).Value = APP_USER_TYPE_ID;

                var r = Convert.ToBoolean(Command.ExecuteNonQuery());
                if (OnInsert != null) OnInsert(this, EventArgs.Empty);
                return r;
            }
        }

        public override bool Update(OracleConnection Conn)
        {
            string sql = string.Empty;
            sql += "update CLIENT_DB_COMPANY_USER_APPS set ";
            sql += "DB_COMPANY_USER_ID = :b, APP_ID = :c, APP_USER_TYPE_ID = :d  ";
            sql += "where DB_COMPANY_USER_APP_ID = :a ";

            using (var Command = new OracleCommand(sql, Conn))
            {
                Command.Parameters.Add(":b", OracleDbType.Int32).Value = DB_COMPANY_USER_APP_ID;
                Command.Parameters.Add(":c", OracleDbType.Int32).Value = APP_ID;
                Command.Parameters.Add(":d", OracleDbType.Int32).Value = APP_USER_TYPE_ID;
                Command.Parameters.Add(":a", OracleDbType.Int32).Value = DB_COMPANY_USER_ID;

                var r = Convert.ToBoolean(Command.ExecuteNonQuery());
                if (OnUpdate != null) OnUpdate(this, EventArgs.Empty);
                return r;

            }
        }

        public override bool Delete(OracleConnection Conn)
        {
            string sql = string.Empty;
            sql += "DELETE FROM CLIENT_DB_COMPANY_USER_APPS WHERE DB_COMPANY_USER_APP_ID = :a";
            using (var Command = new OracleCommand(sql, Conn))
            {
                Command.Parameters.Add(":a", OracleDbType.Int32).Value = DB_COMPANY_USER_APP_ID;
                var r = Convert.ToBoolean(Command.ExecuteNonQuery());
                if (OnDelete != null) OnDelete(this, EventArgs.Empty);
                return r;
            }
        }
    }
}