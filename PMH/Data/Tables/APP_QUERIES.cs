using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.DataAccess.Client;

namespace Plex.PMH.Data.Tables
{
    public class APP_QUERIES : PlexxisDataTransferObjects
    {
        public static event Subscriber OnInsert;
        public static event Subscriber OnUpdate;
        public static event Subscriber OnDelete;

        public static IEnumerable<APP_QUERIES> GetAll()
        {
            using (var Conn = Utilities.GetConnection(true))
                return GetAll(Conn);
        }

        public static IEnumerable<APP_QUERIES> GetAll(OracleConnection Conn)
        {
            List<APP_QUERIES> collection = new List<APP_QUERIES>();
            using (var Command = new OracleCommand("SELECT * FROM APP_QUERIES",Conn))
            using (var reader = Command.ExecuteReader())
                while (reader.Read())
                    collection.Add(new APP_QUERIES(reader));
            return collection;
        }

        public IEnumerable<APP_QUERY_COLUMNS> GetAPP_QUERY_COLUMNS()
        {
            using (var Conn = Utilities.GetConnection(true))
                return GetAPP_QUERY_COLUMNS(Conn);
        }

        public IEnumerable<APP_QUERY_COLUMNS> GetAPP_QUERY_COLUMNS(OracleConnection Conn)
        {
            List<APP_QUERY_COLUMNS> collection = new List<APP_QUERY_COLUMNS>();
            using (var Command = new OracleCommand("SELECT * FROM APP_QUERIES WHERE QUERY_ID = :a", Conn))
            {
                Command.Parameters.Add(":a", ResolveType(QUERY_ID)).Value = QUERY_ID;
                using (var reader = Command.ExecuteReader())
                    while (reader.Read())
                        collection.Add(new APP_QUERY_COLUMNS(reader));
            }
            return collection;
        }

        public IEnumerable<APP_QUERY_CONDITIONS> GetAPP_QUERY_CONDITIONS()
        {
            using (var Conn = Utilities.GetConnection(true))
                return GetAPP_QUERY_CONDITIONS(Conn);
        }

        public IEnumerable<APP_QUERY_CONDITIONS> GetAPP_QUERY_CONDITIONS(OracleConnection Conn)
        {
            List<APP_QUERY_CONDITIONS> collection = new List<APP_QUERY_CONDITIONS>();
            using (var Command = new OracleCommand("SELECT * FROM APP_QUERY_CONDITIONS WHERE QUERY_ID = :a", Conn))
            {
                Command.Parameters.Add(":a", ResolveType(QUERY_ID)).Value = QUERY_ID;
                using (var reader = Command.ExecuteReader())
                    while (reader.Read())
                        collection.Add(new APP_QUERY_CONDITIONS(reader));
            }
            return collection;
        }

        public int QUERY_ID;//NUMBER(10)	N			
        public int APP_ID;//NUMBER(10)	N			
        public string NAME;//VARCHAR2(50)	N			
        public string DESCRIPTION;//VARCHAR2(4000)	Y			
        public int TABLE_ID;//NUMBER(10)	N			
        public int IS_DELTA;//NUMBER(1)	N			
        public string SQL;//VARCHAR2(4000)	N			
        public int SEQ_LIMIT_TIMESPAN;//NUMBER(10)	Y			
        public int SEQ_LIMIT;//NUMBER(10)	Y		

        public APP_QUERIES()
        {
        }

        public APP_QUERIES(OracleDataReader reader)
        {
            QUERY_ID = pQUERY_ID(reader);
            APP_ID = pAPP_ID(reader);
            DESCRIPTION = pDESCRIPTION(reader);
            TABLE_ID = pTABLE_ID(reader);
            IS_DELTA = pIS_DELTA(reader);
            SQL = pSQL(reader);
            SEQ_LIMIT = pSEQ_LIMIT(reader);
            SEQ_LIMIT_TIMESPAN = pSEQ_LIMIT_TIMESPAN(reader);
        }

