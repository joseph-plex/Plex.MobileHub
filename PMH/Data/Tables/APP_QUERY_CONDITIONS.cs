using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.DataAccess.Client;

namespace Plex.PMH.Data.Tables
{
    public class APP_QUERY_CONDITIONS : PlexxisDataTransferObjects
    {
        public static event Subscriber OnInsert;
        public static event Subscriber OnUpdate;
        public static event Subscriber OnDelete;

        public static IEnumerable<APP_QUERY_CONDITIONS> GetAll()
        {
            using (var Conn = Utilities.GetConnection(true))
                return GetAll(Conn);
        }

        public static IEnumerable<APP_QUERY_CONDITIONS> GetAll(OracleConnection Conn)
        {
            var collection = new List<APP_QUERY_CONDITIONS>();
            using (var Command = new OracleCommand("SELECT * FROM APP_QUERY_CONDITIONS", Conn))
            using (var reader = Command.ExecuteReader())
                while (reader.Read())
                    collection.Add(new APP_QUERY_CONDITIONS(reader));
            return collection;
        }

        public int CONDITION_ID;//	NUMBER(10)	N			
        public int QUERY_ID;//	NUMBER(10)	N			
        public string COLUMN_NAME;//	VARCHAR2(30)	N			
        public string COLUMN_NVL;//	VARCHAR2(1000)	Y			
        public string COLUMN_VALUE;//	VARCHAR2(1000)	N			
        public string OPERATOR;//	VARCHAR2(12)	N		

        public APP_QUERY_CONDITIONS()
        {
        }

        public APP_QUERY_CONDITIONS(OracleDataReader reader)
        {
            CONDITION_ID = pCONDITION_ID(reader);
            QUERY_ID = pQUERY_ID(reader);
            COLUMN_NAME = pCOLUMN_NAME(reader);
            COLUMN_NVL = pCOLUMN_NVL(reader);
            COLUMN_VALUE = pCOLUMN_VALUE(reader);
            OPERATOR = pOPERATOR(reader);
        }

        int pCONDITION_ID(OracleDataReader reader)
        {
            return (reader["CONDITION_ID"] != DBNull.Value) ? Convert.ToInt32(reader["CONDITION_ID"]) : new int();
        }
        int pQUERY_ID(OracleDataReader reader)
        {
            return (reader["QUERY_ID"] != DBNull.Value) ? Convert.ToInt32(reader["QUERY_ID"]) : new int();
        }
        string pCOLUMN_NAME(OracleDataReader reader)
        {
            return (reader["COLUMN_NAME"] != DBNull.Value) ? Convert.ToString(reader["COLUMN_NAME"]) : string.Empty;
        }
        string pCOLUMN_NVL(OracleDataReader reader)
        {
            return (reader["COLUMN_NVL"] != DBNull.Value) ? Convert.ToString(reader["COLUMN_NVL"]) : string.Empty;
        }
        string pCOLUMN_VALUE(OracleDataReader reader)
        {
            return (reader["COLUMN_VALUE"] != DBNull.Value)? Convert.ToString(reader["COLUMN_VALUE"]) : string.Empty;
        }
        string pOPERATOR(OracleDataReader reader)
        {
            return (reader["OPERATOR"] != DBNull.Value) ? Convert.ToString(reader["OPERATOR"]) : string.Empty;
        }

        public override bool Insert(OracleConnection Conn)
        {
            string sql = string.Empty;
            sql += "INSERT INTO APP_QUERY_CONDITIONS(CONDITION_ID,QUERY_ID, COLUMN_NAME,";
            sql += "COLUMN_NVL, COLUMN_VALUE, OPERATOR) ";
            sql += "VALUES (:a, :b, :c, :e, :f, :g)";

            using (var Command = new OracleCommand(sql, Conn))
            {
                CONDITION_ID = Utilities.SequenceNextValue(Sequences.ID_GEN);
                Command.Parameters.Add(":a", ResolveType(CONDITION_ID)).Value = CONDITION_ID;
                Command.Parameters.Add(":b", ResolveType(QUERY_ID)).Value = QUERY_ID;
                Command.Parameters.Add(":c", ResolveType(COLUMN_NAME)).Value = COLUMN_NAME;

                Command.Parameters.Add(":d", ResolveType(COLUMN_NVL)).Value = COLUMN_NVL;
                Command.Parameters.Add(":e", ResolveType(COLUMN_VALUE)).Value = COLUMN_VALUE;
                Command.Parameters.Add(":f", ResolveType(OPERATOR)).Value = OPERATOR;

                var r = Convert.ToBoolean(Command.ExecuteNonQuery());
                if (OnInsert != null) OnInsert(this, EventArgs.Empty);
                return r;
            }
        }

        public override bool Update(OracleConnection Conn)
        {
            string sql = "UPDATE APP_QUERY_CONDITIONS SET QUERY_ID = :b, COLUMN_NAME =:c, COLUMN_NVL = :d, COLUMN_VALUE= :e, OPERATOR= :f where CONDITION_ID = :a";

            using (var Command = new OracleCommand(sql, Conn))
            {
                Command.Parameters.Add(":b", ResolveType(QUERY_ID)).Value = QUERY_ID;
                Command.Parameters.Add(":c", ResolveType(COLUMN_NAME)).Value = COLUMN_NAME;

                Command.Parameters.Add(":d", ResolveType(COLUMN_NVL)).Value = COLUMN_NVL;
                Command.Parameters.Add(":e", ResolveType(COLUMN_VALUE)).Value = COLUMN_VALUE;
                Command.Parameters.Add(":f", ResolveType(OPERATOR)).Value = OPERATOR;

                Command.Parameters.Add(":a", ResolveType(CONDITION_ID)).Value = CONDITION_ID;

                var r = Convert.ToBoolean(Command.ExecuteNonQuery());
                if (OnUpdate != null) OnUpdate(this, EventArgs.Empty);
                return r;
            }
        }

        public override bool Delete(OracleConnection Conn)
        {
            string sql = string.Empty;
            sql += "delete from APP_QUERY_CONDITIONS where CONDITION_ID = :a";

            using (var Command = new OracleCommand(sql, Conn))
            {
                Command.Parameters.Add(":a", OracleDbType.Int32).Value = CONDITION_ID;

                var r = Convert.ToBoolean(Command.ExecuteNonQuery());
                if (OnDelete != null) OnDelete(this, EventArgs.Empty);
                return r;
            }
        }
    }
}