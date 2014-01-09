using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.DataAccess.Client;
using System.Data;
namespace Plex.PMH.Data
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

        public static OracleConnection GetConnection(bool IsOpened = false)
        {
            var conn = new OracleConnection(ConnectionString);
            if (IsOpened) conn.Open();
            return conn;
        }


        public static int SequenceNextValue(Sequences Seq)
        {
            string sql = @"select ^seq^.nextval as num from dual";
            switch (Seq)
            {
                case Sequences.DEVICE_ID:
                    sql = sql.Replace("^seq^", "DEVICE_ID");
                    break;
                case Sequences.ID_GEN:
                    sql = sql.Replace("^seq^", "ID_GEN");
                    break;
                default:
                    throw new NotSupportedException();
            }

            using (var Conn = GetConnection(true))
            using (var Comm = new OracleCommand(sql, Conn))
            using (var reader = Comm.ExecuteReader(CommandBehavior.CloseConnection))
                if (reader.Read()) return Convert.ToInt32(reader["num"]);
            throw new NotSupportedException();
        }
    }
}