using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.DataAccess.Client;

namespace Plex.PMH.Data.Tables
{
    public class CLIENT_USERS : PlexxisDataTransferObjects
    {
        public static event Subscriber OnInsert;
        public static event Subscriber OnUpdate;
        public static event Subscriber OnDelete;


        public static IEnumerable<CLIENT_USERS> GetAll()
        {
            using (var Conn = Utilities.GetConnection(true))
                return GetAll(Conn);
        }

        public static IEnumerable<CLIENT_USERS> GetAll(OracleConnection Conn)
        {
            var collection = new List<CLIENT_USERS>();
            using (var Command = new OracleCommand("SELECT * FROM CLIENT_USERS", Conn))
            using (var reader = Command.ExecuteReader())
                while (reader.Read())
                    collection.Add(new CLIENT_USERS(reader));
            return collection;
        }

        public IEnumerable<CLIENT_DB_COMPANY_USERS> GetCLIENT_DB_COMPANY_USERS()
        {
            using (var Conn = Utilities.GetConnection(true))
                return GetCLIENT_DB_COMPANY_USERS(Conn);
        }
        public IEnumerable<CLIENT_DB_COMPANY_USERS> GetCLIENT_DB_COMPANY_USERS(OracleConnection Conn)
        {
            var collection = new List<CLIENT_DB_COMPANY_USERS>();
            using (var Command = new OracleCommand("SELECT * FROM CLIENT_DB_COMPANY_USERS WHERE CLIENT_ID = :a", Conn))
            {
                Command.Parameters.Add(":a", ResolveType(CLIENT_ID)).Value = CLIENT_ID;
                using (var reader = Command.ExecuteReader())
                    while (reader.Read())
                        collection.Add(new CLIENT_DB_COMPANY_USERS(reader));
            }
            return collection;
        }


        public int USER_ID;//	NUMBER(10)	N			
        public int CLIENT_ID;//NUMBER(10)	N			
        public string NAME;//	VARCHAR2(25)	N			
        public string PASSWORD;//VARCHAR2(25)	Y			

        public CLIENT_USERS()
        {
        }

        public CLIENT_USERS(OracleDataReader reader)
        {
            USER_ID = pUSER_ID(reader);
            CLIENT_ID = pCLIENT_ID(reader);
            NAME = pNAME(reader);
            PASSWORD = pPASSWORD(reader);
        }

        int pUSER_ID(OracleDataReader reader)
        {
            return (reader["USER_ID"] != DBNull.Value) ? Convert.ToInt32(reader["USER_ID"]) : new int();
        }
        int pCLIENT_ID(OracleDataReader reader)
        {
            return (reader["CLIENT_ID"] != DBNull.Value) ? Convert.ToInt32(reader["CLIENT_ID"]) : new int();
        }
        string pNAME(OracleDataReader reader)
        {
            return (reader["NAME"] != DBNull.Value) ? Convert.ToString(reader["NAME"]) : string.Empty;
        }
        string pPASSWORD(OracleDataReader reader)
        {
            return (reader["PASSWORD"] != DBNull.Value) ? Convert.ToString(reader["PASSWORD"]) : string.Empty;
        }

        public override bool Insert(OracleConnection Conn)
        {
            string sql = string.Empty;
            sql += "insert into CLIENT_USERS(USER_ID, CLIENT_ID, NAME, PASSWORD) ";
            sql += "values (:a,:b,:c,:d) ";

            using (var Command = new OracleCommand(sql, Conn))
            {
                USER_ID = Utilities.SequenceNextValue(Sequences.ID_GEN);
                Command.Parameters.Add(":a", OracleDbType.Int32).Value = USER_ID;
                Command.Parameters.Add(":b", OracleDbType.Int32).Value = CLIENT_ID;
                Command.Parameters.Add(":c", OracleDbType.Varchar2).Value = NAME;
                Command.Parameters.Add(":d", OracleDbType.Varchar2).Value = PASSWORD;

                var r = Convert.ToBoolean(Command.ExecuteNonQuery());
                if (OnInsert != null) OnInsert(this, EventArgs.Empty);
                return r;
            }
        }

        public override bool Update(OracleConnection Conn)
        {
            string sql = string.Empty;
            sql += "update CLIENT_USERS set ";
            sql += "CLIENT_ID = :b, NAME = :c, PASSWORD = :d ";
            sql += "where USER_ID = :a ";

            using (var Command = new OracleCommand(sql, Conn))
            {
                Command.Parameters.Add(":b", OracleDbType.Int32).Value = CLIENT_ID;
                Command.Parameters.Add(":c", OracleDbType.Varchar2).Value = NAME;
                Command.Parameters.Add(":d", OracleDbType.Varchar2).Value = PASSWORD;
                Command.Parameters.Add(":a", OracleDbType.Int32).Value = USER_ID;

                var r = Convert.ToBoolean(Command.ExecuteNonQuery());
                if (OnUpdate != null) OnUpdate(this, EventArgs.Empty);
                return r;
            }
        }

        public override bool Delete(OracleConnection Conn)
        {
            string sql = string.Empty;
            sql += "DELETE FROM CLIENT_USERS WHERE USER_ID = :a";
            using (var Command = new OracleCommand(sql, Conn))
            {
                Command.Parameters.Add(":a", OracleDbType.Int32).Value = USER_ID;
                var r = Convert.ToBoolean(Command.ExecuteNonQuery());
                if (OnDelete != null) OnDelete(this, EventArgs.Empty);
                return r;
            }
        }
    }
}