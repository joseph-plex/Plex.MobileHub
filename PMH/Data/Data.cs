//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//using System.Reflection;
//using System.Data;

//using Oracle.DataAccess.Client;
//using Oracle.DataAccess.Types;

//namespace Plex.PMH.DataAccess
//{
//    public sealed class Access
//    {
//        private const String User = "C##PMH";
//        private const String Pass = "!!!plex!!!sa";
//        private const String Source = "(DESCRIPTION = (ADDRESS_LIST = (ADDRESS = (PROTOCOL = TCP)(HOST = 10.0.1.96)(PORT = 1521)))(CONNECT_DATA = (SERVICE_NAME = PLE1LIVE)))";
//        private const String DateFormat = "dd/MMM/yyyy";
//        private const String ConnectionString = "User Id=" + User + ";" + "Password=" + Pass + ";" + "Data Source=" + Source + ";";

//        public delegate void Subscriber(Object sender, EventArgs args);


//        static Access()
//        {
//            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);
//        }

//        public static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
//        {
//            string dll = args.Name.Contains(',') ? args.Name.Substring(0, args.Name.IndexOf(',')) : args.Name.Replace(".dll", "");
//            dll = dll.Replace(".", "_");

//            if (dll.EndsWith("_resources")) return null;

//            System.Resources.ResourceManager rm = new System.Resources.ResourceManager(typeof(Access).Namespace + ".Properties.Resources", System.Reflection.Assembly.GetExecutingAssembly());
//            byte[] bytes = (byte[])rm.GetObject(dll);
//            return Assembly.Load(bytes);
//        }

//        public static int GetId()
//        {
//            using (var Conn = new OracleConnection(ConnectionString))
//            {
//                using (var Command = new OracleCommand("PMH.GetId", Conn))
//                {
//                    Command.CommandType = CommandType.StoredProcedure;
//                    Command.Parameters.Add("ret", OracleDbType.Int32, ParameterDirection.ReturnValue);
//                    Conn.Open();
//                    Command.ExecuteNonQuery();

//                    return Convert.ToInt32(Command.Parameters["ret"].Value.ToString());
//                }
//            }
//        }

//        private static int GetDeviceId()
//        {
//            string sql = "select device_id.nextval as num from dual";
//            using (var Conn = new OracleConnection(ConnectionString))
//            using (var Command = new OracleCommand(sql, Conn))
//            {

//                Conn.Open();
//                using (var reader = Command.ExecuteReader(CommandBehavior.CloseConnection))
//                    if (reader.Read())
//                        return Convert.ToInt32(reader["NUM"]);
//                    else
//                        throw new Exception("Reader has no rows");
//            }
//        }

//        public static OracleConnection GetConnection()
//        {
//            return new OracleConnection(ConnectionString);
//        }

//        public static Boolean IsValidLogin(int ClientId, int AppId, String Database, String User, String Password)
//        {
//            string sql = string.Empty;
//            sql += @"
//                select count(*) as c
//                from CLIENT_USERS cu, CLIENT_DB_COMPANIES dbs, APP_CLIENTS ac, CLIENT_DB_COMPANY_USER_APPS  cua
//                where rownum <= 1
//                  and ac.app_id = :a 
//                  and ac.client_id = :b 
//
//                  and dbs.database_sid = :c 
//                  and dbs.client_id = ac.client_id 
//  
//                  and cu.name =  :d 
//                  and cu.password = :e
//                  and cu.client_id =  dbs.client_id
//
//                  and cua.app_id = ac.app_id
//                  and cua.db_company_user_id = (
//                    Select t.db_company_user_id 
//                    from CLIENT_DB_COMPANY_USERS t 
//                    where t.user_id = cu.user_id 
//                      and t.db_company_id = dbs.db_company_id)";

//            using (var Conn = new OracleConnection(ConnectionString))
//            using (var Command = new OracleCommand(sql, Conn))
//            {
//                Command.Parameters.Add(":a", ResolveType(AppId)).Value = AppId;
//                Command.Parameters.Add(":b", ResolveType(ClientId)).Value = ClientId;
//                Command.Parameters.Add(":c", ResolveType(Database)).Value = Database;
//                Command.Parameters.Add(":d", ResolveType(User)).Value = User;
//                Command.Parameters.Add(":e", ResolveType(Password)).Value = Password;


//                Conn.Open();
//                using (var r = Command.ExecuteReader())
//                    if (r.Read())
//                        return Convert.ToBoolean(r["c"]);
//                    else
//                        return false;
//            }

//        }

//        private static OracleDbType ResolveType(object o)
//        {
//            if (o is string)
//                return OracleDbType.Varchar2;
//            if (o is DateTime)
//                return OracleDbType.Date;
//            if (o is Int64)
//                return OracleDbType.Int64;
//            if (o is Int32)
//                return OracleDbType.Int32;
//            if (o is Int16)
//                return OracleDbType.Int16;
//            if (o is byte)
//                return OracleDbType.Byte;
//            if (o is decimal)
//                return OracleDbType.Decimal;
//            if (o is float)
//                return OracleDbType.Single;
//            if (o is double)
//                return OracleDbType.Double;
//            if (o is byte[])
//                return OracleDbType.Blob;
//            if (o is bool)
//                return OracleDbType.Int32;
//            return OracleDbType.Varchar2;
//        }

//        public enum PMHTables
//        {
//            Apps,
//            AppClient,
//            AppQueries,
//            AppQueryColumns,
//            AppQueryConditions,
//            AppTables,
//            AppTableColumns,
//            AppUserTypes,
//            Clients,
//            ClientDBCompanies,
//            ClientDBCompanyUsers,
//            ClientDBCompanyUserApps,
//            ClientUser,
//            Logs,
//            PHMSettings,
//            QuerySequenceRequests
//        }

//        public interface IPlexxisDatabaseRow
//        {
//            //for one shot changes (immediate commit)
//            bool Insert();
//            bool Update();
//            bool Delete();

//            //Makes it possible to do transactions
//            bool Insert(OracleConnection Conn);
//            bool Update(OracleConnection Conn);
//            bool Delete(OracleConnection Conn);
//        }

//        public abstract class PlexxisDataTransferObjects : IPlexxisDatabaseRow
//        {
//            //Constructors
//            public PlexxisDataTransferObjects() { }

//            //Methods
//            public abstract bool Insert(OracleConnection Conn);
//            public abstract bool Update(OracleConnection Conn);
//            public abstract bool Delete(OracleConnection Conn);

//            public bool Insert()
//            {
//                using (var Conn = new OracleConnection(ConnectionString))
//                {
//                    Conn.Open();
//                    return Insert(Conn);
//                }
//            }
//            public bool Update()
//            {
//                using (var Conn = new OracleConnection(ConnectionString))
//                {
//                    Conn.Open();
//                    return Update(Conn);
//                }
//            }
//            public bool Delete()
//            {
//                using (var Conn = new OracleConnection(ConnectionString))
//                {
//                    Conn.Open();
//                    return Delete(Conn);
//                }
//            }
//        }

//        public sealed class Apps : PlexxisDataTransferObjects
//        {
//            public static event Subscriber OnInsert;
//            public static event Subscriber OnUpdate;
//            public static event Subscriber OnDelete;

//            //Static Methods
//            public static List<Apps> GetAll()
//            {
//                using (var Conn = new OracleConnection(ConnectionString))
//                {
//                    Conn.Open();
//                    return GetAll(Conn);
//                }
//            }

//            public static List<Apps> GetAll(OracleConnection Conn)
//            {
//                List<Apps> collection = new List<Apps>();
//                using (var Command = new OracleCommand("select * from APPS", Conn))
//                using (var reader = Command.ExecuteReader())
//                    while (reader.Read())
//                        collection.Add(new Apps(reader));
//                return collection;
//            }

//            //Fields
//            public int AppId;
//            public string AuthKey;
//            public string Title;
//            public string Description;
//            public bool isClientCustomApp;

//            //Constructors
//            public Apps() : base() { }
//            public Apps(OracleDataReader reader)
//                : base()
//            {
//                if (reader["APP_ID"] != DBNull.Value)
//                    this.AppId = Convert.ToInt32(reader["APP_ID"]);

//                if (reader["AUTH_KEY"] != DBNull.Value)
//                    this.AuthKey = Convert.ToString(reader["AUTH_KEY"]);

//                if (reader["TITLE"] != DBNull.Value)
//                    this.Title = Convert.ToString(reader["TITLE"]);

//                if (reader["DESCRIPTION"] != DBNull.Value)
//                    this.Description = Convert.ToString(reader["DESCRIPTION"]);

//                if (reader["IS_CLIENT_CUSTOM_APP"] != DBNull.Value)
//                    this.isClientCustomApp = Convert.ToBoolean(reader["IS_CLIENT_CUSTOM_APP"]);
//            }

//            //Overriden Methods from PlexxisDataTransferObjects
//            public override bool Insert(OracleConnection Conn)
//            {

//                string sql = string.Empty;
//                sql += "INSERT INTO APPS (APP_ID, AUTH_KEY, TITLE, DESCRIPTION, IS_CLIENT_CUSTOM_APP)";
//                sql += "VALUES(:appid, :authkey, :title, :description, :iscust)";

//                using (var Command = new OracleCommand(sql, Conn))
//                {
//                    AppId = GetId();
//                    Command.Parameters.Add(":appid", OracleDbType.Int32).Value = AppId;
//                    Command.Parameters.Add(":authkey", OracleDbType.Varchar2).Value = AuthKey;
//                    Command.Parameters.Add(":title", OracleDbType.Varchar2).Value = Title;
//                    Command.Parameters.Add(":description", OracleDbType.Varchar2).Value = Description;
//                    Command.Parameters.Add(":iscust", OracleDbType.Int32).Value = Convert.ToInt32(isClientCustomApp);

//                    var r = Convert.ToBoolean(Command.ExecuteNonQuery());
//                    if (OnInsert != null) OnInsert(this, EventArgs.Empty);
//                    return r;
//                }
//            }
//            public override bool Update(OracleConnection Conn)
//            {
//                string sql = string.Empty;
//                sql += "UPDATE APPS SET ";
//                sql += "AUTH_KEY = :authkey, TITLE = :title, DESCRIPTION = :description, IS_CLIENT_CUSTOM_APP = :iscust ";
//                sql += "WHERE APP_ID = :appid";

//                using (var Command = new OracleCommand(sql, Conn))
//                {
//                    Command.Parameters.Add(":authkey", OracleDbType.Varchar2).Value = AuthKey;
//                    Command.Parameters.Add(":title", OracleDbType.Varchar2).Value = Title;
//                    Command.Parameters.Add(":description", OracleDbType.Varchar2).Value = Description;
//                    Command.Parameters.Add(":iscust", OracleDbType.Int32).Value = Convert.ToInt32(isClientCustomApp);
//                    Command.Parameters.Add(":appid", OracleDbType.Int32).Value = AppId;

//                    var r = Convert.ToBoolean(Command.ExecuteNonQuery());
//                    if (OnUpdate != null) OnUpdate(this, EventArgs.Empty);
//                    return r;
//                }
//            }
//            public override bool Delete(OracleConnection Conn)
//            {
//                string sql = string.Empty;
//                sql += "DELETE FROM APPS ";
//                sql += "WHERE APP_ID = :appid";

//                using (var Command = new OracleCommand(sql, Conn))
//                {
//                    Command.Parameters.Add(":appid", OracleDbType.Int32).Value = AppId;

//                    var r = Convert.ToBoolean(Command.ExecuteNonQuery());
//                    if (OnDelete != null) OnDelete(this, EventArgs.Empty);
//                    return r;
//                }
//            }

//            //Overriden Methods from Object
//            public override bool Equals(object obj)
//            {
//                return base.Equals(obj);
//            }
//            public override int GetHashCode()
//            {
//                return base.GetHashCode();
//            }
//            public override string ToString()
//            {
//                return base.ToString();
//            }

