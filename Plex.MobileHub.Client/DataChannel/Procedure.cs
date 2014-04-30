//using System;
//using System.Data;

//namespace Client.Core.DataChannel
//{
//    public static class Procedure
//    {
//        public static T Invoke<T>(this IDbConnection conn, string Name, params object[] Arguments)
//        {
//            using (var Command = conn.CreateCommand())
//            {
//                Command.CommandType = CommandType.StoredProcedure;
//                Command.CommandText = Name;

//                var ret = Command.CreateParameter();
//                ret.Value = default(T);
//                ret.Direction = ParameterDirection.ReturnValue;
//                Command.Parameters.Add(ret);

//                foreach (var arg in Arguments)
//                {
//                    var variable = Command.CreateParameter();
//                    variable.Value = arg;
//                    Command.Parameters.Add(variable);
//                }
//                Command.ExecuteNonQuery();
//                return (T)Convert.ChangeType(ret.Value, typeof(T));
//            }
//        }
//    }
//}
