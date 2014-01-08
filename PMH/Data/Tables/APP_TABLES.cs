using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.DataAccess.Client;

namespace Plex.PMH.Data.Tables
{
    public class APP_TABLES : PlexxisDataTransferObjects
    {
        public static event Subscriber OnInsert;
        public static event Subscriber OnUpdate;
        public static event Subscriber OnDelete;

        public static IEnumerable<APP_TABLES> GetAll()
        {
            using (var Conn = Utilities.GetConnection(true))
                return GetAll(Conn);
        }

        public static IEnumerable<APP_TABLES> GetAll(OracleConnection Conn)
        {
            var collection = new List<APP_TABLES>();
            using (var Command = new OracleCommand("SELECT * FROM APP_TABLES", Conn))
            using (var reader = Command.ExecuteReader())
                while (reader.Read())
                    collection.Add(new APP_TABLES(reader));
            return collection;
        }

        public IEnumerable<APP_TABLE_COLUMNS> GetAPP_TABLE_COLUMNS()
        {
            using (var Conn = Utilities.GetConnection(true))
                return GetAPP_TABLE_COLUMNS(Conn);
        }

        public IEnumerable<APP_TABLE_COLUMNS> GetAPP_TABLE_COLUMNS(OracleConnection Conn)
        {
            var collection = new List<APP_TABLE_COLUMNS>();
            using (var Command = new OracleCommand("SELECT * FROM APP_TABLE_COLUMNS WHERE TABLE_ID = :a ", Conn))
            {
                Command.Parameters.Add(":a", ResolveType(TABLE_ID)).Value = TABLE_ID;
                using (var reader = Command.ExecuteReader())
                    while (reader.Read())
                        collection.Add(new APP_TABLE_COLUMNS(reader));
            }
            return collection;
        }


        public int TABLE_ID	;//NUMBER(10)	N			
        public int APP_ID;//	NUMBER(10)	N			
        public string NAME;//	VARCHAR2(50)	N			
        public string DESCRIPTION;//	VARCHAR2(4000)	Y			
        public int INSERT_ALLOWED;//	NUMBER(1)	Y			
        public int UPDATE_ALLOWED;//	NUMBER(1)	Y			
        public int DELETE_ALLOWED;//	NUMBER(1)	Y			
        public int QUERY_ALLOWED;//	NUMBER(1)	Y	

        public APP_TABLES()
        {
        }

        public APP_TABLES(OracleDataReader reader)
        {
            TABLE_ID = pTABLE_ID(reader);
            APP_ID = pAPP_ID(reader);
            NAME = pNAME(reader);
            DESCRIPTION = pDESCRIPTION(reader);
            INSERT_ALLOWED = pINSERT_ALLOWED(reader);
            UPDATE_ALLOWED = pUPDATE_ALLOWED(reader);
            DELETE_ALLOWED = pDELETE_ALLOWED(reader);
            QUERY_ALLOWED = pQUERY_ALLOWED(reader);
        }