//            public static bool operator ==(Apps a, Apps b)
//            {
//                if (a.AppId != b.AppId)
//                    return false;
//                if (a.AuthKey != b.AuthKey)
//                    return false;
//                if (a.Title != b.Title)
//                    return false;
//                if (a.Description != b.Description)
//                    return false;
//                if (a.isClientCustomApp != b.isClientCustomApp)
//                    return false;
//                return true;
//            }
//            public static bool operator !=(Apps a, Apps b)
//            {
//                if ((a == b))
//                    return false;
//                else
//                    return true;
//            }

//        }

//        public sealed class ClientApps : PlexxisDataTransferObjects
//        {
//            public static event Subscriber OnInsert;
//            public static event Subscriber OnUpdate;
//            public static event Subscriber OnDelete;

//            //Static methods
//            public static List<ClientApps> GetAll()
//            {
//                using (var Conn = new OracleConnection(ConnectionString))
//                {
//                    Conn.Open();
//                    return GetAll(Conn);
//                }
//            }

//            public static List<ClientApps> GetAll(OracleConnection Conn)
//            {
//                List<ClientApps> collection = new List<ClientApps>();
//                using (var Command = new OracleCommand("select * from CLIENT_APPS", Conn))
//                using (var reader = Command.ExecuteReader())
//                    while (reader.Read())
//                        collection.Add(new ClientApps(reader));
//                return collection;
//            }


//            //Fields
//            public int ClientAppId;
//            public int AppId;
//            public int ClientId;

//            //Constructors
//            public ClientApps() : base() { }
//            public ClientApps(OracleDataReader reader)
//            {
//                if (reader["CLIENT_APP_ID"] != DBNull.Value)
//                    this.ClientAppId = Convert.ToInt32(reader["CLIENT_APP_ID"]);

//                if (reader["APP_ID"] != DBNull.Value)
//                    this.AppId = Convert.ToInt32(reader["APP_ID"]);

//                if (reader["CLIENT_ID"] != DBNull.Value)
//                    this.ClientId = Convert.ToInt32(reader["CLIENT_ID"]);
//            }

//            //Methods
//            public override bool Insert(OracleConnection Conn)
//            {
//                string sql = string.Empty;
//                sql += "INSERT INTO APP_CLIENTS (CLIENT_APP_ID,APP_ID,CLIENT_ID)";
//                sql += "VALUES(:cai, :appid, :cid)";

//                using (var Command = new OracleCommand(sql, Conn))
//                {
//                    ClientAppId = GetId();
//                    Command.Parameters.Add(":cai", OracleDbType.Int32).Value = ClientAppId;
//                    Command.Parameters.Add(":appid", OracleDbType.Int32).Value = AppId;
//                    Command.Parameters.Add(":cid", OracleDbType.Int32).Value = ClientId;

//                    var r = Convert.ToBoolean(Command.ExecuteNonQuery());
//                    if (OnInsert != null) OnInsert(this, EventArgs.Empty);
//                    return r;
//                }
//            }
//            public override bool Delete(OracleConnection Conn)
//            {
//                string sql = string.Empty;
//                sql += "DELETE FROM APP_CLIENTS WHERE CLIENT_APP_ID = :cai";

//                using (var Command = new OracleCommand(sql, Conn))
//                {
//                    Command.Parameters.Add(":cai", OracleDbType.Int32).Value = ClientAppId;

//                    var r = Convert.ToBoolean(Command.ExecuteNonQuery());
//                    if (OnDelete != null) OnDelete(this, EventArgs.Empty);
//                    return r;
//                }
//            }
//            public override bool Update(OracleConnection Conn)
//            {
//                string sql = string.Empty;
//                sql += "UPDATE APP_CLIENTS set";
//                sql += " app_id = :b, client_id = :c ";
//                sql += "where client_app_id = :a";

//                using (var Command = new OracleCommand(sql, Conn))
//                {
//                    Command.Parameters.Add(":b", OracleDbType.Int32).Value = AppId;
//                    Command.Parameters.Add(":c", OracleDbType.Int32).Value = ClientId;
//                    Command.Parameters.Add(":a", OracleDbType.Int32).Value = ClientAppId;

//                    var r = Convert.ToBoolean(Command.ExecuteNonQuery());
//                    if (OnUpdate != null) OnUpdate(this, EventArgs.Empty);
//                    return r;
//                }
//            }


//            //Overriden Methods from Object
//            public override bool Equals(object obj)
//            {
//                return base.Equals(obj);
//            }
//            public override int GetHashCode()
//            {
//                return base.GetHashCode();
//            }
//            public override string ToString()
//            {
//                return base.ToString();
//            }

//            public static bool operator ==(ClientApps a, ClientApps b)
//            {

//                if (a.ClientAppId != b.ClientAppId)
//                    return false;
//                if (a.AppId != b.AppId)
//                    return false;
//                if (a.ClientId != b.ClientId)
//                    return false;
//                return true;
//            }
//            public static bool operator !=(ClientApps a, ClientApps b)
//            {
//                if ((a == b))
//                    return false;
//                else
//                    return true;
//            }
//        }

//        public sealed class AppQueries : PlexxisDataTransferObjects
//        {

//            public static event Subscriber OnInsert;
//            public static event Subscriber OnUpdate;
//            public static event Subscriber OnDelete;

//            //Static methods
//            public static List<AppQueries> GetAll()
//            {
//                using (var Conn = new OracleConnection(ConnectionString))
//                {
//                    Conn.Open();
//                    return GetAll(Conn);
//                }
//            }
//            public static List<AppQueries> GetAll(OracleConnection Conn)
//            {
//                List<AppQueries> collection = new List<AppQueries>();
//                using (var Command = new OracleCommand("select * from APP_QUERIES", Conn))
//                using (var reader = Command.ExecuteReader())
//                    while (reader.Read())
//                        collection.Add(new AppQueries(reader));
//                return collection;
//            }

//            //Fields
//            public int QueryId;
//            public int AppId;
//            public string Name;
//            public string Description;
//            public int TableId;
//            public bool IsDelta;
//            public string Sql;
//            public int SeqLimitTimeSpan;
//            public int SeqLimit;

//            //Constructors
//            public AppQueries() { }
//            public AppQueries(OracleDataReader reader)
//            {
//                if (reader["QUERY_ID"] != DBNull.Value)
//                    this.QueryId = Convert.ToInt32(reader["QUERY_ID"]);

//                if (reader["APP_ID"] != DBNull.Value)
//                    this.AppId = Convert.ToInt32(reader["APP_ID"]);

//                if (reader["NAME"] != DBNull.Value)
//                    this.Name = Convert.ToString(reader["NAME"]);

//                if (reader["DESCRIPTION"] != DBNull.Value)
//                    this.Description = Convert.ToString(reader["DESCRIPTION"]);

//                if (reader["TABLE_ID"] != DBNull.Value)
//                    this.TableId = Convert.ToInt32(reader["TABLE_ID"]);

//                if (reader["IS_DELTA"] != DBNull.Value)
//                    this.IsDelta = Convert.ToBoolean(reader["IS_DELTA"]);

//                if (reader["SQL"] != DBNull.Value)
//                    this.Sql = Convert.ToString(reader["SQL"]);

//                if (reader["SEQ_LIMIT_TIMESPAN"] != DBNull.Value)
//                    this.SeqLimitTimeSpan = Convert.ToInt32(reader["SEQ_LIMIT_TIMESPAN"]);

//                if (reader["SEQ_LIMIT"] != DBNull.Value)
//                    this.SeqLimit = Convert.ToInt32(reader["SEQ_LIMIT"]);
//            }

//            //Methods
//            public override bool Insert(OracleConnection Conn)
//            {
//                string sql = string.Empty;
//                sql += "insert into app_queries ";
//                sql += "(query_id, app_id, name, description, table_id, ";
//                sql += "is_delta, sql, seq_limit_timespan,seq_limit) ";
//                sql += "values (:a, :b, :c, :d, :e, :f, :g, :h, :i) ";

//                using (var Command = new OracleCommand(sql, Conn))
//                {
//                    QueryId = GetId();
//                    Command.Parameters.Add(":a", OracleDbType.Int32).Value = QueryId;
//                    Command.Parameters.Add(":b", OracleDbType.Int32).Value = AppId;
//                    Command.Parameters.Add(":c", OracleDbType.Varchar2).Value = Name;
//                    Command.Parameters.Add(":d", OracleDbType.Varchar2).Value = Description;
//                    Command.Parameters.Add(":e", OracleDbType.Int32).Value = TableId;
//                    Command.Parameters.Add(":f", OracleDbType.Int32).Value = Convert.ToInt32(IsDelta);
//                    Command.Parameters.Add(":g", OracleDbType.Varchar2).Value = Sql;
//                    Command.Parameters.Add(":h", OracleDbType.Int32).Value = SeqLimitTimeSpan;
//                    Command.Parameters.Add(":i", OracleDbType.Int32).Value = SeqLimit;

//                    var r = Convert.ToBoolean(Command.ExecuteNonQuery());
//                    if (OnInsert != null) OnInsert(this, EventArgs.Empty);
//                    return r;
//                }
//            }
//            public override bool Delete(OracleConnection Conn)
//            {
//                string sql = string.Empty;
//                sql += "DELETE FROM APP_QUERIES WHERE QUERY_ID = :qid";
//                using (var Command = new OracleCommand(sql, Conn))
//                {
//                    Command.Parameters.Add(":qid", OracleDbType.Int32).Value = QueryId;

//                    var r = Convert.ToBoolean(Command.ExecuteNonQuery());
//                    if (OnDelete != null) OnDelete(this, EventArgs.Empty);
//                    return r;
//                }
//            }
//            public override bool Update(OracleConnection Conn)
//            {
//                string sql = string.Empty;
//                sql += "UPDATE APP_QUERIES SET ";
//                sql += "APP_ID = :b ,NAME = :c,DESCRIPTION = :d ,TABLE_ID = :e,";
//                sql += "IS_DELTA = :f,SQL = :g ,SEQ_LIMIT_TIMESPAN = :h,SEQ_LIMIT = :i ";
//                sql += "WHERE QUERY_ID = :a";

//                using (var Command = new OracleCommand(sql, Conn))
//                {
//                    Command.Parameters.Add(":b", OracleDbType.Int32).Value = AppId;
//                    Command.Parameters.Add(":c", OracleDbType.Varchar2).Value = Name;
//                    Command.Parameters.Add(":d", OracleDbType.Varchar2).Value = Description;
//                    Command.Parameters.Add(":e", OracleDbType.Int32).Value = TableId;
//                    Command.Parameters.Add(":f", OracleDbType.Int32).Value = Convert.ToInt32(IsDelta);
//                    Command.Parameters.Add(":g", OracleDbType.Varchar2).Value = Sql;
//                    Command.Parameters.Add(":h", OracleDbType.Int32).Value = SeqLimitTimeSpan;
//                    Command.Parameters.Add(":i", OracleDbType.Int32).Value = SeqLimit;
//                    Command.Parameters.Add(":a", OracleDbType.Int32).Value = QueryId;

//                    var r = Convert.ToBoolean(Command.ExecuteNonQuery());
//                    if (OnUpdate != null) OnUpdate(this, EventArgs.Empty);
//                    return r;
//                }
//            }
//            //Overriden Methods from Object
//            public override bool Equals(object obj)
//            {
//                return base.Equals(obj);
//            }
//            public override int GetHashCode()
//            {
//                return base.GetHashCode();
//            }
//            public override string ToString()
//            {
//                return base.ToString();
//            }

//            public static bool operator ==(AppQueries a, AppQueries b)
//            {
//                if (a.QueryId != b.QueryId)
//                    return false;
//                if (a.AppId != b.AppId)
//                    return false;
//                if (a.Name != b.Name)
//                    return false;
//                if (a.QueryId != b.QueryId)
//                    return false;
//                if (a.Description != b.Description)
//                    return false;
//                if (a.TableId != b.TableId)
//                    return false;
//                if (a.IsDelta != b.IsDelta)
//                    return false;
//                if (a.Sql != b.Sql)
//                    return false;
//                if (a.SeqLimitTimeSpan != b.SeqLimitTimeSpan)
//                    return false;
//                if (a.SeqLimit != b.SeqLimit)
//                    return false;
//                return true;
//            }
//            public static bool operator !=(AppQueries a, AppQueries b)
//            {
//                if ((a == b))
//                    return false;
//                else
//                    return true;
//            }
//        }

