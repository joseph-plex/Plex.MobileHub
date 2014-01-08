using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.DataAccess.Client;

namespace Plex.PMH.Data.Tables
{
    public class PMH_SETTINGS : PlexxisDataTransferObjects
    {
        public static event Subscriber OnInsert;
        public static event Subscriber OnUpdate;
        public static event Subscriber OnDelete;

        public int PMH_ID;//	NUMBER(10)	N			

        public static IEnumerable<PMH_SETTINGS> GetAll()
        {
            using (var Conn = Utilities.GetConnection(true))
                return GetAll(Conn);
        }

        public static IEnumerable<PMH_SETTINGS> GetAll(OracleConnection Conn)
        {
            var collection = new List<PMH_SETTINGS>();
            using (var Command = new OracleCommand("SELECT * FROM PMH_SETTINGS", Conn))
            using (var reader = Command.ExecuteReader())
                while (reader.Read())
                    collection.Add(new PMH_SETTINGS(reader));
            return collection;
        }

        public PMH_SETTINGS()
        { 
        }

        public PMH_SETTINGS(OracleDataReader reader)
        {
            PMH_ID = pPMH_ID(reader);
        }


        int pPMH_ID(OracleDataReader reader)
        {
            return (reader["PMH_ID"] != DBNull.Value) ? Convert.ToInt32(reader["PMH_ID"]) : new int();
        }

        public override bool Insert(OracleConnection Conn)
        {
            string sql = string.Empty;
            sql += "INSERT INTO PMH_SETTINGS (PMH_ID) VALUES (:a)";

            using (var Command = new OracleCommand(sql, Conn))
            {
                Command.Parameters.Add(":a", ResolveType(PMH_ID)).Value = PMH_ID;
                var r = Convert.ToBoolean(Command.ExecuteNonQuery());
                if (OnInsert != null) OnInsert(this, EventArgs.Empty);
                return r;
            }
        }

        public override bool Update(OracleConnection Conn)
        {
            //not meaningful to have an update here presently
            string sql = string.Empty;
            sql += "UPDATE PMH_SETTINGS SET PMH_ID = PMH_ID";

            using (var Command = new OracleCommand(sql, Conn))
            {
                var r = Convert.ToBoolean(Command.ExecuteNonQuery());
                if (OnUpdate != null) OnDelete(this, EventArgs.Empty);
                return r;
            }
        }

        public override bool Delete(OracleConnection Conn)
        {
            string sql = string.Empty;
            sql += "DELETE FROM PMH_SETTINGS WHERE PMH_ID = :a";

            using (var Command = new OracleCommand(sql, Conn))
            {
                Command.Parameters.Add(":a", PMH_ID).Value = PMH_ID;
                return Convert.ToBoolean(Command.ExecuteNonQuery());
            }
        }
    }
}