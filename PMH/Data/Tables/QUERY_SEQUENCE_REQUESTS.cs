using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.DataAccess.Client;

namespace Plex.PMH.Data.Tables
{
    public class QUERY_SEQUENCE_REQUESTS : PlexxisDataTransferObjects
    {
        public static event Subscriber OnInsert;
        public static event Subscriber OnUpdate;
        public static event Subscriber OnDelete;

        public static IEnumerable<QUERY_SEQUENCE_REQUESTS> GetAll()
        {
            using (var Conn = Utilities.GetConnection(true))
                return GetAll(Conn);
        }

        public static IEnumerable<QUERY_SEQUENCE_REQUESTS> GetAll(OracleConnection Conn)
        {
            var collection = new List<QUERY_SEQUENCE_REQUESTS>();
            using (var Command = new OracleCommand("SELECT * FROM QUERY_SEQUENCE_REQUESTS", Conn))
            using (var reader = Command.ExecuteReader())
                while (reader.Read())
                    collection.Add(new QUERY_SEQUENCE_REQUESTS(reader));
            return collection;
        }

        public int TQR_ID;//NUMBER(10)	N			
        public int USER_QUERYING;//	NUMBER(10)	Y			
        public int DATABASE_QUERIED;//	NUMBER(10)	Y			
        public int QUERY_SEQUENCING;//NUMBER(10)	Y			
        public DateTime SEQ_QUERY_TIME;//DATE	Y			

        public QUERY_SEQUENCE_REQUESTS()
        {
        }

        public QUERY_SEQUENCE_REQUESTS(OracleDataReader reader)
        {
            TQR_ID = pTQR_ID(reader);
            USER_QUERYING = pUSER_QUERYING(reader);
            DATABASE_QUERIED = pDATABASE_QUERIED(reader);
            QUERY_SEQUENCING = pQUERY_SEQUENCING(reader);
            SEQ_QUERY_TIME = pSEQ_QUERY_TIME(reader);
        }
    

        int pTQR_ID(OracleDataReader reader)
        {
            return (reader["TQR_ID"] != DBNull.Value) ? Convert.ToInt32(reader["TQR_ID"]) : new int();
        }

        int pUSER_QUERYING(OracleDataReader reader)
        {
            return (reader["USER_QUERYING"] != DBNull.Value) ? Convert.ToInt32(reader["USER_QUERYING"]) : new int();
        }

        int pDATABASE_QUERIED(OracleDataReader reader)
        {
            return (reader["DATABASE_QUERIED"] != DBNull.Value) ? Convert.ToInt32(reader["DATABASE_QUERIED"]) : new int();
        }

        int pQUERY_SEQUENCING(OracleDataReader reader)
        {
            return (reader["QUERY_SEQUENCING"] != DBNull.Value) ? Convert.ToInt32(reader["QUERY_SEQUENCING"]) : new int();
        }

        DateTime pSEQ_QUERY_TIME(OracleDataReader reader)
        {
            return (reader["SEQ_QUERY_TIME"] != DBNull.Value) ? Convert.ToDateTime(reader["SEQ_QUERY_TIME"]) : new DateTime();
        }



        public override bool Insert(OracleConnection Conn)
        {
            string sql = string.Empty;
            sql += "insert into query_sequence_requests ";
            sql += "(TQR_ID, USER_QUERYING, DATABASE_QUERIED, QUERY_SEQUENCING, SEQ_QUERY_TIME) ";
            sql += "values (:a, :b, :c, :d, :e)";

            using (var Command = new OracleCommand(sql, Conn))
            {
                TQR_ID = Utilities.SequenceNextValue(Sequences.ID_GEN);
                Command.Parameters.Add(":a", OracleDbType.Int32).Value = TQR_ID;
                Command.Parameters.Add(":b", OracleDbType.Int32).Value = USER_QUERYING;
                Command.Parameters.Add(":c", OracleDbType.Int32).Value = DATABASE_QUERIED;
                Command.Parameters.Add(":d", OracleDbType.Int32).Value = QUERY_SEQUENCING;
                Command.Parameters.Add(":e", OracleDbType.Date).Value = SEQ_QUERY_TIME;
                var r = Convert.ToBoolean(Command.ExecuteNonQuery());
                if (OnInsert != null) OnInsert(this, EventArgs.Empty);
                return r;
            }
        }

        public override bool Update(OracleConnection Conn)
        {
            string sql = string.Empty;
            sql += "update query_sequence_requests set ";
            sql += "USER_QUERYING = :b, DATABASE_QUERIED = :c, QUERY_SEQUENCING = :d, SEQ_QUERY_TIME = :e ";
            sql += "where TQR_ID = :a ";

            using (var Command = new OracleCommand(sql, Conn))
            {
                Command.Parameters.Add(":b", OracleDbType.Int32).Value = USER_QUERYING;
                Command.Parameters.Add(":c", OracleDbType.Int32).Value = DATABASE_QUERIED;
                Command.Parameters.Add(":d", OracleDbType.Int32).Value = QUERY_SEQUENCING;
                Command.Parameters.Add(":e", OracleDbType.Date).Value = SEQ_QUERY_TIME;
                Command.Parameters.Add(":a", OracleDbType.Int32).Value = TQR_ID;

                var r = Convert.ToBoolean(Command.ExecuteNonQuery());
                if (OnUpdate != null) OnUpdate(this, EventArgs.Empty);
                return r;
            }
        }

        public override bool Delete(OracleConnection Conn)
        {
            string sql = string.Empty;
            sql += "DELETE FROM QUERY_SEQUENCE_REQUESTS WHERE TQR_ID = :a";
            using (var Command = new OracleCommand(sql, Conn))
            {
                Command.Parameters.Add(":a", OracleDbType.Int32).Value = TQR_ID;
                var r = Convert.ToBoolean(Command.ExecuteNonQuery());
                if (OnDelete != null) OnDelete(this, EventArgs.Empty);
                return r;
            }
        }
    }
}