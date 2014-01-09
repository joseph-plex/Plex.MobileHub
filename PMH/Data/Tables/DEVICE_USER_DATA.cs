using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Oracle.DataAccess.Client;

namespace Plex.PMH.Data.Tables
{
    public class DEVICE_USER_DATA : PlexxisDataTransferObjects
    {

        public static event Subscriber OnInsert;
        public static event Subscriber OnUpdate;
        public static event Subscriber OnDelete;

        public static IEnumerable<DEVICE_USER_DATA> GetAll()
        {
            using (var Conn = Utilities.GetConnection(true))
                return GetAll(Conn);
        }

        public static IEnumerable<DEVICE_USER_DATA> GetAll(OracleConnection Conn)
        {
            var collection = new List<DEVICE_USER_DATA>();
            using (var Command = new OracleCommand("SELECT * FROM DEVICE_USER_DATA", Conn))
            using (var reader = Command.ExecuteReader())
                while (reader.Read())
                    collection.Add(new DEVICE_USER_DATA(reader));
            return collection;
        }

        public int USER_DATA_ID;
        public int DEVICE_ID;
        public int USER_PERMISSION;
        public DateTime? EXECUTION_INITIATION;
        public DateTime? EXECUTION_COMPLETION;

        public DEVICE_USER_DATA()
        { 
        }

        public DEVICE_USER_DATA(OracleDataReader reader)
        {
            USER_DATA_ID = pUSER_DATA_ID(reader);
            DEVICE_ID = pDEVICE_ID(reader);
            USER_PERMISSION = pUSER_PERMISSION(reader);
            EXECUTION_INITIATION = pEXECUTION_INITIATION(reader);
            EXECUTION_COMPLETION = pEXECUTION_COMPLETION(reader);
        }

        int pUSER_DATA_ID(OracleDataReader reader)
        {
            return (reader["USER_DATA_ID"] != DBNull.Value) ? Convert.ToInt32(reader["USER_DATA_ID"]) : new int();
        }
        int pDEVICE_ID(OracleDataReader reader)
        {
            return (reader["DEVICE_ID"] != DBNull.Value) ? Convert.ToInt32(reader["DEVICE_ID"]) : new int();
        }
        int pUSER_PERMISSION(OracleDataReader reader)
        {
            return (reader["USER_PERMISSION"] != DBNull.Value) ? Convert.ToInt32(reader["USER_PERMISSION"]) : new int();
        }
        DateTime? pEXECUTION_INITIATION(OracleDataReader reader)
        {
            return (reader["EXECUTION_INITIATION"] != DBNull.Value) ? Convert.ToDateTime(reader["EXECUTION_INITIATION"]) : (DateTime?)null;
        }
        DateTime? pEXECUTION_COMPLETION(OracleDataReader reader)
        {
            return (reader["EXECUTION_COMPLETION"] != DBNull.Value) ? Convert.ToDateTime(reader["EXECUTION_COMPLETION"]) : (DateTime?)null;
        }


        public override bool Insert(OracleConnection Conn)
        {
            string sql = string.Empty;
            sql += "INSERT INTO DEVICE_USER_DATA(USER_DATA_ID,DEVICE_ID,USER_PERMISSION)";
            sql += "values (:a, :b, :c)";

            using (var Command = new OracleCommand(sql, Conn))
            {
                USER_DATA_ID = Utilities.SequenceNextValue(Sequences.DEVICE_ID);
                Command.Parameters.Add(":a", ResolveType(USER_DATA_ID)).Value = USER_DATA_ID;
                Command.Parameters.Add(":b", ResolveType(DEVICE_ID)).Value = DEVICE_ID;
                Command.Parameters.Add(":c", ResolveType(USER_PERMISSION)).Value = USER_PERMISSION;

                var r = Convert.ToBoolean(Command.ExecuteNonQuery());
                if (OnInsert != null) OnInsert(this, EventArgs.Empty);
                return r;
            }
        }

        public override bool Update(OracleConnection Conn)
        {
            string sql = string.Empty;
            sql += "UPDATE DEVICE_USER_DATA SET ";
            sql += "DEVICE_ID = :b , USER_PERMISSION = :c ";
            sql += "WHERE USER_DATA_ID = :a";
            using (var Command = new OracleCommand(sql, Conn))
            {
                Command.Parameters.Add(":b", ResolveType(DEVICE_ID)).Value = DEVICE_ID;
                Command.Parameters.Add(":c", ResolveType(USER_PERMISSION)).Value = USER_PERMISSION;
                Command.Parameters.Add(":a", ResolveType(USER_DATA_ID)).Value = USER_DATA_ID;

                var r = Convert.ToBoolean(Command.ExecuteNonQuery());
                if (OnUpdate != null) OnUpdate(this, EventArgs.Empty);
                return r;
            }
        } 

        public override bool Delete(OracleConnection Conn)
        {
            string sql = string.Empty;
            sql += "DELETE FROM DEVICE_USER_DATA WHERE USER_DATA_ID = :a";
            using (var Command = new OracleCommand(sql, Conn))
            {
                Command.Parameters.Add(":a", ResolveType(USER_DATA_ID)).Value = USER_DATA_ID;
                var r = Convert.ToBoolean(Command.ExecuteNonQuery());
                if (OnDelete != null) OnDelete(this, EventArgs.Empty);
                return r;
            }
        }
    }
}