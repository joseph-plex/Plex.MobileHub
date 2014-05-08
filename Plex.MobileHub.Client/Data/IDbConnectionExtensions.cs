using System;
using System.Data;
using System.Collections.Generic;

namespace MobileHubClient.Data
{
    public static class IDbConnectionExtensions
    {
        public static IDbCommand CreateCommand(this IDbConnection conn, string commandText, params object [] parameters)
        {
            if (conn == null) throw new ArgumentException("Conn cannot be null");
            if (parameters == null) throw new ArgumentException("parameters cannot be null");

            var Command = conn.CreateCommand();
            Command.CommandText = commandText;
            foreach (var p in parameters)
                Command.Parameters.Add(Command.CreateParameter(p));
            return Command;
        }
        public static IDbDataParameter CreateParameter(this IDbCommand command, object value)
        {
            if (command == null) throw new ArgumentException("Command cannot be null");
            var Param = command.CreateParameter();
            Param.Value = value;
            return Param;
        }
        public static T InvokeProcedure<T>(this IDbConnection conn, string name, params object[] arguments)
        {
            if (conn == null) throw new ArgumentException("Conn cannot be null");
            if (arguments == null) throw new ArgumentException("arguments cannot be null");

            using (var Command = conn.CreateCommand(name))
            {
                Command.CommandType = CommandType.StoredProcedure;

                var ret = Command.CreateParameter(default(T));
                ret.Direction = ParameterDirection.ReturnValue;
                Command.Parameters.Add(ret);

                foreach (var arg in arguments)
                    Command.Parameters.Add(Command.CreateParameter(arg));

                Command.ExecuteNonQuery();
                return (T)Convert.ChangeType(ret.Value, typeof(T));
            }
        }
        public static Result Query(this IDbConnection conn, String commandText, params object[] arguments)
        {
            Result r = new Result();
            using (var Comm = conn.CreateCommand(commandText, arguments))
            using (var reader = Comm.ExecuteReader(CommandBehavior.KeyInfo))
            {
                r.Columns = new List<Col>(GetColumnData(reader.GetSchemaTable()));
                for (var Curr = new Row(); reader.Read(); r.Rows.Add(Curr), Curr = new Row())
                    for (int i = 0; i < r.Columns.Count; i++)
                        Curr.Values.Add((reader[r.Columns[i].ColumnName] != DBNull.Value) ? reader[r.Columns[i].ColumnName] : null);
            }
            return r;
        }
        public static int NonQuery(this IDbConnection conn, String commandText, params object[] arguments)
        {
            using (var Comm = conn.CreateCommand(commandText, arguments))
                return Comm.ExecuteNonQuery();
        }
        internal static IEnumerable<Col> GetColumnData(DataTable dataTable)
        {
            foreach (var Row in dataTable.Rows)
                yield return GetColumnData((DataRow)Row);
        }
        internal static Col GetColumnData(DataRow collection)
        {
            return new Col()
            {
                ColumnName = (collection[0] != DBNull.Value) ? Convert.ToString(collection[0]) : string.Empty,
                IsUnique = (collection[5] != DBNull.Value) ? Convert.ToBoolean(collection[5]) : (bool?)null,

                IsKey = (collection[6] != DBNull.Value) ? Convert.ToBoolean(collection[6]) : (bool?)null,
                DataType = (collection[11] != DBNull.Value) ? collection[11].ToString() : string.Empty,
                IsReadOnly = (collection[18] != DBNull.Value) ? Convert.ToBoolean(collection[18]) : (bool?)null,
                IsLong = (collection[19] != DBNull.Value) ? Convert.ToBoolean(collection[19]) : (bool?)null,
            };
        }
    }
}
