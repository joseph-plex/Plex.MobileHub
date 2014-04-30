//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Oracle.DataAccess.Client;
//using Oracle.DataAccess.Types;
//using System.Data;
//namespace Client.Core
//{
//    public static partial class DataChannel
//    {
//        //function names for the PMH
//        #region PMH Client Database Function Names
//        private const string fQueryCreateSyncTrigger = "PMH.QueryCreateSyncTrigger";
//        private const string fQueryCreate = "PMH.QueryCreate";
//        private const string fQueryColumnCreate = "PMH.QueryColumnCreate";
//        private const string fQueryColumnExists = "PMH.QueryColumnExists";
//        private const string fQueryConditionCreate = "PMH.QueryConditionCreate";

//        private const string fQueryExists = "PMH.QueryExists";
//        private const string fQueryGetConditionSql = "PMH.QueryGetConditionSql";
//        private const string fQueryGetNextSequence = "PMH.QueryGetNextSequence";
//        private const string fQueryGetSql = "PMH.QueryGetSql";

//        private const string fQueryGetTableName = "PMH.QueryGetTableName";
//        private const string fQuerySyncData = "PMH.QuerySyncData";
//        private const string fQueryTableCreate = "PMH.QueryTableCreate";
//        private const string fQueryTableExists = "PMH.QueryTableExists";
//        #endregion
//        //todo create custom exception for this.
//        private const string Param = "arg";
//        private const string Return = "ret";

//        public static int QueryCreate(OracleConnection Conn, int AppId, int QueryId, string Description, string TableName)
//        {
//            using (OracleCommand Command = new OracleCommand(fQueryCreate, Conn))
//            {
//                Command.CommandType = CommandType.StoredProcedure;
//                Command.Parameters.Add(new OracleParameter(Return, OracleDbType.Int32, ParameterDirection.ReturnValue));
//                Command.Parameters.Add(new OracleParameter(Param, GetOracleDbType(AppId), ParameterDirection.Input)).Value = AppId;
//                Command.Parameters.Add(new OracleParameter(Param, GetOracleDbType(QueryId), ParameterDirection.Input)).Value = QueryId;
//                Command.Parameters.Add(new OracleParameter(Param, GetOracleDbType(Description), ParameterDirection.Input)).Value = Description;
//                Command.Parameters.Add(new OracleParameter(Param, GetOracleDbType(TableName), ParameterDirection.Input)).Value = TableName;

//                Command.ExecuteNonQuery();
//                OracleDecimal r = (OracleDecimal)Command.Parameters[Return].Value;
//                int i = r.ToInt32();
//                switch (i)
//                {
//                    case -1:
//                        throw new Exception("Query Id cannot be 0");
//                    case -2:
//                        throw new Exception("Description cannot be empty");
//                    case -3:
//                        throw new Exception("Table name cannot be empty");
//                    case -4:
//                        throw new Exception("Query Cannot Already Exists");
//                    case -5:
//                        throw new Exception("Table has the matching IUD table required for getting delta changes");
//                }
//                return i;
//            }
//        }
//        public static bool QueryExists(OracleConnection Conn, int QueryId)
//        {
//            using (OracleCommand Command = new OracleCommand(fQueryExists, Conn))
//            {
//                Command.CommandType = CommandType.StoredProcedure;
//                Command.Parameters.Add(new OracleParameter(Return, OracleDbType.Int32, ParameterDirection.ReturnValue));
//                Command.Parameters.Add(new OracleParameter(Param, GetOracleDbType(QueryId), ParameterDirection.Input)).Value = QueryId;
//                Command.ExecuteNonQuery();
//                OracleDecimal r = (OracleDecimal)Command.Parameters[Return].Value;
//                return Convert.ToBoolean(r.ToInt32());
//            }
//        }
 
//        public static int QueryColumnCreate(OracleConnection Conn, int QueryId, int ColumnId, string ColumnName, int Seq)
//        {
//            using (OracleCommand Command = new OracleCommand(fQueryColumnCreate, Conn))
//            {
//                Command.CommandType = CommandType.StoredProcedure;
//                Command.Parameters.Add(new OracleParameter(Return, OracleDbType.Int32, ParameterDirection.ReturnValue));