//        public sealed class AppQueryColumns : PlexxisDataTransferObjects
//        {

//            public static event Subscriber OnInsert;
//            public static event Subscriber OnUpdate;
//            public static event Subscriber OnDelete;

//            //Static methods
//            public static List<AppQueryColumns> GetAll()
//            {
//                using (var Conn = new OracleConnection(ConnectionString))
//                {
//                    Conn.Open();
//                    return GetAll(Conn);
//                }
//            }

//            public static List<AppQueryColumns> GetAll(OracleConnection Conn)
//            {
//                List<AppQueryColumns> collection = new List<AppQueryColumns>();
//                using (var Command = new OracleCommand("select * from APP_QUERY_COLUMNS", Conn))
//                using (var reader = Command.ExecuteReader())
//                    while (reader.Read())
//                        collection.Add(new AppQueryColumns(reader));
//                return collection;
//            }

//            //Fields
//            public int ColumnId;
//            public int QueryId;
//            public string ColumnName;
//            public int ColumnSequence;


//            //Constructors
//            public AppQueryColumns() { }
//            public AppQueryColumns(OracleDataReader reader)
//            {
//                if (reader["Column_Id"] != DBNull.Value)
//                    this.ColumnId = Convert.ToInt32(reader["Column_Id"]);

//                if (reader["QUERY_ID"] != DBNull.Value)
//                    this.QueryId = Convert.ToInt32(reader["QUERY_ID"]);

//                if (reader["COLUMN_NAME"] != DBNull.Value)
//                    this.ColumnName = Convert.ToString(reader["COLUMN_NAME"]);

//                if (reader["COLUMN_SEQUENCE"] != DBNull.Value)
//                    this.ColumnSequence = Convert.ToInt32(reader["COLUMN_SEQUENCE"]);

//            }

//            //Methods
//            public override bool Insert(OracleConnection Conn)
//            {
//                string sql = string.Empty;
//                sql += "INSERT INTO APP_QUERY_COLUMNS(Column_Id, QUERY_ID, COLUMN_NAME,";
//                sql += "COLUMN_SEQUENCE) VALUES (:a, :b, :c, :d)";

//                using (var Command = new OracleCommand(sql, Conn))
//                {
//                    ColumnId = GetId();
//                    Command.Parameters.Add(":a", OracleDbType.Int32).Value = ColumnId;
//                    Command.Parameters.Add(":b", OracleDbType.Int32).Value = QueryId;
//                    Command.Parameters.Add(":c", OracleDbType.Varchar2).Value = ColumnName;
//                    Command.Parameters.Add(":d", OracleDbType.Int32).Value = ColumnSequence;

//                    var r = Convert.ToBoolean(Command.ExecuteNonQuery());
//                    if (OnInsert != null) OnInsert(this, EventArgs.Empty);
//                    return r;
//                }

//            }
//            public override bool Update(OracleConnection Conn)
//            {
//                string sql = string.Empty;
//                sql += "UPDATE APP_QUERY_COLUMNS SET ";
//                sql += "QUERY_ID = :qid, COLUMN_NAME = :cn, ";
//                sql += "COLUMN_SEQUENCE = :csq WHERE Column_Id = :qcd";

//                using (var Command = new OracleCommand(sql, Conn))
//                {
//                    Command.Parameters.Add(":qid", OracleDbType.Int32).Value = QueryId;
//                    Command.Parameters.Add(":cn", OracleDbType.Varchar2).Value = ColumnName;
//                    Command.Parameters.Add(":csq", OracleDbType.Int32).Value = ColumnSequence;
//                    Command.Parameters.Add(":qcd", OracleDbType.Int32).Value = ColumnId;

//                    var r = Convert.ToBoolean(Command.ExecuteNonQuery());
//                    if (OnUpdate != null) OnUpdate(this, EventArgs.Empty);
//                    return r;
//                }
//            }
//            public override bool Delete(OracleConnection Conn)
//            {
//                string sql = string.Empty;
//                sql += "DELETE FROM APP_QUERY_COLUMNS WHERE Column_Id = :qid";

//                using (var Command = new OracleCommand(sql, Conn))
//                {
//                    Command.Parameters.Add(":qid", OracleDbType.Int32).Value = ColumnId;

//                    var r = Convert.ToBoolean(Command.ExecuteNonQuery());
//                    if (OnDelete != null) OnDelete(this, EventArgs.Empty);
//                    return r;
//                }
//            }

//            //Overriden Methods from Object
//            public override bool Equals(object obj)
//            {
//                return base.Equals(obj);
//            }
//            public override int GetHashCode()
//            {
//                return base.GetHashCode();
//            }
//            public override string ToString()
//            {
//                return base.ToString();
//            }

//            public static bool operator ==(AppQueryColumns a, AppQueryColumns b)
//            {
//                if (a.ColumnId != b.ColumnId)
//                    return false;
//                if (a.QueryId != b.QueryId)
//                    return false;
//                if (a.ColumnName != b.ColumnName)
//                    return false;
//                if (a.ColumnSequence != b.ColumnSequence)
//                    return false;
//                return true;
//            }

//            public static bool operator !=(AppQueryColumns a, AppQueryColumns b)
//            {
//                if ((a == b))
//                    return false;
//                else
//                    return true;
//            }
//        }

//        public sealed class AppQueryConditions : PlexxisDataTransferObjects
//        {
//            public static event Subscriber OnInsert;
//            public static event Subscriber OnUpdate;
//            public static event Subscriber OnDelete;

//            public static List<AppQueryConditions> GetAll()
//            {
//                using (var Conn = new OracleConnection(ConnectionString))
//                {
//                    Conn.Open();
//                    return GetAll(Conn);
//                }
//            }
//            public static List<AppQueryConditions> GetAll(OracleConnection Conn)
//            {
//                List<AppQueryConditions> collection = new List<AppQueryConditions>();
//                using (var Command = new OracleCommand("select * from APP_QUERY_CONDITIONS", Conn))
//                using (var reader = Command.ExecuteReader())
//                    while (reader.Read())
//                        collection.Add(new AppQueryConditions(reader));
//                return collection;

//            }

//            public int ConditionId;
//            public int QueryId;
//            public string ColumnName;
//            public string ColumnNVL;
//            public string ColumnValue;
//            public string Operator;


//            public AppQueryConditions() { }
//            public AppQueryConditions(OracleDataReader reader)
//            {
//                if (reader["Condition_Id"] != DBNull.Value)
//                    this.ConditionId = Convert.ToInt32(reader["Condition_Id"]);

//                if (reader["QUERY_ID"] != DBNull.Value)
//                    this.QueryId = Convert.ToInt32(reader["QUERY_ID"]);

//                if (reader["Column_Name"] != DBNull.Value)
//                    this.ColumnName = Convert.ToString(reader["Column_Name"]);

//                if (reader["Column_NVL"] != DBNull.Value)
//                    this.ColumnNVL = Convert.ToString(reader["Column_NVL"]);

//                if (reader["Column_Value"] != DBNull.Value)
//                    this.ColumnValue = Convert.ToString(reader["Column_Value"]);

//                if (reader["Operator"] != DBNull.Value)
//                    this.Operator = Convert.ToString(reader["Operator"]);

//            }

//            public override bool Insert(OracleConnection Conn)
//            {
//                string sql = string.Empty;
//                sql += "INSERT INTO APP_QUERY_CONDITIONS(Condition_Id,QUERY_ID, Column_Name,";
//                sql += "Column_NVL, Column_Value, Operator) ";
//                sql += "VALUES (:a, :b, :c, :e, :f, :g)";

//                using (var Command = new OracleCommand(sql, Conn))
//                {
//                    ConditionId = GetId();
//                    Command.Parameters.Add(":a", ResolveType(ConditionId)).Value = ConditionId;
//                    Command.Parameters.Add(":b", ResolveType(QueryId)).Value = QueryId;
//                    Command.Parameters.Add(":c", ResolveType(ColumnName)).Value = ColumnName;

//                    Command.Parameters.Add(":d", ResolveType(ColumnNVL)).Value = ColumnNVL;
//                    Command.Parameters.Add(":e", ResolveType(ColumnValue)).Value = ColumnValue;
//                    Command.Parameters.Add(":f", ResolveType(Operator)).Value = Operator;

//                    var r = Convert.ToBoolean(Command.ExecuteNonQuery());
//                    if (OnInsert != null) OnInsert(this, EventArgs.Empty);
//                    return r;
//                }

//            }
//            public override bool Delete(OracleConnection Conn)
//            {
//                string sql = string.Empty;
//                sql += "delete from app_Query_conditions where condition_id = :a";

//                using (var Command = new OracleCommand(sql, Conn))
//                {
//                    Command.Parameters.Add(":a", OracleDbType.Int32).Value = ConditionId;

//                    var r = Convert.ToBoolean(Command.ExecuteNonQuery());
//                    if (OnDelete != null) OnDelete(this, EventArgs.Empty);
//                    return r;
//                }
//            }
//            public override bool Update(OracleConnection Conn)
//            {
//                string sql = "update APP_QUERY_CONDITIONS set query_id = :b, column_name =:c, column_nvl = :d, column_value=:e, operator=:f where Condition_id = :a";

//                using (var Command = new OracleCommand(sql, Conn))
//                {
//                    Command.Parameters.Add(":b", ResolveType(QueryId)).Value = QueryId;
//                    Command.Parameters.Add(":c", ResolveType(ColumnName)).Value = ColumnName;

//                    Command.Parameters.Add(":d", ResolveType(ColumnNVL)).Value = ColumnNVL;
//                    Command.Parameters.Add(":e", ResolveType(ColumnValue)).Value = ColumnValue;
//                    Command.Parameters.Add(":f", ResolveType(Operator)).Value = Operator;

//                    Command.Parameters.Add(":a", ResolveType(ConditionId)).Value = ConditionId;

//                    var r = Convert.ToBoolean(Command.ExecuteNonQuery());
//                    if (OnUpdate != null) OnUpdate(this, EventArgs.Empty);
//                    return r;
//                }
//            }
//            //Overriden Methods from Object
//            public override bool Equals(object obj)
//            {
//                return base.Equals(obj);
//            }
//            public override int GetHashCode()
//            {
//                return base.GetHashCode();
//            }
//            public override string ToString()
//            {
//                return base.ToString();
//            }

//            public static bool operator ==(AppQueryConditions a, AppQueryConditions b)
//            {
//                if (a.ConditionId != b.ConditionId)
//                    return false;
//                if (a.QueryId != b.QueryId)
//                    return false;
//                if (a.QueryId != b.QueryId)
//                    return false;
//                if (a.ColumnName != b.ColumnName)
//                    return false;
//                if (a.ColumnNVL != b.ColumnNVL)
//                    return false;
//                if (a.ColumnValue != b.ColumnValue)
//                    return false;
//                if (a.Operator != b.Operator)
//                    return false;
//                return true;
//            }
//            public static bool operator !=(AppQueryConditions a, AppQueryConditions b)
//            {
//                if ((a == b))
//                    return false;
//                else
//                    return true;
//            }
//        }

//        public sealed class AppTables : PlexxisDataTransferObjects
//        {
//            public static event Subscriber OnInsert;
//            public static event Subscriber OnUpdate;
//            public static event Subscriber OnDelete;

//            public static List<AppTables> GetAll()
//            {
//                List<AppTables> collection = new List<AppTables>();
//                using (var Conn = new OracleConnection(ConnectionString))
//                {
//                    Conn.Open();
//                    return GetAll(Conn);
//                }
//            }

//            public static List<AppTables> GetAll(OracleConnection Conn)
//            {
//                List<AppTables> collection = new List<AppTables>();
//                using (var Command = new OracleCommand("select * from APP_TABLES", Conn))
//                using (var reader = Command.ExecuteReader())
//                    while (reader.Read())
//                        collection.Add(new AppTables(reader));
//                return collection;
//            }


