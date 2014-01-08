using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.DataAccess.Client;

namespace Plex.PMH.Data.Tables
{
    public class CLIENT_DB_COMPANY_USERS : PlexxisDataTransferObjects
    {
        public static event Subscriber OnInsert;
        public static event Subscriber OnUpdate;
        public static event Subscriber OnDelete;


        public static IEnumerable<CLIENT_DB_COMPANY_USERS> GetAll()
        {
            using (var Conn = Utilities.GetConnection(true))
                return GetAll(Conn);
        }

        public static IEnumerable<CLIENT_DB_COMPANY_USERS> GetAll(OracleConnection Conn)
        {
            var collection = new List<CLIENT_DB_COMPANY_USERS>();
            using (var Command = new OracleCommand("SELECT * FROM CLIENT_DB_COMPANY_USERS", Conn))
            using (var reader = Command.ExecuteReader())
                while (reader.Read())
                    collection.Add(new CLIENT_DB_COMPANY_USERS(reader));
            return collection;
        }


        public int DB_COMPANY_USER_ID	;//NUMBER(10)	N			
        public int DB_COMPANY_ID;//	NUMBER(10)	N			
        public int USER_ID	;//NUMBER(10)	N			
        public string CONNECT_AS;//	VARCHAR2(100)	Y		

        public CLIENT_DB_COMPANY_USERS()
        {
        }

        public CLIENT_DB_COMPANY_USERS(OracleDataReader reader)
        {
            DB_COMPANY_USER_ID = pDB_COMPANY_USER_ID(reader);
            DB_COMPANY_ID = pDB_COMPANY_ID(reader);
            USER_ID = pUSER_ID(reader);
            CONNECT_AS = pCONNECT_AS(reader);
        }

        int pDB_COMPANY_USER_ID(OracleDataReader reader)
        {
            return (reader["DB_COMPANY_USER_ID"] != DBNull.Value) ? Convert.ToInt32(reader["DB_COMPANY_USER_ID"]) : new int();
        }
        int pDB_COMPANY_ID(OracleDataReader reader)
        {
            return (reader["DB_COMPANY_ID"] != DBNull.Value) ? Convert.ToInt32(reader["DB_COMPANY_ID"]) : new int();
        }
        int pUSER_ID(OracleDataReader reader)
        {
            return (reader["USER_ID"] != DBNull.Value) ? Convert.ToInt32(reader["USER_ID"]) : new int();
        }
        string pCONNECT_AS(OracleDataReader reader)
        {
            return (reader["CONNECT_AS"] != DBNull.Value) ? Convert.ToString(reader["CONNECT_AS"]) : string.Empty;
        }

        public override bool Insert(OracleConnection Conn)
        {
            string sql = string.Empty;
            sql += "insert into client_db_company_users ";
            sql += "(DB_COMPANY_USER_ID, DB_COMPANY_ID, USER_ID, CONNECT_AS) ";
            sql += "values(:a,:b,:c, :d)";

            using (var Command = new OracleCommand(sql, Conn))
            {
                DB_COMPANY_USER_ID = Utilities.SequenceNextValue(Sequences.ID_GEN);
                Command.Parameters.Add(":a", OracleDbType.Int32).Value = DB_COMPANY_USER_ID;
                Command.Parameters.Add(":b", OracleDbType.Int32).Value = DB_COMPANY_ID;
                Command.Parameters.Add(":c", OracleDbType.Int32).Value = USER_ID;
                Command.Parameters.Add(":d", OracleDbType.Varchar2).Value = CONNECT_AS;

                var r = Convert.ToBoolean(Command.ExecuteNonQuery());
                if (OnInsert != null) OnInsert(this, EventArgs.Empty);
                return r;
            }
        }

        public override bool Update(OracleConnection Conn)
        {
            string sql = string.Empty;

            sql += "update client_db_company_users set ";
            sql += "DB_COMPANY_ID = :b, USER_ID = :c, CONNECT_A ";
            sql += "where DB_COMPANY_USER_ID = :a";

            using (var Command = new OracleCommand(sql, Conn))
            {
                Command.Parameters.Add(":b", OracleDbType.Int32).Value = DB_COMPANY_ID;
                Command.Parameters.Add(":c", OracleDbType.Int32).Value = USER_ID;
                Command.Parameters.Add(":d", OracleDbType.Varchar2).Value = CONNECT_AS;
                Command.Parameters.Add(":a", OracleDbType.Int32).Value = DB_COMPANY_USER_ID;

                var r = Convert.ToBoolean(Command.ExecuteNonQuery());
                if (OnUpdate != null) OnUpdate(this, EventArgs.Empty);
                return r;
            }
        }

        public override bool Delete(OracleConnection Conn)
        {
            string sql = string.Empty;
            sql += "DELETE FROM CLIENT_DB_COMPANY_USERS WHERE DB_COMPANY_USER_ID = :a";
            using (var Command = new OracleCommand(sql, Conn))
            {
                Command.Parameters.Add(":a", OracleDbType.Int32).Value = DB_COMPANY_USER_ID;
                var r = Convert.ToBoolean(Command.ExecuteNonQuery());
                if (OnDelete != null) OnDelete(this, EventArgs.Empty);
                return r;
            }
        }
    }
}