        int pTABLE_ID(OracleDataReader reader)
        {
            return (reader["TABLE_ID"] != DBNull.Value) ? Convert.ToInt32(reader["TABLE_ID"]) : new int();
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
        int pINSERT_ALLOWED(OracleDataReader reader)
        {
            return (reader["INSERT_ALLOWED"] != DBNull.Value) ? Convert.ToInt32(reader["INSERT_ALLOWED"]) : new int();
        }
        int pUPDATE_ALLOWED(OracleDataReader reader)
        {
            return (reader["UPDATE_ALLOWED"] != DBNull.Value) ? Convert.ToInt32(reader["UPDATE_ALLOWED"]) : new int();
        }
        int pDELETE_ALLOWED(OracleDataReader reader)
        {
            return (reader["DELETE_ALLOWED"] != DBNull.Value) ? Convert.ToInt32(reader["DELETE_ALLOWED"]) : new int();
        }
        int pQUERY_ALLOWED(OracleDataReader reader)
        {
            return (reader["QUERY_ALLOWED"] != DBNull.Value) ? Convert.ToInt32(reader["QUERY_ALLOWED"]) : new int();
        }

        public override bool Insert(OracleConnection Conn)
        {

            string sql = string.Empty;
            sql += "INSERT INTO APP_TABLES(TABLE_ID, APP_ID, NAME, DESCRIPTION, INSERT_ALLOWED, ";
            sql += "UPDATE_ALLOWED, DELETE_ALLOWED, QUERY_ALLOWED) VALUES ";
            sql += "(:a, :b, :c, :d, :e, :f, :g, :h)";

            using (var Command = new OracleCommand(sql, Conn))
            {
                TABLE_ID = Utilities.SequenceNextValue(Sequences.ID_GEN);
                Command.Parameters.Add(":a", OracleDbType.Int32).Value = TABLE_ID;
                Command.Parameters.Add(":b", OracleDbType.Int32).Value = APP_ID;
                Command.Parameters.Add(":c", OracleDbType.Varchar2).Value = NAME;
                Command.Parameters.Add(":d", OracleDbType.Varchar2).Value = DESCRIPTION;
                Command.Parameters.Add(":e", OracleDbType.Int32).Value = INSERT_ALLOWED;
                Command.Parameters.Add(":f", OracleDbType.Int32).Value = UPDATE_ALLOWED;
                Command.Parameters.Add(":g", OracleDbType.Int32).Value = DELETE_ALLOWED;
                Command.Parameters.Add(":h", OracleDbType.Int32).Value = QUERY_ALLOWED;

                var r = Convert.ToBoolean(Command.ExecuteNonQuery());
                if (OnInsert != null) OnInsert(this, EventArgs.Empty);
                return r;
            }
        }

        public override bool Update(OracleConnection Conn)
        {
            string sql = string.Empty;
            sql += "UPDATE APP_TABLES SET ";
            sql += "APP_ID = :b, NAME = :c, DESCRIPTION = :d, INSERT_ALLOWED = :e, ";
            sql += "UPDATE_ALLOWED = :f, DELETE_ALLOWED = :g, QUERY_ALLOWED = :h ";
            sql += "WHERE TABLE_ID = :a";

            using (var Command = new OracleCommand(sql, Conn))
            {
                Command.Parameters.Add(":b", OracleDbType.Int32).Value = APP_ID;
                Command.Parameters.Add(":c", OracleDbType.Varchar2).Value = NAME;
                Command.Parameters.Add(":d", OracleDbType.Varchar2).Value = DESCRIPTION;
                Command.Parameters.Add(":e", OracleDbType.Int32).Value = INSERT_ALLOWED;
                Command.Parameters.Add(":f", OracleDbType.Int32).Value = UPDATE_ALLOWED;
                Command.Parameters.Add(":g", OracleDbType.Int32).Value = DELETE_ALLOWED;
                Command.Parameters.Add(":h", OracleDbType.Int32).Value = QUERY_ALLOWED; 
                Command.Parameters.Add(":a", OracleDbType.Int32).Value = TABLE_ID;

                var r = Convert.ToBoolean(Command.ExecuteNonQuery());
                if (OnUpdate != null) OnUpdate(this, EventArgs.Empty);
                return r;
            }
        }

        public override bool Delete(OracleConnection Conn)
        {
            string sql = string.Empty;
            sql += "DELETE FROM APP_TABLES WHERE TABLE_ID = :a";

            using (var Command = new OracleCommand(sql, Conn))
            {
                Command.Parameters.Add("a", OracleDbType.Int32).Value = TABLE_ID;
                var r = Convert.ToBoolean(Command.ExecuteNonQuery());
                if (OnDelete != null) OnDelete(this, EventArgs.Empty);
                return r;
            }
        }
    }
}