//            public int TableId;
//            public int AppId;
//            public string Name;
//            public string Description;
//            public bool InsertAllowed;
//            public bool UpdateAllowed;
//            public bool DeleteAllowed;
//            public bool QueryAllowed;

//            public AppTables() { }
//            public AppTables(OracleDataReader reader)
//            {
//                if (reader["TABLE_ID"] != DBNull.Value)
//                    this.TableId = Convert.ToInt32(reader["TABLE_ID"]);

//                if (reader["APP_ID"] != DBNull.Value)
//                    this.AppId = Convert.ToInt32(reader["APP_ID"]);

//                if (reader["NAME"] != DBNull.Value)
//                    this.Name = Convert.ToString(reader["NAME"]);

//                if (reader["DESCRIPTION"] != DBNull.Value)
//                    this.Description = Convert.ToString(reader["DESCRIPTION"]);

//                if (reader["INSERT_ALLOWED"] != DBNull.Value)
//                    this.InsertAllowed = Convert.ToBoolean(reader["INSERT_ALLOWED"]);

//                if (reader["UPDATe_ALLOWED"] != DBNull.Value)
//                    this.UpdateAllowed = Convert.ToBoolean(reader["UPDATe_ALLOWED"]);

//                if (reader["DELETE_ALLOWED"] != DBNull.Value)
//                    this.DeleteAllowed = Convert.ToBoolean(reader["DELETE_ALLOWED"]);

//                if (reader["QUERY_ALLOWED"] != DBNull.Value)
//                    this.QueryAllowed = Convert.ToBoolean(reader["QUERY_ALLOWED"]);

//            }

//            public override bool Insert(OracleConnection Conn)
//            {
//                string sql = string.Empty;
//                sql += "INSERT INTO APP_TABLES(TABLE_ID, APP_ID, NAME, DESCRIPTION, INSERT_ALLOWED, ";
//                sql += "UPDATE_ALLOWED, DELETE_ALLOWED, QUERY_ALLOWED) VALUES ";
//                sql += "(:a, :b, :c, :d, :e, :f, :g, :h)";

//                using (var Command = new OracleCommand(sql, Conn))
//                {
//                    TableId = GetId();
//                    Command.Parameters.Add(":a", OracleDbType.Int32).Value = TableId;
//                    Command.Parameters.Add(":b", OracleDbType.Int32).Value = AppId;
//                    Command.Parameters.Add(":c", OracleDbType.Varchar2).Value = Name;
//                    Command.Parameters.Add(":d", OracleDbType.Varchar2).Value = Description;
//                    Command.Parameters.Add(":e", OracleDbType.Int32).Value = Convert.ToInt32(InsertAllowed);
//                    Command.Parameters.Add(":f", OracleDbType.Int32).Value = Convert.ToInt32(UpdateAllowed);
//                    Command.Parameters.Add(":g", OracleDbType.Int32).Value = Convert.ToInt32(DeleteAllowed);
//                    Command.Parameters.Add(":h", OracleDbType.Int32).Value = Convert.ToInt32(QueryAllowed);

//                    var r = Convert.ToBoolean(Command.ExecuteNonQuery());
//                    if (OnInsert != null) OnInsert(this, EventArgs.Empty);
//                    return r;
//                }


//            }
//            public override bool Delete(OracleConnection Conn)
//            {
//                string sql = string.Empty;
//                sql += "DELETE FROM APP_TABLES WHERE TABLE_ID = :tid";

//                using (var Command = new OracleCommand(sql, Conn))
//                {
//                    Command.Parameters.Add("tid", OracleDbType.Int32).Value = TableId;

//                    var r = Convert.ToBoolean(Command.ExecuteNonQuery());
//                    if (OnDelete != null) OnDelete(this, EventArgs.Empty);
//                    return r;
//                }
//            }
//            public override bool Update(OracleConnection Conn)
//            {
//                string sql = string.Empty;
//                sql += "UPDATE APP_TABLES SET ";
//                sql += "APP_ID = :b, NAME = :c, DESCRIPTION = :d, INSERT_ALLOWED = :e, ";
//                sql += "UPDATE_ALLOWED = :f, DELETE_ALLOWED = :g, QUERY_ALLOWED = :h ";
//                sql += "WHERE TABLE_ID = :a";

//                using (var Command = new OracleCommand(sql, Conn))
//                {
//                    AppId = GetId();
//                    Command.Parameters.Add(":b", OracleDbType.Int32).Value = AppId;
//                    Command.Parameters.Add(":c", OracleDbType.Varchar2).Value = Name;
//                    Command.Parameters.Add(":d", OracleDbType.Varchar2).Value = Description;
//                    Command.Parameters.Add(":e", OracleDbType.Int32).Value = Convert.ToInt32(InsertAllowed);
//                    Command.Parameters.Add(":f", OracleDbType.Int32).Value = Convert.ToInt32(UpdateAllowed);
//                    Command.Parameters.Add(":g", OracleDbType.Int32).Value = Convert.ToInt32(DeleteAllowed);
//                    Command.Parameters.Add(":h", OracleDbType.Int32).Value = Convert.ToInt32(QueryAllowed);
//                    Command.Parameters.Add(":a", OracleDbType.Int32).Value = TableId;

//                    var r = Convert.ToBoolean(Command.ExecuteNonQuery());
//                    if (OnUpdate != null) OnUpdate(this, EventArgs.Empty);
//                    return r;
//                }
//            }
//            //Overriden Methods from Object
//            public override bool Equals(object obj)
//            {
//                return base.Equals(obj);
//            }
//            public override int GetHashCode()
//            {
//                return base.GetHashCode();
//            }
//            public override string ToString()
//            {
//                return base.ToString();
//            }

//            public static bool operator ==(AppTables a, AppTables b)
//            {
//                if (a.AppId != b.AppId)
//                    return false;
//                if (a.Name != b.Name)
//                    return false;
//                if (a.Description != b.Description)
//                    return false;
//                if (a.InsertAllowed != b.InsertAllowed)
//                    return false;
//                if (a.UpdateAllowed != b.UpdateAllowed)
//                    return false;
//                if (a.DeleteAllowed != b.DeleteAllowed)
//                    return false;
//                if (a.QueryAllowed != b.QueryAllowed)
//                    return false;
//                return true;
//            }

//            public static bool operator !=(AppTables a, AppTables b)
//            {
//                if ((a == b))
//                    return false;
//                else
//                    return true;
//            }
//        }

//        public sealed class AppTableColumns : PlexxisDataTransferObjects
//        {
//            public static event Subscriber OnInsert;
//            public static event Subscriber OnUpdate;
//            public static event Subscriber OnDelete;

//            //Static methods
//            public static List<AppTableColumns> GetAll()
//            {
//                using (var Conn = new OracleConnection(ConnectionString))
//                {
//                    Conn.Open();
//                    return GetAll(Conn);
//                }
//            }

//            public static List<AppTableColumns> GetAll(OracleConnection Conn)
//            {
//                List<AppTableColumns> collection = new List<AppTableColumns>();
//                using (var Command = new OracleCommand("select * from APP_TABLE_COLUMNS", Conn))

//                using (var reader = Command.ExecuteReader())
//                    while (reader.Read())
//                        collection.Add(new AppTableColumns(reader));
//                return collection;
//            }

//            //Fields
//            public int TableId;
//            public int TableColumnId;
//            public string ColumnName;
//            public int ColumnSequence;
//            public string DataType;
//            public int DataLength;
//            public int DataPrecision;
//            public int DataScale;
//            public bool AllowDbNull;
//            public bool IsReadOnly;
//            public bool IsLong;
//            public bool IsKey;
//            public string KeyType;
//            public bool IsUnique;
//            public string Description;

//            //Constructors
//            public AppTableColumns() { }
//            public AppTableColumns(OracleDataReader reader)
//            {
//                if (reader["TABLE_ID"] != DBNull.Value)
//                    this.TableId = Convert.ToInt32(reader["TABLE_ID"]);

//                if (reader["TABLE_COLUMN_ID"] != DBNull.Value)
//                    this.TableColumnId = Convert.ToInt32(reader["TABLE_COLUMN_ID"]);

//                if (reader["COLUMN_NAME"] != DBNull.Value)
//                    this.ColumnName = Convert.ToString(reader["COLUMN_NAME"]);

//                if (reader["COLUMN_SEQUENCE"] != DBNull.Value)
//                    this.ColumnSequence = Convert.ToInt32(reader["COLUMN_SEQUENCE"]);

//                if (reader["DATA_TYPE"] != DBNull.Value)
//                    this.DataType = Convert.ToString(reader["DATA_TYPE"]);

//                if (reader["DATA_LENGTH"] != DBNull.Value)
//                    this.DataLength = Convert.ToInt32(reader["DATA_LENGTH"]);

//                if (reader["DATA_PRECISION"] != DBNull.Value)
//                    this.DataPrecision = Convert.ToInt32(reader["DATA_PRECISION"]);

//                if (reader["DATA_sCALE"] != DBNull.Value)
//                    this.DataScale = Convert.ToInt32(reader["DATA_sCALE"]);

//                if (reader["ALLOW_DB_NULL"] != DBNull.Value)
//                    this.AllowDbNull = Convert.ToBoolean(reader["ALLOW_DB_NULL"]);

//                if (reader["IS_LONG"] != DBNull.Value)
//                    this.IsLong = Convert.ToBoolean(reader["IS_LONG"]);

//                if (reader["IS_KEY"] != DBNull.Value)
//                    this.IsKey = Convert.ToBoolean(reader["IS_KEY"]);

//                if (reader["KEY_TYPE"] != DBNull.Value)
//                    this.KeyType = Convert.ToString(reader["KEY_TYPE"]);

//                if (reader["IS_UNIQUE"] != DBNull.Value)
//                    this.IsUnique = Convert.ToBoolean(reader["IS_UNIQUE"]);

//                if (reader["DESCRIPTION"] != DBNull.Value)
//                    this.Description = Convert.ToString(reader["DESCRIPTION"]);
//            }

//            //Methods
//            public override bool Insert(OracleConnection Conn)
//            {
//                string sql = string.Empty;
//                sql += "insert into app_table_columns ";
//                sql += "(table_id, table_column_id, column_name, column_sequence, ";
//                sql += "data_type, data_length, data_precision, data_scale, allow_db_null,";
//                sql += "is_read_only, is_long, is_key, key_type, is_unique, description)";
//                sql += "values(:a, :b, :c, :d, :e, :f, :g, :h, :i, :j, :k, :l, :m, :n, :o)";

//                using (var Command = new OracleCommand(sql, Conn))
//                {
//                    TableId = GetId();
//                    Command.Parameters.Add(":a", OracleDbType.Int32).Value = TableId;
//                    Command.Parameters.Add(":b", OracleDbType.Int32).Value = TableColumnId;
//                    Command.Parameters.Add(":c", OracleDbType.Varchar2).Value = ColumnName;
//                    Command.Parameters.Add(":d", OracleDbType.Int32).Value = ColumnSequence;
//                    Command.Parameters.Add(":e", OracleDbType.Varchar2).Value = DataType;
//                    Command.Parameters.Add(":f", OracleDbType.Int32).Value = DataLength;
//                    Command.Parameters.Add(":g", OracleDbType.Int32).Value = DataPrecision;
//                    Command.Parameters.Add(":h", OracleDbType.Int32).Value = DataScale;
//                    Command.Parameters.Add(":i", OracleDbType.Int32).Value = Convert.ToInt32(AllowDbNull);
//                    Command.Parameters.Add(":j", OracleDbType.Int32).Value = Convert.ToInt32(IsReadOnly);
//                    Command.Parameters.Add(":k", OracleDbType.Int32).Value = Convert.ToInt32(IsLong);
//                    Command.Parameters.Add(":l", OracleDbType.Int32).Value = Convert.ToInt32(IsKey);
//                    Command.Parameters.Add(":m", OracleDbType.Varchar2).Value = KeyType;
//                    Command.Parameters.Add(":n", OracleDbType.Int32).Value = Convert.ToInt32(IsUnique);
//                    Command.Parameters.Add(":o", OracleDbType.Varchar2).Value = Description;

