using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.DataAccess.Client;
using System.Data.Common;

namespace Plex.PMH.Data.Tables
{
    public class APP_QUERY_COLUMNS : PlexxisDataTransferObjects
    {
        public static event Subscriber OnInsert;
        public static event Subscriber OnUpdate;
        public static event Subscriber OnDelete;

        public static IEnumerable<APP_QUERY_COLUMNS> GetAll()
        {
            using (var Conn = Utilities.GetConnection(true))
                return GetAll(Conn);
        }

        public static IEnumerable<APP_QUERY_COLUMNS> GetAll(OracleConnection Conn)
        {
            var collection = new List<APP_QUERY_COLUMNS>();
            using (var Command = new OracleCommand("SELECT * FROM APP_QUERY_COLUMNS", Conn))
            using (var reader = Command.ExecuteReader())
                while (reader.Read())
                    collection.Add(new APP_QUERY_COLUMNS(reader));
            return collection;
        }


        public int COLUMN_ID;//	NUMBER(10)	N			
        public int QUERY_ID;//	NUMBER(10)	N			
        public string COLUMN_NAME;//	VARCHAR2(50)	N			
        public int COLUMN_SEQUENCE;//	NUMBER(3)	N	

        public APP_QUERY_COLUMNS()
        {
        }

        public APP_QUERY_COLUMNS(OracleDataReader reader)
        {
            COLUMN_ID = pCOLUMN_ID(reader);
            QUERY_ID = pQUERY_ID(reader);
            COLUMN_NAME = pCOLUMN_NAME(reader);
            pCOLUMN_SEQUENCE(reader);
        }
        
        int pCOLUMN_ID(OracleDataReader reader)
        {
            return (reader["COLUMN_ID"] != DBNull.Value) ? Convert.ToInt32(reader["COLUMN_ID"]) : new int();
        }
        int pQUERY_ID(OracleDataReader reader)
        {
            return (reader["QUERY_ID"] != DBNull.Value) ? Convert.ToInt32(reader["QUERY_ID"]) : new int();
        }
        string pCOLUMN_NAME(OracleDataReader reader)
        {
            return (reader["COLUMN_NAME"] != DBNull.Value) ? Convert.ToString(reader["COLUMN_NAME"]) : string.Empty;
        }
        int pCOLUMN_SEQUENCE(OracleDataReader reader)
        {
            return (reader["COLUMN_SEQUENCE"] != DBNull.Value) ? Convert.ToInt32(reader["COLUMN_SEQUENCE"]) : new int();
        }

        public override bool Insert(OracleConnection Conn)
        {
            string sql = string.Empty;
            sql += "INSERT INTO APP_QUERY_COLUMNS(COLUMN_ID, QUERY_ID, COLUMN_NAME,";
            sql += "COLUMN_SEQUENCE) VALUES (:a, :b, :c, :d)";

            using (var Command = new OracleCommand(sql, Conn))
            {
                COLUMN_ID = Utilities.SequenceNextValue(Sequences.ID_GEN);
                Command.Parameters.Add(":a", OracleDbType.Int32).Value = COLUMN_ID;
                Command.Parameters.Add(":b", OracleDbType.Int32).Value = QUERY_ID;
                Command.Parameters.Add(":c", OracleDbType.Varchar2).Value = COLUMN_NAME;
                Command.Parameters.Add(":d", OracleDbType.Int32).Value = COLUMN_SEQUENCE;

                var r = Convert.ToBoolean(Command.ExecuteNonQuery());
                if (OnInsert != null) OnInsert(this, EventArgs.Empty);
                return r;
            }
        }

        public override bool Update(OracleConnection Conn)
        {
            string sql = string.Empty;
            sql += "UPDATE APP_QUERY_COLUMNS SET ";
            sql += "QUERY_ID = :b, COLUMN_NAME = :c, ";
            sql += "COLUMN_SEQUENCE = :d WHERE COLUMN_ID = :a";

            using (var Command = new OracleCommand(sql, Conn))
            {
                Command.Parameters.Add(":b", OracleDbType.Int32).Value = QUERY_ID;
                Command.Parameters.Add(":c", OracleDbType.Varchar2).Value = COLUMN_NAME;
                Command.Parameters.Add(":d", OracleDbType.Int32).Value = COLUMN_SEQUENCE;
                Command.Parameters.Add(":a", OracleDbType.Int32).Value = COLUMN_ID;

                var r = Convert.ToBoolean(Command.ExecuteNonQuery());
                if (OnUpdate != null) OnUpdate(this, EventArgs.Empty);
                return r;
            }
        }

        public override bool Delete(OracleConnection Conn)
        {
            string sql = string.Empty;
            sql += "DELETE FROM APP_QUERY_COLUMNS WHERE COLUMN_ID = :a";

            using (var Command = new OracleCommand(sql, Conn))
            {
                Command.Parameters.Add(":a", OracleDbType.Int32).Value = COLUMN_ID;
                var r = Convert.ToBoolean(Command.ExecuteNonQuery());
                if (OnDelete != null) OnDelete(this, EventArgs.Empty);
                return r;
            }
        }
    }
}