        int pQUERY_ID(OracleDataReader reader)
        {
            return (reader["QUERY_ID"] != DBNull.Value) ? Convert.ToInt32(reader["QUERY_ID"]) : new int();
        }
        int pAPP_ID(OracleDataReader reader)
        {
            return (reader["APP_ID"] != DBNull.Value) ? Convert.ToInt32(reader["APP_ID"]) : new int();
        }
        string pNAME(OracleDataReader reader) 
        {
            return (reader["NAME"] != DBNull.Value) ? Convert.ToString(reader["NAME"]) : string.Empty;
        }
        string pDESCRIPTION(OracleDataReader reader)
        {
            return (reader["DESCRIPTION"] != DBNull.Value) ? Convert.ToString(reader["DESCRIPTION"]) : string.Empty;
        }
        int pTABLE_ID(OracleDataReader reader)
        {
            return (reader["TABLE_ID"] != DBNull.Value) ? Convert.ToInt32(reader["TABLE_ID"]) : new int();
        }
        int pIS_DELTA(OracleDataReader reader)
        {
            return (reader["IS_DELTA"] != DBNull.Value) ? Convert.ToInt32(reader["IS_DELTA"]) : new int();
        }
        string pSQL(OracleDataReader reader)
        {
            return (reader["SQL"] != DBNull.Value) ? Convert.ToString(reader["SQL"]) : string.Empty;
        }
        int pSEQ_LIMIT_TIMESPAN(OracleDataReader reader)
        {
            return (reader["IS_DELTA"] != DBNull.Value) ? Convert.ToInt32(reader["IS_DELTA"]) : new int();
        }
        int pSEQ_LIMIT(OracleDataReader reader)
        {
            return (reader["SEQ_LIMIT"] != DBNull.Value) ? Convert.ToInt32(reader["SEQ_LIMIT"]) : new int();
        }
        
        public override bool Insert(OracleConnection Conn)
        {
            string sql = string.Empty;
            sql += "insert into APP_QUERIES ";
            sql += "(QUERY_ID, APP_ID, NAME, DESCRIPTION, TABLE_ID, ";
            sql += "IS_DELTA, SQL , SEQ_LIMIT_TIMESPAN, SEQ_LIMIT) ";
            sql += "values (:a, :b, :c, :d, :e, :f, :g, :h, :i) ";

            using (var Command = new OracleCommand(sql, Conn))
            {
                QUERY_ID = Utilities.SequenceNextValue(Sequences.ID_GEN);
                Command.Parameters.Add(":a", OracleDbType.Int32).Value = QUERY_ID;
                Command.Parameters.Add(":b", OracleDbType.Int32).Value = APP_ID;
                Command.Parameters.Add(":c", OracleDbType.Varchar2).Value = NAME;
                Command.Parameters.Add(":d", OracleDbType.Varchar2).Value = DESCRIPTION;
                Command.Parameters.Add(":e", OracleDbType.Int32).Value = TABLE_ID;
                Command.Parameters.Add(":f", OracleDbType.Int32).Value = IS_DELTA;
                Command.Parameters.Add(":g", OracleDbType.Varchar2).Value = SQL;
                Command.Parameters.Add(":h", OracleDbType.Int32).Value = SEQ_LIMIT_TIMESPAN;
                Command.Parameters.Add(":i", OracleDbType.Int32).Value = SEQ_LIMIT;

                var r = Convert.ToBoolean(Command.ExecuteNonQuery());
                if (OnInsert != null) OnInsert(this, EventArgs.Empty);
                return r;
            }
        }
        public override bool Update(OracleConnection Conn)
        {
            string sql = string.Empty;
            sql += "UPDATE APP_QUERIES SET ";
            sql += "APP_ID = :b, NAME = :c, DESCRIPTION = :d, TABLE_ID = :e, ";
            sql += "IS_DELTA = :f, SQL = :g, SEQ_LIMIT_TIMESPAN = :h, SEQ_LIMIT = :i ";
            sql += "WHERE QUERY_ID = :a";

            using (var Command = new OracleCommand(sql, Conn))
            {
                Command.Parameters.Add(":b", OracleDbType.Int32).Value = APP_ID;
                Command.Parameters.Add(":c", OracleDbType.Varchar2).Value = NAME;
                Command.Parameters.Add(":d", OracleDbType.Varchar2).Value = DESCRIPTION;
                Command.Parameters.Add(":e", OracleDbType.Int32).Value = TABLE_ID;
                Command.Parameters.Add(":f", OracleDbType.Int32).Value = IS_DELTA;
                Command.Parameters.Add(":g", OracleDbType.Varchar2).Value = SQL;
                Command.Parameters.Add(":h", OracleDbType.Int32).Value = SEQ_LIMIT_TIMESPAN;
                Command.Parameters.Add(":i", OracleDbType.Int32).Value = SEQ_LIMIT;
                Command.Parameters.Add(":a", OracleDbType.Int32).Value = QUERY_ID;

                var r = Convert.ToBoolean(Command.ExecuteNonQuery());
                if (OnUpdate != null) OnUpdate(this, EventArgs.Empty);
                return r;
            }
        }
        public override bool Delete(OracleConnection Conn)
        {
            string sql = string.Empty;
            sql += "DELETE FROM APP_QUERIES WHERE QUERY_ID = :a";
            using (var Command = new OracleCommand(sql, Conn))
            {
                Command.Parameters.Add(":a", OracleDbType.Int32).Value = QUERY_ID;

                var r = Convert.ToBoolean(Command.ExecuteNonQuery());
                if (OnDelete != null) OnDelete(this, EventArgs.Empty);
                return r;
            }
        }
    }
}