//                    var r = Convert.ToBoolean(Command.ExecuteNonQuery());
//                    if (OnInsert != null) OnInsert(this, EventArgs.Empty);
//                    return r;
//                }

//            }
//            public override bool Delete(OracleConnection Conn)
//            {
//                string sql = string.Empty;
//                sql += "DELETE FROM APP_TABLE_COLUMNS WHERE TABLE_ID = :tid";

//                using (var Command = new OracleCommand(sql, Conn))
//                {
//                    Command.Parameters.Add(":tid", OracleDbType.Int32).Value = TableId;

//                    var r = Convert.ToBoolean(Command.ExecuteNonQuery());
//                    if (OnDelete != null) OnDelete(this, EventArgs.Empty);
//                    return r;
//                }
//            }
//            public override bool Update(OracleConnection Conn)
//            {
//                string sql = string.Empty;
//                sql += "UPDATE APP_TABLE_COLUMNS SET ";
//                sql += "TABLE_COLUMN_ID = :b, COLUMN_NAME = :c, COLUMN_SEQUENCE = :d, ";
//                sql += "DATA_TYPE = :e, DATA_LENGTH = :f, DATA_PRECISION = :g , DATA_SCALE = :h, ";
//                sql += "ALLOW_DB_NULL = :i, IS_READ_ONLY =:j, IS_LONG = :k, IS_KEY = :l, KEY_TYPE = :m, ";
//                sql += "IS_UNIQUE = :n, DESCRIPTION = :o ";
//                sql += "WHERE TABLE_ID = :a";

//                using (var Command = new OracleCommand(sql, Conn))
//                {
//                    Command.Parameters.Add(":a", OracleDbType.Int32).Value = TableColumnId;
//                    Command.Parameters.Add(":b", OracleDbType.Varchar2).Value = ColumnName;
//                    Command.Parameters.Add(":c", OracleDbType.Int32).Value = ColumnSequence;
//                    Command.Parameters.Add(":d", OracleDbType.Varchar2).Value = DataType;
//                    Command.Parameters.Add(":e", OracleDbType.Int32).Value = DataLength;
//                    Command.Parameters.Add(":f", OracleDbType.Int32).Value = DataPrecision;
//                    Command.Parameters.Add(":g", OracleDbType.Int32).Value = DataScale;
//                    Command.Parameters.Add(":h", OracleDbType.Int32).Value = Convert.ToInt32(AllowDbNull);
//                    Command.Parameters.Add(":i", OracleDbType.Int32).Value = Convert.ToInt32(IsReadOnly);
//                    Command.Parameters.Add(":j", OracleDbType.Int32).Value = Convert.ToInt32(IsLong);
//                    Command.Parameters.Add(":k", OracleDbType.Int32).Value = Convert.ToInt32(IsKey);
//                    Command.Parameters.Add(":l", OracleDbType.Varchar2).Value = KeyType;
//                    Command.Parameters.Add(":m", OracleDbType.Int32).Value = Convert.ToInt32(IsUnique);
//                    Command.Parameters.Add(":n", OracleDbType.Varchar2).Value = Description;
//                    Command.Parameters.Add(":o", OracleDbType.Int32).Value = TableId;


//                    var r = Convert.ToBoolean(Command.ExecuteNonQuery());
//                    if (OnUpdate != null) OnUpdate(this, EventArgs.Empty);
//                    return r;
//                }
//            }
//            //Overriden Methods from Object
//            public override bool Equals(object obj)
//            {
//                return base.Equals(obj);
//            }
//            public override int GetHashCode()
//            {
//                return base.GetHashCode();
//            }
//            public override string ToString()
//            {
//                return base.ToString();
//            }

//            public static bool operator ==(AppTableColumns a, AppTableColumns b)
//            {
//                if (a.TableColumnId != b.TableColumnId)
//                    return false;
//                if (a.ColumnName != b.ColumnName)
//                    return false;
//                if (a.ColumnSequence != b.ColumnSequence)
//                    return false;
//                if (a.DataType != b.DataType)
//                    return false;
//                if (a.DataLength != b.DataLength)
//                    return false;
//                if (a.DataPrecision != b.DataPrecision)
//                    return false;
//                if (a.DataScale != b.DataScale)
//                    return false;

//                if (a.AllowDbNull != b.AllowDbNull)
//                    return false;
//                if (a.IsReadOnly != b.IsReadOnly)
//                    return false;
//                if (a.IsLong != b.IsLong)
//                    return false;
//                if (a.IsKey != b.IsKey)
//                    return false;
//                if (a.KeyType != b.KeyType)
//                    return false;
//                if (a.IsUnique != b.IsUnique)
//                    return false;
//                if (a.Description != b.Description)
//                    return false;
//                if (a.TableId != b.TableId)
//                    return false;
//                return true;
//            }

//            public static bool operator !=(AppTableColumns a, AppTableColumns b)
//            {
//                if ((a == b))
//                    return false;
//                else
//                    return true;
//            }
//        }

//        public sealed class AppUserTypes : PlexxisDataTransferObjects
//        {
//            public static event Subscriber OnInsert;
//            public static event Subscriber OnUpdate;
//            public static event Subscriber OnDelete;

//            //Static methods
//            public static List<AppUserTypes> GetAll()
//            {
//                using (var Conn = new OracleConnection(ConnectionString))
//                {
//                    Conn.Open();
//                    return GetAll(Conn);
//                }
//            }
//            public static List<AppUserTypes> GetAll(OracleConnection Conn)
//            {
//                List<AppUserTypes> collection = new List<AppUserTypes>();
//                using (var Command = new OracleCommand("select * from APP_USER_TYPES", Conn))
//                using (var reader = Command.ExecuteReader())
//                    while (reader.Read())
//                        collection.Add(new AppUserTypes(reader));
//                return collection;
//            }
//            //Fields
//            public int UserTypeId;
//            public int AppId;
//            public string Code;
//            public string Description;

//            //Constructors
//            public AppUserTypes() { }
//            public AppUserTypes(OracleDataReader reader)
//            {
//                if (reader["USER_TYPE_ID"] != DBNull.Value)
//                    this.UserTypeId = Convert.ToInt32(reader["USER_TYPE_ID"]);

//                if (reader["APP_ID"] != DBNull.Value)
//                    this.AppId = Convert.ToInt32(reader["APP_ID"]);

//                if (reader["CODE"] != DBNull.Value)
//                    this.Code = Convert.ToString(reader["CODE"]);

//                if (reader["DESCRIPTION"] != DBNull.Value)
//                    this.Description = Convert.ToString(reader["DESCRIPTION"]);
//            }

//            //Methods
//            public override bool Insert(OracleConnection Conn)
//            {
//                string sql = string.Empty;
//                sql += "INSERT INTO APP_USER_TYPES (USER_TYPE_ID,APP_ID,CODE,DESCRIPTION)";
//                sql += "VALUES (:a, :b, :c, :d ) ";

//                using (var Command = new OracleCommand(sql, Conn))
//                {
//                    UserTypeId = GetId();
//                    Command.Parameters.Add(":a", OracleDbType.Int32).Value = UserTypeId;
//                    Command.Parameters.Add(":b", OracleDbType.Int32).Value = AppId;
//                    Command.Parameters.Add(":c", OracleDbType.Varchar2).Value = Code;
//                    Command.Parameters.Add(":d", OracleDbType.Varchar2).Value = Description;

//                    var r = Convert.ToBoolean(Command.ExecuteNonQuery());
//                    if (OnInsert != null) OnInsert(this, EventArgs.Empty);
//                    return r;
//                }

//            }
//            public override bool Delete(OracleConnection Conn)
//            {
//                string sql = string.Empty;
//                sql += "DELETE FROM APP_USER_TYPES WHERE USER_TYPE_ID = :utid";

//                using (var Command = new OracleCommand(sql, Conn))
//                {
//                    Command.Parameters.Add(":utid", OracleDbType.Int32).Value = UserTypeId;
//                    var r = Convert.ToBoolean(Command.ExecuteNonQuery());
//                    if (OnDelete != null) OnDelete(this, EventArgs.Empty);
//                    return r;
//                }
//            }
//            public override bool Update(OracleConnection Conn)
//            {
//                string sql = string.Empty;
//                sql += "update app_user_types set ";
//                sql += "app_id = :b, code = :c, description = :d ";
//                sql += "where user_type_id = :a ";

//                using (var Command = new OracleCommand(sql, Conn))
//                {
//                    Command.Parameters.Add(":b", OracleDbType.Int32).Value = AppId;
//                    Command.Parameters.Add(":c", OracleDbType.Varchar2).Value = Code;
//                    Command.Parameters.Add(":d", OracleDbType.Varchar2).Value = Description;
//                    Command.Parameters.Add(":a", OracleDbType.Int32).Value = UserTypeId;

//                    var r = Convert.ToBoolean(Command.ExecuteNonQuery());
//                    if (OnUpdate != null) OnUpdate(this, EventArgs.Empty);
//                    return r;
//                }
//            }

//            //Overriden Methods from Object
//            public override bool Equals(object obj)
//            {
//                return base.Equals(obj);
//            }
//            public override int GetHashCode()
//            {
//                return base.GetHashCode();
//            }
//            public override string ToString()
//            {
//                return base.ToString();
//            }

//            public static bool operator ==(AppUserTypes a, AppUserTypes b)
//            {

//                if (a.UserTypeId != b.UserTypeId)
//                    return false;
//                if (a.AppId != b.AppId)
//                    return false;
//                if (a.Code != b.Code)
//                    return false;
//                if (a.Description != b.Description)
//                    return false;
//                return true;
//            }

//            public static bool operator !=(AppUserTypes a, AppUserTypes b)
//            {
//                if ((a == b))
//                    return false;
//                else
//                    return true;
//            }
//        }

//        public sealed class Clients : PlexxisDataTransferObjects
//        {
//            public static event Subscriber OnInsert;
//            public static event Subscriber OnUpdate;
//            public static event Subscriber OnDelete;

//            //Static methods
//            public static List<Clients> GetAll()
//            {
//                using (var Conn = new OracleConnection(ConnectionString))
//                {
//                    Conn.Open();
//                    return GetAll(Conn);
//                }
//            }

//            public static List<Clients> GetAll(OracleConnection Conn)
//            {
//                List<Clients> collection = new List<Clients>();
//                using (var Command = new OracleCommand("select * from CLIENTS", Conn))
//                using (var reader = Command.ExecuteReader())
//                    while (reader.Read())
//                        collection.Add(new Clients(reader));
//                return collection;
//            }

//            //Fields
//            public int ClientId;
//            public string Description;
//            public string ClientKey;

//            //Constructors
//            public Clients() { }
//            public Clients(OracleDataReader reader)
//            {
//                if (reader["CLIENT_ID"] != DBNull.Value)
//                    this.ClientId = Convert.ToInt32(reader["CLIENT_ID"]);
//                if (reader["DESCRIPTION"] != DBNull.Value)
//                    this.Description = Convert.ToString(reader["DESCRIPTION"]);
//                if (reader["CLIENT_KEY"] != DBNull.Value)
//                    this.ClientKey = Convert.ToString(reader["CLIENT_KEY"]);
//            }

//            //Methods
//            public override bool Insert(OracleConnection Conn)
//            {
//                string sql = string.Empty;
//                sql += "INSERT INTO CLIENTS (CLIENT_ID,DESCRIPTION,CLIENT_KEY) VALUES (:a, :b, :c) ";

//                using (var Command = new OracleCommand(sql, Conn))
//                {
//                    ClientId = GetId();
//                    Command.Parameters.Add(":a", OracleDbType.Int32).Value = ClientId;
//                    Command.Parameters.Add(":b", OracleDbType.Varchar2).Value = Description;
//                    Command.Parameters.Add(":c", OracleDbType.Varchar2).Value = ClientKey;

