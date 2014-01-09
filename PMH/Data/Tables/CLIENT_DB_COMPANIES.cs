using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.DataAccess.Client;

namespace Plex.PMH.Data.Tables
{
    public class CLIENT_DB_COMPANIES : PlexxisDataTransferObjects
    {
        public static event Subscriber OnInsert;
        public static event Subscriber OnUpdate;
        public static event Subscriber OnDelete;

        public static IEnumerable<CLIENT_DB_COMPANIES> GetAll()
        {
            using (var Conn = Utilities.GetConnection(true))
                return GetAll(Conn);
        }

        public static IEnumerable<CLIENT_DB_COMPANIES> GetAll(OracleConnection Conn)
        {
            var collection = new List<CLIENT_DB_COMPANIES>();
            using (var Command = new OracleCommand("SELECT * FROM CLIENT_DB_COMPANIES", Conn))
            using (var reader = Command.ExecuteReader())
                while (reader.Read())
                    collection.Add(new CLIENT_DB_COMPANIES(reader));
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
            //todo test
            using (var Command = new OracleCommand("SELECT * FROM CLIENT_DB_COMPANY_USERS WHERE DB_COMPANY_ID = :a", Conn))
            {
                Command.Parameters.Add(":a", ResolveType(DB_COMPANY_ID)).Value = DB_COMPANY_ID;
                using (var reader = Command.ExecuteReader())
                    while (reader.Read())
                        collection.Add(new CLIENT_DB_COMPANY_USERS(reader));
            }
            return collection;
        }

        public int DB_COMPANY_ID;//	NUMBER(10)	N			
        public string DATABASE_SID;//	VARCHAR2(25)	Y			
        public int? COMPANY_ID;//	NUMBER(10)	Y			
        public string COMPANY_CODE;//	VARCHAR2(12)	Y			
        public int CLIENT_ID;//	NUMBER(10)	N			

        public CLIENT_DB_COMPANIES()
        { 
        }

        public CLIENT_DB_COMPANIES(OracleDataReader reader)
        {
            DB_COMPANY_ID = pDB_COMPANY_ID(reader);
            DATABASE_SID = pDATABASE_SID(reader);
            COMPANY_ID = pCOMPANY_ID(reader);
            COMPANY_CODE = pCOMPANY_CODE(reader);
            CLIENT_ID = pCLIENT_ID(reader);
        }

        int pDB_COMPANY_ID(OracleDataReader reader)
        {
            return (reader["DB_COMPANY_ID"] != DBNull.Value) ? Convert.ToInt32(reader["DB_COMPANY_ID"]) : new int();
        }
        string pDATABASE_SID(OracleDataReader reader)
        {
            return (reader["DATABASE_SID"] != DBNull.Value) ? Convert.ToString(reader["DATABASE_SID"]) : string.Empty;
        }
        int pCOMPANY_ID(OracleDataReader reader)
        {
            return (reader["COMPANY_ID"] != DBNull.Value) ? Convert.ToInt32(reader["COMPANY_ID"]) : new int();
        }
        string pCOMPANY_CODE(OracleDataReader reader)
        {
            return (reader["COMPANY_CODE"] != DBNull.Value) ? Convert.ToString(reader["COMPANY_CODE"]) : string.Empty;
        }
        int pCLIENT_ID(OracleDataReader reader)
        {
            return (reader["CLIENT_ID"] != DBNull.Value) ? Convert.ToInt32(reader["CLIENT_ID"]) : new int();
        }

        public override bool Insert(OracleConnection Conn)
        {
            string sql = string.Empty;
            sql += "insert into CLIENT_DB_COMPANIES ";
            sql += "(DB_COMPANY_ID, DATABASE_SID, COMPANY_ID, COMPANY_CODE, CLIENT_ID) ";
            sql += "values (:a, :b, :c, :d, :e)";

            using (var Command = new OracleCommand(sql, Conn))
            {
                DB_COMPANY_ID = Utilities.SequenceNextValue(Sequences.ID_GEN);
                Command.Parameters.Add(":a", ResolveType(DB_COMPANY_ID)).Value = DB_COMPANY_ID;
                Command.Parameters.Add(":b", ResolveType(DATABASE_SID)).Value = DATABASE_SID;
                Command.Parameters.Add(":c", ResolveType(COMPANY_ID)).Value = COMPANY_ID ?? null;
                Command.Parameters.Add(":d", ResolveType(COMPANY_CODE)).Value = COMPANY_CODE;
                Command.Parameters.Add(":e", ResolveType(CLIENT_ID)).Value = CLIENT_ID;

                var r = Convert.ToBoolean(Command.ExecuteNonQuery());
                if (OnInsert != null) OnInsert(this, EventArgs.Empty);
                return r;
            }
        }
        public override bool Update(OracleConnection Conn)
        {
            string sql = string.Empty;
            sql += "UPDATE CLIENT_DB_COMPANIES SET ";
            sql += "DATABASE_SID = :b, COMPANY_ID = :c, COMPANY_CODE = :d, CLIENT_ID = :e ";
            sql += "WHERE DB_COMPANY_ID = :a ";

            using (var Command = new OracleCommand(sql, Conn))
            {
                Command.Parameters.Add(":b", ResolveType(DATABASE_SID)).Value = DATABASE_SID;
                Command.Parameters.Add(":c", ResolveType(COMPANY_ID)).Value = COMPANY_ID ?? null;
                Command.Parameters.Add(":d", ResolveType(COMPANY_CODE)).Value = COMPANY_CODE;
                Command.Parameters.Add(":e", ResolveType(CLIENT_ID)).Value = CLIENT_ID;
                Command.Parameters.Add(":a", ResolveType(DB_COMPANY_ID)).Value = DB_COMPANY_ID;

                var r = Convert.ToBoolean(Command.ExecuteNonQuery());
                if (OnUpdate != null) OnUpdate(this, EventArgs.Empty);
                return r;
            }
        }
        public override bool Delete(OracleConnection Conn)
        {
            string sql = string.Empty;
            sql += "DELETE FROM CLIENT_DB_COMPANIES WHERE DB_COMPANY_ID = :a";

            using (var Command = new OracleCommand(sql, Conn))
            {
                Command.Parameters.Add(":a", ResolveType(DB_COMPANY_ID)).Value = DB_COMPANY_ID;
                var r = Convert.ToBoolean(Command.ExecuteNonQuery());
                if (OnDelete != null) OnDelete(this, EventArgs.Empty);
                return r;
            }
        }
    }
}