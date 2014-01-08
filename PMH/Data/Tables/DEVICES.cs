using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Oracle.DataAccess.Client;

namespace Plex.PMH.Data.Tables
{
    public class DEVICES : PlexxisDataTransferObjects
    {
        public static event Subscriber OnInsert;
        public static event Subscriber OnUpdate;
        public static event Subscriber OnDelete;

        public static IEnumerable<DEVICES> GetAll()
        {
            throw new NotImplementedException();
        }

        public static IEnumerable<DEVICES> GetAll(OracleConnection Conn)
        {
            throw new NotImplementedException();
        }

        public int DEVICE_ID;

        public DEVICES()
        {
        }

        public DEVICES(OracleDataReader reader)
        {
            DEVICE_ID = pDEVICE_ID(reader);
        }

        int pDEVICE_ID(OracleDataReader reader)
        {
            return (reader["DEVICE_ID"] != DBNull.Value) ? Convert.ToInt32(reader["DEVICE_ID"]) : new int();
        }


        public override bool Insert(OracleConnection Conn)
        {
            string sql = string.Empty;
            sql += "INSERT INTO DEVICES(DEVICE_ID) VALUES (:a);";

            using (var Command = new OracleCommand(sql, Conn))
            {
                Command.Parameters.Add(":a", ResolveType(DEVICE_ID)).Value = DEVICE_ID;
                var r = Convert.ToBoolean(Command.ExecuteNonQuery());
                if (OnInsert != null) OnInsert(this, EventArgs.Empty);
                return r;
            }
        }

        public override bool Update(OracleConnection Conn)
        {
            throw new NotImplementedException();
        }

        public override bool Delete(OracleConnection Conn)
        {
            throw new NotImplementedException();
        }
    }
}