//                    var r = Convert.ToBoolean(Command.ExecuteNonQuery());
//                    if (OnInsert != null) OnInsert(this, EventArgs.Empty);
//                    return r;
//                }
//            }
//            public override bool Delete(OracleConnection Conn)
//            {
//                string sql = string.Empty;
//                sql += "DELETE FROM CLIENTS ";
//                sql += "WHERE CLIENT_ID = :cid";

//                using (var Command = new OracleCommand(sql, Conn))
//                {
//                    Command.Parameters.Add(":cid", OracleDbType.Int32).Value = ClientId;

//                    var r = Convert.ToBoolean(Command.ExecuteNonQuery());
//                    if (OnDelete != null) OnDelete(this, EventArgs.Empty);
//                    return r;
//                }
//            }
//            public override bool Update(OracleConnection Conn)
//            {
//                string sql = string.Empty;
//                sql += "UPDATE CLIENTS SET ";
//                sql += "DESCRIPTION = :b, CLIENT_KEY = :c ";
//                sql += "WHERE CLIENT_ID = :a";

//                using (var Command = new OracleCommand(sql, Conn))
//                {
//                    Command.Parameters.Add(":b", OracleDbType.Varchar2).Value = Description;
//                    Command.Parameters.Add(":c", OracleDbType.Varchar2).Value = ClientKey;
//                    Command.Parameters.Add(":a", OracleDbType.Int32).Value = ClientId;

//                    var r = Convert.ToBoolean(Command.ExecuteNonQuery());
//                    if (OnUpdate != null) OnUpdate(this, EventArgs.Empty);
//                    return r;
//                }
//            }


//            //Overriden Methods from Object
//            public override bool Equals(object obj)
//            {
//                return base.Equals(obj);
//            }
//            public override int GetHashCode()
//            {
//                return base.GetHashCode();
//            }
//            public override string ToString()
//            {
//                return base.ToString();
//            }

//            public static bool operator ==(Clients a, Clients b)
//            {

//                if (a.ClientId != b.ClientId)
//                    return false;
//                if (a.ClientKey != b.ClientKey)
//                    return false;
//                if (a.Description != b.Description)
//                    return false;
//                return true;
//            }

//            public static bool operator !=(Clients a, Clients b)
//            {
//                if ((a == b))
//                    return false;
//                else
//                    return true;
//            }
//        }

//        public sealed class ClientDBCompanies : PlexxisDataTransferObjects
//        {
//            public static event Subscriber OnInsert;
//            public static event Subscriber OnUpdate;
//            public static event Subscriber OnDelete;

//            //Static methods
//            public static List<ClientDBCompanies> GetAll()
//            {
//                using (var Conn = new OracleConnection(ConnectionString))
//                {
//                    Conn.Open();
//                    return GetAll(Conn);
//                }
//            }
//            public static List<ClientDBCompanies> GetAll(OracleConnection Conn)
//            {
//                List<ClientDBCompanies> collection = new List<ClientDBCompanies>();
//                using (var Command = new OracleCommand("select * from CLIENT_DB_COMPANIES", Conn))
//                using (var reader = Command.ExecuteReader())
//                    while (reader.Read())
//                        collection.Add(new ClientDBCompanies(reader));
//                return collection;
//            }

//            //Fields
//            public int DbCompanyId;
//            public string DatabaseSid;
//            public int CompanyId;
//            public string CompanyCode;
//            public int ClientId;

//            //Constructors
//            public ClientDBCompanies() { }
//            public ClientDBCompanies(OracleDataReader reader)
//            {
//                if (reader["DB_COMPANY_ID"] != DBNull.Value)
//                    this.DbCompanyId = Convert.ToInt32(reader["DB_COMPANY_ID"]);

//                if (reader["DATABASE_SID"] != DBNull.Value)
//                    this.DatabaseSid = Convert.ToString(reader["DATABASE_SID"]);

//                if (reader["COMPANY_ID"] != DBNull.Value)
//                    this.CompanyId = Convert.ToInt32(reader["COMPANY_ID"]);

//                if (reader["COMPANY_CODE"] != DBNull.Value)
//                    this.CompanyCode = Convert.ToString(reader["COMPANY_CODE"]);

//                if (reader["CLIENT_ID"] != DBNull.Value)
//                    this.ClientId = Convert.ToInt32(reader["CLIENT_ID"]);
//            }

//            //Methods
//            public override bool Insert(OracleConnection Conn)
//            {
//                string sql = string.Empty;
//                sql += "insert into CLIENT_DB_COMPANIES ";
//                sql += "(db_company_id, database_sid,company_id,company_code,client_id) ";
//                sql += "values (:a,:b,:c,:d,:e)";

//                using (var Command = new OracleCommand(sql, Conn))
//                {
//                    DbCompanyId = GetId();
//                    Command.Parameters.Add(":a", OracleDbType.Int32).Value = DbCompanyId;
//                    Command.Parameters.Add(":b", OracleDbType.Varchar2).Value = DatabaseSid;
//                    Command.Parameters.Add(":c", OracleDbType.Int32).Value = CompanyId;
//                    Command.Parameters.Add(":d", OracleDbType.Varchar2).Value = CompanyCode;
//                    Command.Parameters.Add(":e", OracleDbType.Int32).Value = ClientId;

//                    var r = Convert.ToBoolean(Command.ExecuteNonQuery());
//                    if (OnInsert != null) OnInsert(this, EventArgs.Empty);
//                    return r;
//                }
//            }
//            public override bool Delete(OracleConnection Conn)
//            {
//                string sql = string.Empty;
//                sql += "DELETE FROM CLIENT_DB_COMPANIES WHERE DB_COMPANY_ID = :dci";

//                using (var Command = new OracleCommand(sql, Conn))
//                {
//                    Command.Parameters.Add("dci", OracleDbType.Int32).Value = DbCompanyId;
//                    var r = Convert.ToBoolean(Command.ExecuteNonQuery());
//                    if (OnDelete != null) OnDelete(this, EventArgs.Empty);
//                    return r;
//                }
//            }
//            public override bool Update(OracleConnection Conn)
//            {
//                string sql = string.Empty;
//                sql += "update client_db_companies set ";
//                sql += "database_sid = :b, company_id = :c, company_code = :d, client_id = :e ";
//                sql += "where db_company_id = :a ";

//                using (var Command = new OracleCommand(sql, Conn))
//                {
//                    Command.Parameters.Add(":b", OracleDbType.Varchar2).Value = DatabaseSid;
//                    Command.Parameters.Add(":c", OracleDbType.Varchar2).Value = CompanyId;
//                    Command.Parameters.Add(":d", OracleDbType.Varchar2).Value = CompanyCode;
//                    Command.Parameters.Add(":e", OracleDbType.Varchar2).Value = ClientId;
//                    Command.Parameters.Add(":a", OracleDbType.Int32).Value = DbCompanyId;

//                    var r = Convert.ToBoolean(Command.ExecuteNonQuery());
//                    if (OnUpdate != null) OnUpdate(this, EventArgs.Empty);
//                    return r;
//                }
//            }
//            public override bool Equals(object obj)
//            {
//                return base.Equals(obj);
//            }
//            public override int GetHashCode()
//            {
//                return base.GetHashCode();
//            }
//            public override string ToString()
//            {
//                return base.ToString();
//            }

//            public static bool operator ==(ClientDBCompanies a, ClientDBCompanies b)
//            {

//                if (a.ClientId != b.ClientId)
//                    return false;
//                if (a.DbCompanyId != b.DbCompanyId)
//                    return false;
//                if (a.DatabaseSid != b.DatabaseSid)
//                    return false;
//                if (a.CompanyId != b.CompanyId)
//                    return false;
//                if (a.CompanyCode != b.CompanyCode)
//                    return false;
//                return true;
//            }

//            public static bool operator !=(ClientDBCompanies a, ClientDBCompanies b)
//            {
//                if ((a == b))
//                    return false;
//                else
//                    return true;
//            }
//        }

//        public sealed class ClientDBCompanyUsers : PlexxisDataTransferObjects
//        {
//            public static event Subscriber OnInsert;
//            public static event Subscriber OnUpdate;
//            public static event Subscriber OnDelete;

//            //Static methods
//            public static List<ClientDBCompanyUsers> GetAll()
//            {
//                using (var Conn = new OracleConnection(ConnectionString))
//                {
//                    Conn.Open();
//                    return GetAll(Conn);
//                }
//            }
//            public static List<ClientDBCompanyUsers> GetAll(OracleConnection Conn)
//            {
//                List<ClientDBCompanyUsers> collection = new List<ClientDBCompanyUsers>();
//                using (var Command = new OracleCommand("select * from CLIENT_DB_COMPANY_USERS", Conn))
//                using (var reader = Command.ExecuteReader())
//                    while (reader.Read())
//                        collection.Add(new ClientDBCompanyUsers(reader));
//                return collection;
//            }

//            //Fields
//            public int DbCompanyUserId;
//            public int DbCompanyId;
//            public int UserId;

//            //Constructors
//            public ClientDBCompanyUsers() { }
//            public ClientDBCompanyUsers(OracleDataReader reader)
//            {
//                if (reader["DB_COMPANY_USER_ID"] != DBNull.Value)
//                    this.DbCompanyUserId = Convert.ToInt32(reader["DB_COMPANY_USER_ID"]);

//                if (reader["DB_COMPANY_ID"] != DBNull.Value)
//                    this.DbCompanyId = Convert.ToInt32(reader["Db_COMPANY_ID"]);

//                if (reader["USER_ID"] != DBNull.Value)
//                    this.UserId = Convert.ToInt32(reader["USER_ID"]);
//            }

//            //Methods
//            public override bool Insert(OracleConnection Conn)
//            {
//                string sql = string.Empty;
//                sql += "insert into client_db_company_users ";
//                sql += "(db_company_user_id,db_company_Id, user_id) ";
//                sql += "values(:a,:b,:c)";

//                using (var Command = new OracleCommand(sql, Conn))
//                {
//                    DbCompanyUserId = GetId();
//                    Command.Parameters.Add(":a", OracleDbType.Int32).Value = DbCompanyUserId;
//                    Command.Parameters.Add(":b", OracleDbType.Int32).Value = DbCompanyId;
//                    Command.Parameters.Add(":c", OracleDbType.Int32).Value = UserId;

//                    var r = Convert.ToBoolean(Command.ExecuteNonQuery());
//                    if (OnInsert != null) OnInsert(this, EventArgs.Empty);
//                    return r;
//                }
//            }

//            public override bool Delete(OracleConnection Conn)
//            {
//                string sql = string.Empty;
//                sql += "DELETE FROM CLIENT_DB_COMPANY_USERS WHERE DB_COMPANY_USER_ID = :dcui";
//                using (var Command = new OracleCommand(sql, Conn))
//                {
//                    Command.Parameters.Add(":dcui", OracleDbType.Int32).Value = DbCompanyUserId;
//                    var r = Convert.ToBoolean(Command.ExecuteNonQuery());
//                    if (OnDelete != null) OnDelete(this, EventArgs.Empty);
//                    return r;
//                }
//            }
//            public override bool Update(OracleConnection Conn)
//            {
//                string sql = string.Empty;

//                sql += "update client_db_company_users set ";
//                sql += "db_company_id = :b, user_id = :c ";
//                sql += "where db_company_user_id = :a";

//                using (var Command = new OracleCommand(sql, Conn))
//                {
//                    Command.Parameters.Add(":b", ResolveType(DbCompanyId)).Value = DbCompanyId;
//                    Command.Parameters.Add(":c", ResolveType(UserId)).Value = UserId;
//                    Command.Parameters.Add(":a", ResolveType(DbCompanyUserId)).Value = DbCompanyUserId;

//                    var r = Convert.ToBoolean(Command.ExecuteNonQuery());
//                    if (OnUpdate != null) OnUpdate(this, EventArgs.Empty);
//                    return r;
//                }
//            }

