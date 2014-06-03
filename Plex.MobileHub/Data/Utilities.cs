using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.DataAccess.Client;
using System.Data;

using System.Reflection;
using Plex.MobileHub.Objects;
using Plex.MobileHub.Data.Types;
namespace Plex.MobileHub.Data
{

    public delegate void Subscriber(Object sender, EventArgs e);

    public static class Utilities
    {
        public static String ConnectionString
        {
            get;
            private set;
        }

        static Utilities()
        {
            string User = "C##PMH";
            string Pass = "!!!plex!!!sa";
            string Source = "(DESCRIPTION = (ADDRESS_LIST = (ADDRESS = (PROTOCOL = TCP)(HOST = 10.0.1.96)(PORT = 1521)))(CONNECT_DATA = (SERVICE_NAME = PLE1LIVE)))";
            Utilities.ConnectionString = "User Id=" + User + ";" + "Password=" + Pass + ";" + "Data Source=" + Source + ";";
        }

        public static IDbConnection GetConnection(bool IsOpened = false)
        {
            var conn = new OracleConnection(ConnectionString);
            if (IsOpened) conn.Open();
            return conn;
        }

        public static int GetNextSequenceValue(SequenceType Seq)
        {
            switch (Seq)
            {
                case SequenceType.ID_GEN:
                    return Convert.ToInt32(GetConnection(true).Query("select ID_GEN.nextval from dual").Rows.First()[0]);
                case SequenceType.DEVICE_ID:
                    return Convert.ToInt32(GetConnection(true).Query("select DEVICE_ID.nextval from dual").Rows.First()[0]);
                case SequenceType.DEV_DATA_SEQ:
                    return Convert.ToInt32(GetConnection(true).Query("select DEV_DATA_SEQ.nextval from dual").Rows.First()[0]);
                case SequenceType.DEV_DATA_VER_SEQ:
                    return Convert.ToInt32(GetConnection(true).Query("select DEV_DATA_VER_SEQ.nextval from dual").Rows.First()[0]);
                case SequenceType.DEV_DATA_VER_Q_SEQ:
                    return Convert.ToInt32(GetConnection(true).Query("select DEV_DATA_VER_Q_SEQ.nextval from dual").Rows.First()[0]);
                default:
                    throw new NotImplementedException();
            }
        }
    }

    public enum SequenceType
    {
        DEVICE_ID,
        ID_GEN,
        DEV_DATA_SEQ,
        DEV_DATA_VER_SEQ,
        DEV_DATA_VER_Q_SEQ
    }
}