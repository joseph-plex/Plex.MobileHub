using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.DataAccess.Client;

namespace Plex.PMH.Data.Tables
{
    public class APP_TABLE_COLUMNS : PlexxisDataTransferObjects
    {
        public static event Subscriber OnInsert;
        public static event Subscriber OnUpdate;
        public static event Subscriber OnDelete;

        public static IEnumerable<APP_TABLE_COLUMNS> GetAll()
        {
            using (var Conn = Utilities.GetConnection(true))
                return GetAll(Conn);
        }

        public static IEnumerable<APP_TABLE_COLUMNS> GetAll(OracleConnection Conn)
        {
            var collection = new List<APP_TABLE_COLUMNS>();
            using (var Command = new OracleCommand("SELECT * FROM APP_TABLE_COLUMNS", Conn))
            using (var reader = Command.ExecuteReader())
                while (reader.Read())
                    collection.Add(new APP_TABLE_COLUMNS(reader));
            return collection;
        }

        public int TABLE_ID;//NUMBER(10)	N			
        public int TABLE_COLUMN_ID;//NUMBER(10)	N			
        public string  COLUMN_NAME;//VARCHAR2(50)	N			
        public int COLUMN_SEQUENCE;//NUMBER(3)	N			
        public string DATA_TYPE;//VARCHAR2(50)	Y			
        public int DATA_LENGTH;//NUMBER(10)	Y			
        public int DATA_PRECISION;//NUMBER(2)	Y			
        public int DATA_SCALE;//NUMBER(2)	Y			
        public int ALLOW_DB_NULL;//NUMBER(1)	Y			
        public int IS_READ_ONLY;//NUMBER(1)	Y			
        public int IS_LONG;//NUMBER(1)	Y			
        public int IS_KEY;//NUMBER(1)	Y			
        public string KEY_TYPE;//VARCHAR2(20)	Y			
        public int IS_UNIQUE;//	NUMBER(1)	Y			
        public string DESCRIPTION;//VARCHAR2(4000)	Y		

        public APP_TABLE_COLUMNS()
        {
        }
        public APP_TABLE_COLUMNS(OracleDataReader reader)
        {
            TABLE_ID = pTABLE_ID(reader);
            TABLE_COLUMN_ID = pTABLE_COLUMN_ID(reader);
            COLUMN_NAME = pCOLUMN_NAME(reader);
            DATA_TYPE = pDATA_TYPE(reader);
            DATA_LENGTH = pDATA_LENGTH(reader);
            DATA_PRECISION = pDATA_PRECISION(reader);
            DATA_SCALE = pDATA_SCALE(reader);
            ALLOW_DB_NULL = pALLOW_DB_NULL(reader);
            IS_READ_ONLY = pALLOW_DB_NULL(reader);
            IS_LONG = pIS_LONG(reader);
            IS_KEY = pIS_KEY(reader);
            KEY_TYPE = pKEY_TYPE(reader);
            DESCRIPTION = pDESCRIPTION(reader);
            IS_UNIQUE = pIS_UNIQUE(reader);
        }