//            public override bool Equals(object obj)
//            {
//                return base.Equals(obj);
//            }
//            public override int GetHashCode()
//            {
//                return base.GetHashCode();
//            }
//            public override string ToString()
//            {
//                return base.ToString();
//            }

//            public static bool operator ==(ClientDBCompanyUsers a, ClientDBCompanyUsers b)
//            {

//                if (a.DbCompanyUserId != b.DbCompanyUserId)
//                    return false;
//                if (a.DbCompanyId != b.DbCompanyId)
//                    return false;
//                if (a.UserId != b.UserId)
//                    return false;
//                return true;
//            }

//            public static bool operator !=(ClientDBCompanyUsers a, ClientDBCompanyUsers b)
//            {
//                if ((a == b))
//                    return false;
//                else
//                    return true;
//            }
//        }

//        public sealed class ClientDBCompanyUserApps : PlexxisDataTransferObjects
//        {
//            public static event Subscriber OnInsert;
//            public static event Subscriber OnUpdate;
//            public static event Subscriber OnDelete;

//            //Static methods
//            public static List<ClientDBCompanyUserApps> GetAll()
//            {
//                using (var Conn = new OracleConnection(ConnectionString))
//                {
//                    Conn.Open();
//                    return GetAll(Conn);
//                }
//            }
//            public static List<ClientDBCompanyUserApps> GetAll(OracleConnection Conn)
//            {
//                List<ClientDBCompanyUserApps> collection = new List<ClientDBCompanyUserApps>();
//                using (var Command = new OracleCommand("select * from CLIENT_DB_COMPANY_USER_APPS", Conn))
//                using (var reader = Command.ExecuteReader())
//                    while (reader.Read())
//                        collection.Add(new ClientDBCompanyUserApps(reader));
//                return collection;
//            }
//            //Fields
//            public int DbCompanyUserAppId;
//            public int DbCompanyUserId;
//            public int AppId;

//            //Constructors
//            public ClientDBCompanyUserApps() { }
//            public ClientDBCompanyUserApps(OracleDataReader reader)
//            {
//                if (reader["DB_COMPANY_USER_APP_ID"] != DBNull.Value)
//                    this.DbCompanyUserAppId = Convert.ToInt32(reader["DB_COMPANY_USER_APP_ID"]);

//                if (reader["DB_COMPANY_USER_ID"] != DBNull.Value)
//                    this.DbCompanyUserId = Convert.ToInt32(reader["DB_COMPANY_USER_ID"]);

//                if (reader["APP_ID"] != DBNull.Value)
//                    this.AppId = Convert.ToInt32(reader["APP_ID"]);
//            }

//            //Methods
//            public override bool Insert(OracleConnection Conn)
//            {
//                string sql = string.Empty;
//                sql += "insert into CLIENT_DB_COMPANY_USER_APPS ";
//                sql += "(DB_COMPANY_USER_APP_ID,Db_Company_User_Id,app_id) ";
//                sql += "values (:a,:b,:c)";

//                using (var Command = new OracleCommand(sql, Conn))
//                {
//                    DbCompanyUserAppId = GetId();
//                    Command.Parameters.Add(":a", OracleDbType.Int32).Value = DbCompanyUserAppId;
//                    Command.Parameters.Add(":b", OracleDbType.Int32).Value = DbCompanyUserId;
//                    Command.Parameters.Add(":c", OracleDbType.Int32).Value = AppId;

//                    var r = Convert.ToBoolean(Command.ExecuteNonQuery());
//                    if (OnInsert != null) OnInsert(this, EventArgs.Empty);
//                    return r;
//                }
//            }
//            public override bool Delete(OracleConnection Conn)
//            {
//                string sql = string.Empty;
//                sql += "DELETE FROM CLIENT_DB_COMPANY_USER_APPS WHERE DB_COMPANY_USER_APP_ID = :id";
//                using (var Command = new OracleCommand(sql, Conn))
//                {
//                    Command.Parameters.Add(":id", OracleDbType.Int32).Value = DbCompanyUserAppId;
//                    var r = Convert.ToBoolean(Command.ExecuteNonQuery());
//                    if (OnDelete != null) OnDelete(this, EventArgs.Empty);
//                    return r;
//                }
//            }
//            public override bool Update(OracleConnection Conn)
//            {
//                string sql = string.Empty;
//                sql += "update CLIENT_DB_COMPANY_USER_APPS set ";
//                sql += "Db_Company_User_Id = :b, app_id = :c ";
//                sql += "where DB_COMPANY_USER_APP_ID = :a ";

//                using (var Command = new OracleCommand(sql, Conn))
//                {
//                    Command.Parameters.Add(":b", OracleDbType.Int32).Value = DbCompanyUserId;
//                    Command.Parameters.Add(":c", OracleDbType.Int32).Value = AppId;
//                    Command.Parameters.Add(":a", OracleDbType.Int32).Value = DbCompanyUserAppId;

//                    var r = Convert.ToBoolean(Command.ExecuteNonQuery());
//                    if (OnUpdate != null) OnUpdate(this, EventArgs.Empty);
//                    return r;

//                }
//            }

//            public override bool Equals(object obj)
//            {
//                return base.Equals(obj);
//            }
//            public override int GetHashCode()
//            {
//                return base.GetHashCode();
//            }
//            public override string ToString()
//            {
//                return base.ToString();
//            }

//            public static bool operator ==(ClientDBCompanyUserApps a, ClientDBCompanyUserApps b)
//            {

//                if (a.DbCompanyUserAppId != b.DbCompanyUserId)
//                    return false;
//                if (a.DbCompanyUserId != b.DbCompanyUserId)
//                    return false;
//                if (a.AppId != b.AppId)
//                    return false;
//                return true;
//            }

//            public static bool operator !=(ClientDBCompanyUserApps a, ClientDBCompanyUserApps b)
//            {
//                if ((a == b))
//                    return false;
//                else
//                    return true;
//            }
//        }

//        public sealed class ClientUsers : PlexxisDataTransferObjects
//        {
//            public static event Subscriber OnInsert;
//            public static event Subscriber OnUpdate;
//            public static event Subscriber OnDelete;

//            //Static methods
//            public static List<ClientUsers> GetAll()
//            {
//                using (var Conn = new OracleConnection(ConnectionString))
//                {
//                    Conn.Open();
//                    return GetAll(Conn);
//                }
//            }

//            public static List<ClientUsers> GetAll(OracleConnection Conn)
//            {
//                List<ClientUsers> collection = new List<ClientUsers>();
//                using (var Command = new OracleCommand("select * from CLIENT_USERS", Conn))
//                using (var reader = Command.ExecuteReader(CommandBehavior.CloseConnection))
//                    while (reader.Read())
//                        collection.Add(new ClientUsers(reader));
//                return collection;
//            }

//            //Fields
//            public int UserId;
//            public int ClientId;
//            public string Name;
//            public string Password;

//            //Constructors
//            public ClientUsers() { }
//            public ClientUsers(OracleDataReader reader)
//            {
//                if (reader["USER_ID"] != DBNull.Value)
//                    this.UserId = Convert.ToInt32(reader["USER_ID"]);

//                if (reader["CLIENT_ID"] != DBNull.Value)
//                    this.ClientId = Convert.ToInt32(reader["CLIENT_ID"]);

//                if (reader["NAME"] != DBNull.Value)
//                    this.Name = Convert.ToString(reader["NAME"]);

//                if (reader["PASSWORD"] != DBNull.Value)
//                    this.Password = Convert.ToString(reader["PASSWORD"]);
//            }

//            //Methods
//            public override bool Insert(OracleConnection Conn)
//            {
//                string sql = string.Empty;
//                sql += "insert into client_users(user_id, client_id, name, password) ";
//                sql += "values (:a,:b,:c,:d) ";

//                using (var Command = new OracleCommand(sql, Conn))
//                {
//                    UserId = GetId();
//                    Command.Parameters.Add(":a", OracleDbType.Int32).Value = UserId;
//                    Command.Parameters.Add(":b", OracleDbType.Int32).Value = ClientId;
//                    Command.Parameters.Add(":c", OracleDbType.Varchar2).Value = Name;
//                    Command.Parameters.Add(":d", OracleDbType.Varchar2).Value = Password;

//                    var r = Convert.ToBoolean(Command.ExecuteNonQuery());
//                    if (OnInsert != null) OnInsert(this, EventArgs.Empty);
//                    return r;
//                }
//            }
//            public override bool Delete(OracleConnection Conn)
//            {
//                string sql = string.Empty;
//                sql += "DELETE FROM CLIENT_USERS WHERE USER_ID = :id";
//                using (var Command = new OracleCommand(sql, Conn))
//                {
//                    Command.Parameters.Add(":id", OracleDbType.Int32).Value = UserId;
//                    var r = Convert.ToBoolean(Command.ExecuteNonQuery());
//                    if (OnDelete != null) OnDelete(this, EventArgs.Empty);
//                    return r;
//                }
//            }
//            public override bool Update(OracleConnection Conn)
//            {
//                string sql = string.Empty;
//                sql += "update client_users set ";
//                sql += "client_id = :b, name = :c, password = :d ";
//                sql += "where user_id = :a ";

//                using (var Command = new OracleCommand(sql, Conn))
//                {
//                    Command.Parameters.Add(":b", OracleDbType.Int32).Value = ClientId;
//                    Command.Parameters.Add(":c", OracleDbType.Varchar2).Value = Name;
//                    Command.Parameters.Add(":d", OracleDbType.Varchar2).Value = Password;
//                    Command.Parameters.Add(":a", OracleDbType.Int32).Value = UserId;

//                    var r = Convert.ToBoolean(Command.ExecuteNonQuery());
//                    if (OnUpdate != null) OnUpdate(this, EventArgs.Empty);
//                    return r;
//                }
//            }

//            public override bool Equals(object obj)
//            {
//                return base.Equals(obj);
//            }
//            public override int GetHashCode()
//            {
//                return base.GetHashCode();
//            }
//            public override string ToString()
//            {
//                return base.ToString();
//            }

//            public static bool operator ==(ClientUsers a, ClientUsers b)
//            {

//                if (a.UserId != b.UserId)
//                    return false;
//                if (a.ClientId != b.ClientId)
//                    return false;
//                if (a.Name != b.Name)
//                    return false;
//                if (a.Password != b.Password)
//                    return false;
//                return true;
//            }

//            public static bool operator !=(ClientUsers a, ClientUsers b)
//            {
//                if ((a == b))
//                    return false;
//                else
//                    return true;
//            }
//        }

//        public sealed class Logs : PlexxisDataTransferObjects
//        {
//            public static event Subscriber OnInsert;
//            public static event Subscriber OnUpdate;
//            public static event Subscriber OnDelete;

//            //Static methods
//            public static List<Logs> GetAll()
//            {
//                using (var Conn = new OracleConnection(ConnectionString))
//                {
//                    Conn.Open();
//                    return GetAll(Conn);
//                }
//            }

//            public static List<Logs> GetAll(OracleConnection Conn)
//            {
//                List<Logs> collection = new List<Logs>();
//                using (var Command = new OracleCommand("select * from LOGS", Conn))
//                using (var reader = Command.ExecuteReader())
//                    while (reader.Read())
//                        collection.Add(new Logs(reader));
//                return collection;
//            }

//            //Fields
//            public int LogId;
//            public DateTime LogDate;
//            public string Description;
//            public int ClientId;

//            //Constructors
//            public Logs() { }
//            public Logs(OracleDataReader reader)
//            {
//                if (reader["Log_ID"] != DBNull.Value)
//                    this.LogId = Convert.ToInt32(reader["Log_ID"]);

//                if (reader["Log_DATE"] != DBNull.Value)
//                    this.LogDate = Convert.ToDateTime(reader["LOG_DATE"]);

//                if (reader["DESCRIPTION"] != DBNull.Value)
//                    this.Description = Convert.ToString(reader["DESCRIPTION"]);

//                if (reader["CLIENT_ID"] != DBNull.Value)
//                    this.ClientId = Convert.ToInt32(reader["CLIENT_ID"]);

//            }