//                Command.Parameters.Add(new OracleParameter(Param, GetOracleDbType(QueryId), ParameterDirection.Input)).Value = QueryId;
//                Command.Parameters.Add(new OracleParameter(Param, GetOracleDbType(ColumnId), ParameterDirection.Input)).Value = ColumnId;
//                Command.Parameters.Add(new OracleParameter(Param, GetOracleDbType(ColumnName), ParameterDirection.Input)).Value = ColumnName;
//                Command.Parameters.Add(new OracleParameter(Param, GetOracleDbType(Seq), ParameterDirection.Input)).Value = Seq;
//                Command.ExecuteNonQuery();
//                OracleDecimal r = (OracleDecimal)Command.Parameters[Return].Value;
//                int i = r.ToInt32();
//                switch (i)
//                {
//                    case -1:
//                        throw new Exception("Query Id cannot be 0");
//                    case -2:
//                        throw new Exception("Column Name cannot be empty");
//                    case -3:
//                        throw new Exception("Query Column alredy exists");
//                    case -99:
//                        throw new Exception("An unknown error has occured");
//                }
//                return i;
//            }
//        }
//        public static bool QueryColumnExists(OracleConnection Conn, int QueryId, string ColumnName)
//        {
//            using (OracleCommand Command = new OracleCommand(fQueryColumnExists, Conn))
//            {
//                Command.CommandType = CommandType.StoredProcedure;
//                Command.Parameters.Add(new OracleParameter(Return,OracleDbType.Int32, ParameterDirection.ReturnValue));
//                Command.Parameters.Add(new OracleParameter(Param, GetOracleDbType(QueryId), ParameterDirection.Input)).Value = QueryId;
//                Command.Parameters.Add(new OracleParameter(Param, GetOracleDbType(ColumnName), ParameterDirection.Input)).Value = ColumnName;

//                Command.ExecuteNonQuery();
//                OracleDecimal r = (OracleDecimal)Command.Parameters[Return].Value;
//                int i = r.ToInt32();
//                switch (i)
//                {
//                    case -1:
//                        throw new Exception("Query Id Cannot of 0");
//                    case -2:
//                        throw new Exception("Column Name cannot be null");
//                    case -99:
//                        return false;
//                }
//                if(i != 0)
//                    return false;
//                return true;
//            }
//        }
//        public static int QueryConditionCreate(OracleConnection Conn, int QueryId, string ColumnName, string ColumnNVL, string ColumnValue, string Operator)
//        {
//            using (OracleCommand Command = new OracleCommand(fQueryConditionCreate, Conn))
//            {
//                Command.CommandType = CommandType.StoredProcedure;
//                Command.Parameters.Add(new OracleParameter(Return, OracleDbType.Int32, ParameterDirection.ReturnValue));

//                Command.Parameters.Add(new OracleParameter(Param, GetOracleDbType(QueryId), ParameterDirection.Input)).Value = QueryId;
//                Command.Parameters.Add(new OracleParameter(Param, GetOracleDbType(ColumnName), ParameterDirection.Input)).Value = ColumnName;
//                Command.Parameters.Add(new OracleParameter(Param, GetOracleDbType(ColumnNVL), ParameterDirection.Input)).Value = ColumnNVL;
//                Command.Parameters.Add(new OracleParameter(Param, GetOracleDbType(ColumnValue), ParameterDirection.Input)).Value = ColumnValue;
//                Command.Parameters.Add(new OracleParameter(Param, GetOracleDbType(Operator), ParameterDirection.Input)).Value = Operator;
//                Command.ExecuteNonQuery();
//                OracleDecimal r = (OracleDecimal)Command.Parameters[Return].Value;
//                int i = r.ToInt32();
//                switch (i)
//                {
//                    case -1:
//                        throw new Exception("Query Id cannot be 0");
//                    case -2:
//                        throw new Exception("Column Nmae cannot be empty");
//                    case -3:
//                        throw new Exception("Column value cannot be empty");
//                    case -4:
//                        throw new Exception("Operator is not the list of specified usable operators");
//                    case -99:
//                        throw new Exception("unknown error has occured");
//                }
//                return i;
//            }
//        }
//        public static bool QueryConditionExists(OracleConnection Conn)
//        {
//            //todo implement QueryConditionExists
//            throw new NotImplementedException();
//        }

//        public static int RegisterApp(OracleConnection Conn, int AppId, string AppTitle, string AppDescription)
//        {
//            string sql = "insert into pmh$_apps(app_id, title, description) values (:a,:b,:c)";
//            using (var Command = new OracleCommand(sql, Conn))
//            {
//                Command.Parameters.Add(":a", GetOracleDbType(AppId)).Value = AppId;
//                Command.Parameters.Add(":b", GetOracleDbType(AppTitle)).Value = AppTitle;
//                Command.Parameters.Add(":c", GetOracleDbType(AppDescription)).Value = AppDescription;
//                return Command.ExecuteNonQuery();
//            }
//        }
//        public static bool IsAppRegistered(OracleConnection Conn, int AppId)
//        {
//            string sql = "select * from pmh$_apps where APP_Id=:a";
//            using (var Command = new OracleCommand(sql, Conn))
//            {
//                Command.Parameters.Add(":a", GetOracleDbType(AppId)).Value = AppId;
//                using (var reader = Command.ExecuteReader())
//                    return reader.HasRows;
//            }          
//        }