        int pTABLE_ID(OracleDataReader reader)
        {
            return (reader["TABLE_ID"] != DBNull.Value) ? Convert.ToInt32(reader["TABLE_ID"]) : new int();
        }
        int pTABLE_COLUMN_ID(OracleDataReader reader)
        {
            return (reader["TABLE_COLUMN_ID"] != DBNull.Value) ? Convert.ToInt32(reader["TABLE_COLUMN_ID"]) : new int();
        }
        string pCOLUMN_NAME(OracleDataReader reader)
        {
            return (reader["COLUMN_NAME"] != DBNull.Value) ? Convert.ToString(reader["COLUMN_NAME"]) : string.Empty;
        }
        int pCOLUMN_SEQUENCE(OracleDataReader reader)
        {
            return (reader["COLUMN_SEQUENCE"] != DBNull.Value) ? Convert.ToInt32(reader["COLUMN_SEQUENCE"]) : new int();
        }
        string pDATA_TYPE(OracleDataReader reader)
        {
            return (reader["DATA_TYPE"] != DBNull.Value) ? Convert.ToString(reader["DATA_TYPE"]) : string.Empty;
        }
        int pDATA_LENGTH(OracleDataReader reader)
        {
            return (reader["DATA_LENGTH"] != DBNull.Value) ? Convert.ToInt32(reader["DATA_LENGTH"]) : new int();
        }
        int pDATA_PRECISION(OracleDataReader reader)
        {
            return (reader["DATA_PRECISION"] != DBNull.Value) ? Convert.ToInt32(reader["DATA_PRECISION"]) : new int();
        }
        int pDATA_SCALE(OracleDataReader reader)
        {
            return (reader["DATA_SCALE"] != DBNull.Value) ? Convert.ToInt32(reader["DATA_SCALE"]) : new int();
        }
        int pALLOW_DB_NULL(OracleDataReader reader)
        {
            return (reader["ALLOW_DB_NULL"] != DBNull.Value) ? Convert.ToInt32(reader["ALLOW_DB_NULL"]) : new int();
        }
        int pIS_READ_ONLY(OracleDataReader reader)
        {
            return (reader["IS_READ_ONLY"] != DBNull.Value) ? Convert.ToInt32(reader["IS_READ_ONLY"]) : new int();
        }
        int pIS_LONG(OracleDataReader reader)
        {
            return (reader["IS_LONG"] != DBNull.Value) ? Convert.ToInt32(reader["IS_LONG"]) : new int();
        }
        int pIS_KEY(OracleDataReader reader)
        {
            return (reader["IS_KEY"] != DBNull.Value) ? Convert.ToInt32(reader["IS_KEY"]) : new int();
        }
        string pKEY_TYPE(OracleDataReader reader)
        {
            return (reader["KEY_TYPE"] != DBNull.Value) ? Convert.ToString(reader["KEY_TYPE"]) : string.Empty;
        }
        int pIS_UNIQUE(OracleDataReader reader)
        {
            return (reader["IS_UNIQUE"] != DBNull.Value) ? Convert.ToInt32(reader["IS_UNIQUE"]) : new int();
        }
        string pDESCRIPTION(OracleDataReader reader)
        {
            return (reader["DESCRIPTION"] != DBNull.Value) ? Convert.ToString(reader["DESCRIPTION"]) : string.Empty;
        }