//            //Methods
//            public override bool Insert(OracleConnection Conn)
//            {
//                string sql = string.Empty;
//                sql += "insert into Logs ";
//                sql += "(Log_id, Log_Date, description,client_id) ";
//                sql += "values (:a, :b, :c, :d) ";

//                using (var Command = new OracleCommand(sql, Conn))
//                {
//                    LogId = GetId();
//                    Command.Parameters.Add(":a", OracleDbType.Int32).Value = LogId;
//                    Command.Parameters.Add(":b", OracleDbType.Date).Value = LogDate;
//                    Command.Parameters.Add(":c", OracleDbType.Varchar2).Value = Description;
//                    Command.Parameters.Add(":d", OracleDbType.Int32).Value = ClientId;


//                    var r = Convert.ToBoolean(Command.ExecuteNonQuery());
//                    if (OnInsert != null) OnInsert(this, EventArgs.Empty);
//                    return r;

//                }
//            }
//            public override bool Delete(OracleConnection Conn)
//            {
//                string sql = string.Empty;
//                sql += "delete from logs where log_id = :a";

//                using (var Command = new OracleCommand(sql, Conn))
//                {
//                    Command.Parameters.Add(":a", OracleDbType.Int32).Value = LogId;
//                    var r = Convert.ToBoolean(Command.ExecuteNonQuery());
//                    if (OnDelete != null) OnDelete(this, EventArgs.Empty);
//                    return r;
//                }
//            }
//            public override bool Update(OracleConnection Conn)
//            {
//                string sql = string.Empty;
//                sql += "update Logs set ";
//                sql += "Log_Date = :b,description = :c, client_id = :d ";
//                sql += "where Log_id = :a ";

//                using (var Command = new OracleCommand(sql, Conn))
//                {

//                    Command.Parameters.Add(":b", OracleDbType.Date).Value = LogDate;
//                    Command.Parameters.Add(":c", OracleDbType.Varchar2).Value = Description;
//                    Command.Parameters.Add(":d", OracleDbType.Int32).Value = ClientId;
//                    Command.Parameters.Add(":a", OracleDbType.Int32).Value = LogId;

//                    var r = Convert.ToBoolean(Command.ExecuteNonQuery());
//                    if (OnUpdate != null) OnUpdate(this, EventArgs.Empty);
//                    return r;
//                }
//            }
//            public override bool Equals(object obj)
//            {
//                return base.Equals(obj);
//            }
//            public override int GetHashCode()
//            {
//                return base.GetHashCode();
//            }
//            public override string ToString()
//            {
//                return base.ToString();
//            }

//            public static bool operator ==(Logs a, Logs b)
//            {

//                if (a.LogId != b.LogId)
//                    return false;
//                if (a.LogDate != b.LogDate)
//                    return false;
//                if (a.Description != b.Description)
//                    return false;
//                if (a.ClientId != b.ClientId)
//                    return false;
//                return true;
//            }

//            public static bool operator !=(Logs a, Logs b)
//            {
//                if ((a == b))
//                    return false;
//                else
//                    return true;
//            }
//        }

//        public sealed class PMHSettings : PlexxisDataTransferObjects
//        {
//            public static event Subscriber OnInsert;
//            public static event Subscriber OnUpdate;
//            public static event Subscriber OnDelete;

//            //Static Methods
//            public static List<PMHSettings> GetAll()
//            {
//                using (var Conn = new OracleConnection(ConnectionString))
//                {
//                    Conn.Open();
//                    return GetAll(Conn);
//                }
//            }

//            public static List<PMHSettings> GetAll(OracleConnection Conn)
//            {
//                List<PMHSettings> collection = new List<PMHSettings>();
//                using (var Command = new OracleCommand("select * from PMH_SETTINGS", Conn))
//                using (var reader = Command.ExecuteReader())
//                    while (reader.Read())
//                        collection.Add(new PMHSettings(reader));
//                return collection;
//            }

//            //Fields
//            public int PmhId;

//            //Constructors
//            public PMHSettings() { }
//            public PMHSettings(OracleDataReader reader)
//            {
//                if (reader["PMH_ID"] != DBNull.Value)
//                    this.PmhId = Convert.ToInt32(reader["PMH_ID"]);
//            }

//            //Methods
//            public override bool Insert(OracleConnection Conn)
//            {
//                string sql = string.Empty;
//                sql += "INSERT INTO PMH_SETTINGS (PMH_ID) VALUES (:pmhid)";

//                using (var Command = new OracleCommand(sql, Conn))
//                {
//                    Command.Parameters.Add(":pmhid", PmhId).Value = PmhId;
//                    var r = Convert.ToBoolean(Command.ExecuteNonQuery());
//                    if (OnInsert != null) OnInsert(this, EventArgs.Empty);
//                    return r;
//                }
//            }

//            public override bool Delete(OracleConnection Conn)
//            {
//                string sql = string.Empty;
//                sql += "DELETE FROM PMH_SETTINGS WHERE PMH_ID = :pmhid";

//                using (var Command = new OracleCommand(sql, Conn))
//                {
//                    Command.Parameters.Add(":pmhid", PmhId).Value = PmhId;
//                    return Convert.ToBoolean(Command.ExecuteNonQuery());
//                }
//            }

//            public override bool Update(OracleConnection Conn)
//            {
//                //not meaningful to have an update here presently
//                string sql = string.Empty;
//                sql += "UPDATE PMH_SETTINGS SET PMH_ID = PMH_ID";

//                using (var Command = new OracleCommand(sql, Conn))
//                {
//                    var r = Convert.ToBoolean(Command.ExecuteNonQuery());
//                    if (OnUpdate != null) OnDelete(this, EventArgs.Empty);
//                    return r;
//                }
//            }

//            public override bool Equals(object obj)
//            {
//                return base.Equals(obj);
//            }
//            public override int GetHashCode()
//            {
//                return base.GetHashCode();
//            }
//            public override string ToString()
//            {
//                return base.ToString();
//            }

//            public static bool operator ==(PMHSettings a, PMHSettings b)
//            {

//                if (a.PmhId != b.PmhId)
//                    return false;
//                return true;
//            }

//            public static bool operator !=(PMHSettings a, PMHSettings b)
//            {
//                if ((a == b))
//                    return false;
//                else
//                    return true;
//            }
//        }

//        public sealed class QuerySequenceRequests : PlexxisDataTransferObjects
//        {
//            public static event Subscriber OnInsert;
//            public static event Subscriber OnUpdate;
//            public static event Subscriber OnDelete;

//            //Static Methods
//            public static List<QuerySequenceRequests> GetAll()
//            {
//                using (var Conn = new OracleConnection(ConnectionString))
//                {
//                    Conn.Open();
//                    return GetAll(Conn);
//                }
//            }
//            public static List<QuerySequenceRequests> GetAll(OracleConnection Conn)
//            {
//                List<QuerySequenceRequests> collection = new List<QuerySequenceRequests>();
//                using (var Command = new OracleCommand("select * from QUERY_SEQUENCE_REQUESTS", Conn))
//                using (var reader = Command.ExecuteReader())
//                    while (reader.Read())
//                        collection.Add(new QuerySequenceRequests(reader));
//                return collection;
//            }


//            //Fields
//            public int TqrId;
//            public int UserQuerying;
//            public bool DatabaseRequired;
//            public int QuerySequencing;
//            public DateTime SeqQueryTime;

//            //Constructors
//            public QuerySequenceRequests() { }
//            public QuerySequenceRequests(OracleDataReader reader)
//            {
//                if (reader["Tqr_Id"] != DBNull.Value)
//                    this.TqrId = Convert.ToInt32(reader["Tqr_Id"]);

//                if (reader["USER_QUERYING"] != DBNull.Value)
//                    this.UserQuerying = Convert.ToInt32(reader["USER_QUERYING"]);

//                if (reader["DATABASE_QUERIED"] != DBNull.Value)
//                    this.DatabaseRequired = Convert.ToBoolean(reader["DATABASE_QUERIED"]);

//                if (reader["QUERY_SEQUENCING"] != DBNull.Value)
//                    this.QuerySequencing = Convert.ToInt32(reader["QUERY_SEQUENCING"]);

//                if (reader["Seq_QUERY_TIME"] != DBNull.Value)
//                    this.SeqQueryTime = Convert.ToDateTime(reader["SEQ_QUERY_TIME"]);
//            }

//            //Methods
//            public override bool Insert(OracleConnection Conn)
//            {
//                string sql = string.Empty;
//                sql += "insert into query_sequence_requests ";
//                sql += "(tqr_id,user_querying,database_queried,query_sequencing,seq_query_time) ";
//                sql += "values (:a,:b,:c,:d,:e)";

//                using (var Command = new OracleCommand(sql, Conn))
//                {
//                    TqrId = GetId();
//                    Command.Parameters.Add(":a", OracleDbType.Int32).Value = TqrId;
//                    Command.Parameters.Add(":b", OracleDbType.Int32).Value = UserQuerying;
//                    Command.Parameters.Add(":c", OracleDbType.Int32).Value = Convert.ToInt32(DatabaseRequired);
//                    Command.Parameters.Add(":d", OracleDbType.Int32).Value = QuerySequencing;
//                    Command.Parameters.Add(":e", OracleDbType.Date).Value = SeqQueryTime;
//                    var r = Convert.ToBoolean(Command.ExecuteNonQuery());
//                    if (OnInsert != null) OnInsert(this, EventArgs.Empty);
//                    return r;
//                }
//            }
//            public override bool Delete(OracleConnection Conn)
//            {
//                string sql = string.Empty;
//                sql += "DELETE FROM QUERY_SEQUENCE_REQUESTS WHERE Tqr_Id = :id";
//                using (var Command = new OracleCommand(sql, Conn))
//                {
//                    Command.Parameters.Add(":id", OracleDbType.Int32).Value = TqrId;
//                    var r = Convert.ToBoolean(Command.ExecuteNonQuery());
//                    if (OnDelete != null) OnDelete(this, EventArgs.Empty);
//                    return r;
//                }
//            }
//            public override bool Update(OracleConnection Conn)
//            {
//                string sql = string.Empty;
//                sql += "update query_sequence_requests set ";
//                sql += "user_querying = :b, database_queried = :c, query_sequencing = :d, seq_query_time = :e ";
//                sql += "where tqr_id = :a ";

//                using (var Command = new OracleCommand(sql, Conn))
//                {
//                    Command.Parameters.Add(":b", OracleDbType.Int32).Value = UserQuerying;
//                    Command.Parameters.Add(":c", OracleDbType.Int32).Value = Convert.ToInt32(DatabaseRequired);
//                    Command.Parameters.Add(":d", OracleDbType.Int32).Value = QuerySequencing;
//                    Command.Parameters.Add(":e", OracleDbType.Date).Value = SeqQueryTime;
//                    Command.Parameters.Add(":a", OracleDbType.Int32).Value = TqrId;

//                    var r = Convert.ToBoolean(Command.ExecuteNonQuery());
//                    if (OnUpdate != null) OnUpdate(this, EventArgs.Empty);
//                    return r;
//                }
//            }


//            public override bool Equals(object obj)
//            {
//                return base.Equals(obj);
//            }
//            public override int GetHashCode()
//            {
//                return base.GetHashCode();
//            }
//            public override string ToString()
//            {
//                return base.ToString();
//            }

//            public static bool operator ==(QuerySequenceRequests a, QuerySequenceRequests b)
//            {

//                if (a.TqrId != b.TqrId)
//                    return false;
//                if (a.UserQuerying != b.UserQuerying)
//                    return false;
//                if (a.DatabaseRequired != b.DatabaseRequired)
//                    return false;
//                if (a.QuerySequencing != b.QuerySequencing)
//                    return false;
//                if (a.SeqQueryTime != b.SeqQueryTime)
//                    return false;
//                return true;
//            }

//            public static bool operator !=(QuerySequenceRequests a, QuerySequenceRequests b)
//            {
//                if ((a == b))
//                    return false;
//                else
//                    return true;
//            }
//        }
//    }
//}