//        public static int QuerySyncData(OracleConnection Conn, int QueryId)
//        {
//            using (OracleCommand Command = new OracleCommand(fQuerySyncData, Conn))
//            {
//                Command.CommandType = CommandType.StoredProcedure;
//                Command.Parameters.Add(new OracleParameter(Return, OracleDbType.Int32, ParameterDirection.ReturnValue));
//                Command.Parameters.Add(new OracleParameter(Param, GetOracleDbType(QueryId), ParameterDirection.Input)).Value = QueryId;

//                Command.ExecuteNonQuery();
//                OracleDecimal r = (OracleDecimal)Command.Parameters[Return].Value;
//                int i = r.ToInt32();
//                switch (i)
//                {
//                    case -1:
//                        throw new Exception("An expected exception has occurred");
//                }
//                return i;
//            }
//        }
//        public static int QueryCreateSyncTrigger(OracleConnection Conn,int QueryId)
//        {
//            using (var Command = new OracleCommand(fQueryCreateSyncTrigger, Conn))
//            {
//                Command.CommandType = CommandType.StoredProcedure;
//                Command.Parameters.Add(Return,OracleDbType.Int32,ParameterDirection.ReturnValue);
//                Command.Parameters.Add(Param, GetOracleDbType(QueryId)).Value = QueryId;
//                Command.ExecuteNonQuery();
//                OracleDecimal r = (OracleDecimal)Command.Parameters[Return].Value;
//                int i = r.ToInt32();
//                switch(i)
//                {
//                    case 0:
//                        return i;
//                    case -1:
//                        throw new Exception("Query Id cannot be null or 0");
//                    case -2:
//                        throw new Exception("Primary Key cannot be null");
//                    default:
//                        return i;
//                }
//            }
//        }
//        public static int QueryTableCreate(OracleConnection Conn, int QueryId)
//        {
//            using (var Command = new OracleCommand(fQueryTableCreate, Conn))
//            {
//                Command.CommandType = CommandType.StoredProcedure;
//                Command.Parameters.Add(Return, OracleDbType.Int32, ParameterDirection.ReturnValue);
//                Command.Parameters.Add(Param, GetOracleDbType(QueryId)).Value = QueryId;
//                Command.ExecuteNonQuery();
//                OracleDecimal r = (OracleDecimal)Command.Parameters[Return].Value;
//                int i = r.ToInt32();
//                switch (i)
//                {
//                    case 0:
//                        return i;
//                    case -1:
//                        throw new Exception("Query Id cannot be null or 0");
//                    case -2:
//                        throw new Exception("Query Must exist to call this function");
//                    default:
//                        return i;
//                }
//            }
//        }
//        public static string QueryGetSql(OracleConnection Conn, int QueryId, DateTime? Time)
//        {
//            string sql = "select PMH.QueryGetSql(:a,:b) as Query from dual";
//            using (var Command = new OracleCommand(sql, Conn))
//            {
//                Command.Parameters.Add(":a", OracleDbType.Int32, ParameterDirection.Input).Value = QueryId;
//                Command.Parameters.Add(":b", OracleDbType.TimeStamp, ParameterDirection.Input).Value = Time ?? OracleTimeStamp.MinValue;
//                using (var reader = Command.ExecuteReader())
//                {
//                    if (reader.Read())
//                        return reader["Query"].ToString();
//                    else
//                        return null;
//                }
//            }
//        }
//        public static List<int> QueriesGet(OracleConnection Conn, int AppId)
//        {
//            var Queries = new List<Int32>();
//            string sql = "select QUERY_ID from pmh$_app_queries where APP_ID = :a";
//            using (var Command = new OracleCommand(sql, Conn))
//            {
//                Command.Parameters.Add(":a", GetOracleDbType(AppId)).Value = AppId;
//                using (var reader = Command.ExecuteReader())
//                    while (reader.Read())
//                        Queries.Add(Convert.ToInt32(reader["QUERY_ID"]));
//            }
//            return Queries;
//        }

//        private static OracleDbType GetOracleDbType(object o)
//        {
//            if (o is string)
//                return OracleDbType.Varchar2;
//            if (o is DateTime || o is DateTime?)
//                return OracleDbType.Date;
//            if (o is Int64 || o is Int64?)
//                return OracleDbType.Int64;
//            if (o is Int32 || o is bool || o is Int32?)
//                return OracleDbType.Int32;
//            if (o is Int16 || o is Int16?)
//                return OracleDbType.Int16;
//            if (o is byte || o is byte?)
//                return OracleDbType.Byte;
//            if (o is decimal || o is decimal?)
//                return OracleDbType.Decimal;
//            if (o is float || o is float?)
//                return OracleDbType.Single;
//            if (o is double || o is double?)
//                return OracleDbType.Double;
//            if (o is byte[])
//                return OracleDbType.Blob;
//            return OracleDbType.Varchar2;
//        }
//    }
    
//}
