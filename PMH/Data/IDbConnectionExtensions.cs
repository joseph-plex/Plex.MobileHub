using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;

using Plex.PMH.Data.Types;

namespace Plex.PMH.Data
{
    public static class IDbConnectionExtensions
    {
        public static IDbCommand CreateCommand(this IDbConnection Conn, string CommandText, params object[] Parameters)
        {
            var Command = Conn.CreateCommand();
            Command.CommandText = CommandText;
            foreach (var p in Parameters)
                Command.Parameters.Add(Command.CreateParameter(p));
            return Command;
        }
        public static IDbDataParameter CreateParameter(this IDbCommand Command, object  Value)
        {
            var Param = Command.CreateParameter();
            Param.Value = Value;
            return Param;
        }
        public static Result Query(this IDbConnection conn, String CommandText, params object[] Arguments)
        {
            Result r = new Result();
            using (var Comm = conn.CreateCommand(CommandText, Arguments))
            using (var reader = Comm.ExecuteReader(CommandBehavior.KeyInfo))
            {
                r.Columns = new List<Col>(GetColumnData(reader.GetSchemaTable()));
                for (var Curr = new Row(); reader.Read(); r.Rows.Add(Curr), Curr = new Row())
                    for (int i = 0; i < r.Columns.Count; i++)
                        Curr.Values.Add((reader[r.Columns[i].ColumnName] != DBNull.Value) ? reader[r.Columns[i].ColumnName] : null);
            }
            return r;
        }
        public static int NonQuery(this IDbConnection conn, String CommandText, params object[] Arguments)
        {
            using (var Comm = conn.CreateCommand(CommandText,Arguments))
                return Comm.ExecuteNonQuery();
        }
        static IEnumerable<Col> GetColumnData(DataTable dataTable)
        {
            foreach (var Row in dataTable.Rows)
                yield return GetColumnData((DataRow)Row);
        }
        static Col GetColumnData(DataRow Collection)
        {
            return new Col()
            {
                ColumnName = (Collection[0] != DBNull.Value) ? Convert.ToString(Collection[0]) : string.Empty,
                IsUnique = (Collection[5] != DBNull.Value) ? Convert.ToBoolean(Collection[5]) : (bool?)null,

                IsKey = (Collection[6] != DBNull.Value) ? Convert.ToBoolean(Collection[6]) : (bool?)null,
                DataType = (Collection[11] != DBNull.Value) ? Collection[11].ToString() : string.Empty,
                IsReadOnly = (Collection[18] != DBNull.Value) ? Convert.ToBoolean(Collection[18]) : (bool?)null,
                IsLong = (Collection[19] != DBNull.Value) ? Convert.ToBoolean(Collection[19]) : (bool?)null,
            };
        }
    }

}