        public override bool Insert(OracleConnection Conn)
        {
            string sql = string.Empty;
            sql += "insert into app_table_columns ";
            sql += "(TABLE_ID, TABLE_COLUMN_ID, COLUMN_NAME, COLUMN_SEQUENCE, ";
            sql += "DATA_TYPE, DATA_LENGTH, DATA_PRECISION, DATA_SCALE, ALLOW_DB_NULL,";
            sql += "IS_READ_ONLY, IS_LONG, IS_KEY, KEY_TYPE, IS_UNIQUE, DESCRIPTION)";
            sql += "values(:a, :b, :c, :d, :e, :f, :g, :h, :i, :j, :k, :l, :m, :n, :o)";

            using (var Command = new OracleCommand(sql, Conn))
            {
                TABLE_ID = Utilities.SequenceNextValue(Sequences.ID_GEN);
                Command.Parameters.Add(":a", OracleDbType.Int32).Value = TABLE_ID;
                Command.Parameters.Add(":b", OracleDbType.Int32).Value = TABLE_COLUMN_ID;
                Command.Parameters.Add(":c", OracleDbType.Varchar2).Value = COLUMN_NAME;
                Command.Parameters.Add(":d", OracleDbType.Int32).Value = COLUMN_SEQUENCE;
                Command.Parameters.Add(":e", OracleDbType.Varchar2).Value = DATA_TYPE;
                Command.Parameters.Add(":f", OracleDbType.Int32).Value = DATA_LENGTH;
                Command.Parameters.Add(":g", OracleDbType.Int32).Value = DATA_PRECISION;
                Command.Parameters.Add(":h", OracleDbType.Int32).Value = DATA_SCALE;
                Command.Parameters.Add(":i", OracleDbType.Int32).Value = ALLOW_DB_NULL;
                Command.Parameters.Add(":j", OracleDbType.Int32).Value = IS_READ_ONLY;
                Command.Parameters.Add(":k", OracleDbType.Int32).Value = IS_LONG;
                Command.Parameters.Add(":l", OracleDbType.Int32).Value = IS_KEY;
                Command.Parameters.Add(":m", OracleDbType.Varchar2).Value = KEY_TYPE;
                Command.Parameters.Add(":n", OracleDbType.Int32).Value = IS_UNIQUE;
                Command.Parameters.Add(":o", OracleDbType.Varchar2).Value = DESCRIPTION;

                var r = Convert.ToBoolean(Command.ExecuteNonQuery());
                if (OnInsert != null) OnInsert(this, EventArgs.Empty);
                return r;
            }
        }
        public override bool Update(OracleConnection Conn)
        {
            string sql = string.Empty;
            sql += "UPDATE APP_TABLE_COLUMNS SET ";
            sql += "TABLE_COLUMN_ID = :b, COLUMN_NAME = :c, COLUMN_SEQUENCE = :d, ";
            sql += "DATA_TYPE = :e, DATA_LENGTH = :f, DATA_PRECISION = :g , DATA_SCALE = :h, ";
            sql += "ALLOW_DB_NULL = :i, IS_READ_ONLY =:j, IS_LONG = :k, IS_KEY = :l, KEY_TYPE = :m, ";
            sql += "IS_UNIQUE = :n, DESCRIPTION = :o ";
            sql += "WHERE TABLE_ID = :a";

            using (var Command = new OracleCommand(sql, Conn))
            {
                Command.Parameters.Add(":b", OracleDbType.Int32).Value = TABLE_COLUMN_ID;
                Command.Parameters.Add(":c", OracleDbType.Varchar2).Value = COLUMN_NAME;
                Command.Parameters.Add(":d", OracleDbType.Int32).Value = COLUMN_SEQUENCE;
                Command.Parameters.Add(":e", OracleDbType.Varchar2).Value = DATA_TYPE;
                Command.Parameters.Add(":f", OracleDbType.Int32).Value = DATA_LENGTH;
                Command.Parameters.Add(":g", OracleDbType.Int32).Value = DATA_PRECISION;
                Command.Parameters.Add(":h", OracleDbType.Int32).Value = DATA_SCALE;
                Command.Parameters.Add(":i", OracleDbType.Int32).Value = ALLOW_DB_NULL;
                Command.Parameters.Add(":j", OracleDbType.Int32).Value = IS_READ_ONLY;
                Command.Parameters.Add(":k", OracleDbType.Int32).Value = IS_LONG;
                Command.Parameters.Add(":l", OracleDbType.Int32).Value = IS_KEY;
                Command.Parameters.Add(":m", OracleDbType.Varchar2).Value = KEY_TYPE;
                Command.Parameters.Add(":n", OracleDbType.Int32).Value = IS_UNIQUE;
                Command.Parameters.Add(":o", OracleDbType.Varchar2).Value = DESCRIPTION;
                Command.Parameters.Add(":a", OracleDbType.Int32).Value = TABLE_ID;

                var r = Convert.ToBoolean(Command.ExecuteNonQuery());
                if (OnUpdate != null) OnUpdate(this, EventArgs.Empty);
                return r;
            }
        }
        public override bool Delete(OracleConnection Conn)
        {
            string sql = string.Empty;
            sql += "DELETE FROM APP_TABLE_COLUMNS WHERE TABLE_ID = :a";

            using (var Command = new OracleCommand(sql, Conn))
            {
                Command.Parameters.Add(":a", OracleDbType.Int32).Value = TABLE_ID;
                var r = Convert.ToBoolean(Command.ExecuteNonQuery());
                if (OnDelete != null) OnDelete(this, EventArgs.Empty);
                return r;
            }
        }
    }
}