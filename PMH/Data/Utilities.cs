using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.DataAccess.Client;
using System.Data;

using Plex.PMH.Objects;

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


        public static QueryResult Query(string Query)
        {
            QueryResult r = new QueryResult();
            using (var Conn = GetConnection(true))
            {
                using (var Comm = new OracleCommand(Query, Conn))
                {
                    using (var reader = Comm.ExecuteReader(CommandBehavior.KeyInfo | CommandBehavior.CloseConnection))
                    {
                        r.Columns = new List<Column>(GetColumnData(reader.GetSchemaTable()));
                        while (reader.Read())
                        {
                            var CurrentRow = new Row();
                            foreach (var Col in r.Columns)
                                CurrentRow.Values.Add((reader[Col.ColumnName] != DBNull.Value) ? reader[Col.ColumnName] : null);
                            r.Rows.Add(CurrentRow);
                        }
                    }
                }
            }
            return r;
        }

        static IEnumerable<Column> GetColumnData(DataTable dataTable)
        {
            foreach (var Row in dataTable.Rows)
                yield return GetColumnData((DataRow)Row);
        }
        static Column GetColumnData(DataRow Collection)
        {
            return new Column()
            {
                ColumnName = (Collection[0] != DBNull.Value) ? Convert.ToString(Collection[0]) : string.Empty,
                //ColumnOrdinal = (Collection[1] != DBNull.Value) ? Convert.ToInt32(Collection[1]) : (int?)null,
                //ColumnSize = (Collection[2] != DBNull.Value) ? Convert.ToInt32(Collection[2]) : (int?)null,

                //NumericPrecision = (Collection[3] != DBNull.Value) ? Convert.ToInt16(Collection[3]) : (short?)null,
                //NumericScale = (Collection[4] != DBNull.Value) ? Convert.ToInt16(Collection[4]) : (short?)null,
                IsUnique = (Collection[5] != DBNull.Value) ? Convert.ToBoolean(Collection[5]) : (bool?)null,

                IsKey = (Collection[6] != DBNull.Value) ? Convert.ToBoolean(Collection[6]) : (bool?)null,
                //IsRowID = (Collection[7] != DBNull.Value) ? Convert.ToBoolean(Collection[7]) : (bool?)null,
                //BaseColumnName = (Collection[8] != DBNull.Value) ? Convert.ToString(Collection[8]) : string.Empty,

                //BaseSchemaName = (Collection[9] != DBNull.Value) ? Convert.ToString(Collection[9]) : string.Empty,
                //BaseTableName = (Collection[10] != DBNull.Value) ? Convert.ToString(Collection[10]) : string.Empty,
                DataType = (Collection[11] != DBNull.Value) ? Collection[11].ToString() : string.Empty,

                //ProviderType = (Collection[12] != DBNull.Value) ? GetOracleDbType(Collection[12]).ToString() : string.Empty,
                //AllowDBNull = (Collection[13] != DBNull.Value) ? Convert.ToBoolean(Collection[13]) : (bool?)null,
                //IsAliased = (Collection[14] != DBNull.Value) ? Convert.ToBoolean(Collection[14]) : (bool?)null,

                //IsByteSemantic = (Collection[15] != DBNull.Value) ? Convert.ToBoolean(Collection[15]) : (bool?)null,
                //IsExpression = (Collection[16] != DBNull.Value) ? Convert.ToBoolean(Collection[16]) : (bool?)null,
                //IsHidden = (Collection[17] != DBNull.Value) ? Convert.ToBoolean(Collection[17]) : (bool?)null,

                IsReadOnly = (Collection[18] != DBNull.Value) ? Convert.ToBoolean(Collection[18]) : (bool?)null,
                IsLong = (Collection[19] != DBNull.Value) ? Convert.ToBoolean(Collection[19]) : (bool?)null,
                //UdtTypeName = (Collection[20] != DBNull.Value) ? Convert.ToString(Collection[20]) : string.Empty
            };
        }
        static OracleDbType GetOracleDbType(Object obj)
        {
            if (obj is OracleDbType)
                return (OracleDbType)obj;
            else
                return OracleDbType.Varchar2;
        }
    }
}