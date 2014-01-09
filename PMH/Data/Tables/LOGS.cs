using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.DataAccess.Client;

namespace Plex.PMH.Data.Tables
{
    public class LOGS : PlexxisDataTransferObjects
    {
        public static event Subscriber OnInsert;
        public static event Subscriber OnUpdate;
        public static event Subscriber OnDelete;


        public static IEnumerable<LOGS> GetAll()
        {
            using (var Conn = Utilities.GetConnection(true))
                return GetAll(Conn);
        }

        public static IEnumerable<LOGS> GetAll(OracleConnection Conn)
        {
            var collection = new List<LOGS>();
            using (var Command = new OracleCommand("SELECT * FROM LOGS", Conn))
            using (var reader = Command.ExecuteReader())
                while (reader.Read())
                    collection.Add(new LOGS(reader));
            return collection;
        }

        public int LOG_ID;//NUMBER(10)	N			
        public DateTime LOG_DATE;//DATE	N			
        public string DESCRIPTION;//	VARCHAR2(2048)	N			
        public int? CLIENT_ID;//	NUMBER(10)	Y			

        public LOGS()
        { 
        }

        public LOGS(OracleDataReader reader)
        {
            LOG_ID = pLOG_ID(reader);
            LOG_DATE = pLOG_DATE(reader);
            DESCRIPTION = pDESCRIPTION(reader);
            CLIENT_ID = pCLIENT_ID(reader);
        }
        int pLOG_ID(OracleDataReader reader)
        {
            return (reader["LOG_ID"] != DBNull.Value) ? Convert.ToInt32(reader["LOG_ID"]) : new int();
        }
        DateTime pLOG_DATE(OracleDataReader reader)
        {
            return (reader["LOG_DATE"] != DBNull.Value) ? Convert.ToDateTime(reader["LOG_DATE"]) : new DateTime();
        }
        string pDESCRIPTION(OracleDataReader reader)
        {
            return (reader["DESCRIPTION"] != DBNull.Value) ? Convert.ToString(reader["DESCRIPTION"]) : string.Empty;
        }
        int? pCLIENT_ID(OracleDataReader reader)
        {
            return (reader["CLIENT_ID"] != DBNull.Value) ? Convert.ToInt32(reader["CLIENT_ID"]) : (int?)null;
        }

        public override bool Insert(OracleConnection Conn)
        {
            string sql = string.Empty;
            sql += "insert into LOG_DATE ";
            sql += "(LOG_ID, LOG_DATE, DESCRIPTION, CLIENT_ID) ";
            sql += "values (:a, :b, :c, :d) ";

            using (var Command = new OracleCommand(sql, Conn))
            {
                LOG_ID = Utilities.SequenceNextValue(Sequences.ID_GEN);
                Command.Parameters.Add(":a", OracleDbType.Int32).Value = LOG_ID;
                Command.Parameters.Add(":b", OracleDbType.Date).Value = LOG_DATE;
                Command.Parameters.Add(":c", OracleDbType.Varchar2).Value = DESCRIPTION;
                Command.Parameters.Add(":d", OracleDbType.Int32).Value = CLIENT_ID;

                var r = Convert.ToBoolean(Command.ExecuteNonQuery());
                if (OnInsert != null) OnInsert(this, EventArgs.Empty);
                return r;
            }
        }

        public override bool Update(OracleConnection Conn)
        {
            string sql = string.Empty;
            sql += "update Logs set ";
            sql += "LOG_DATE = :b, DESCRIPTION = :c, CLIENT_ID = :d ";
            sql += "where LOG_ID = :a ";

            using (var Command = new OracleCommand(sql, Conn))
            {
                Command.Parameters.Add(":b", OracleDbType.Date).Value = LOG_DATE;
                Command.Parameters.Add(":c", OracleDbType.Varchar2).Value = DESCRIPTION;
                Command.Parameters.Add(":d", OracleDbType.Int32).Value = CLIENT_ID;
                Command.Parameters.Add(":a", OracleDbType.Int32).Value = LOG_ID;

                var r = Convert.ToBoolean(Command.ExecuteNonQuery());
                if (OnUpdate != null) OnUpdate(this, EventArgs.Empty);
                return r;
            }
        }

        public override bool Delete(OracleConnection Conn)
        {
            string sql = string.Empty;
            sql += "delete from logs where log_id = :a";

            using (var Command = new OracleCommand(sql, Conn))
            {
                Command.Parameters.Add(":a", OracleDbType.Int32).Value = LOG_ID;
                var r = Convert.ToBoolean(Command.ExecuteNonQuery());
                if (OnDelete != null) OnDelete(this, EventArgs.Empty);
                return r;
            }
        }
    }
}