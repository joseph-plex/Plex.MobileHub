using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.DataAccess.Client;

namespace Plex.PMH.Data.Tables
{
    public class APP_USER_TYPES : PlexxisDataTransferObjects
    {
        public static event Subscriber OnInsert;
        public static event Subscriber OnUpdate;
        public static event Subscriber OnDelete;

        public static IEnumerable<APP_USER_TYPES> GetAll()
        {
            using (var Conn = Utilities.GetConnection(true))
                return GetAll(Conn);
        }

        public static IEnumerable<APP_USER_TYPES> GetAll(OracleConnection Conn)
        {
            var collection = new List<APP_USER_TYPES>();
            using (var Command = new OracleCommand("SELECT * FROM APP_USER_TYPES", Conn))
            using (var reader = Command.ExecuteReader())
                while (reader.Read())
                    collection.Add(new APP_USER_TYPES(reader));
            return collection;
        }

        public IEnumerable<CLIENT_DB_COMPANY_USER_APPS> GetCLIENT_DB_COMPANY_USER_APPS()
        {
            using (var Conn = Utilities.GetConnection(true))
                return GetCLIENT_DB_COMPANY_USER_APPS(Conn);
        }
        public IEnumerable<CLIENT_DB_COMPANY_USER_APPS> GetCLIENT_DB_COMPANY_USER_APPS(OracleConnection Conn)
        {
            var collection = new List<CLIENT_DB_COMPANY_USER_APPS>();
           //todo check this out because this may be an error
            using (var Command = new OracleCommand("SELECT * FROM CLIENT_DB_COMPANY_USER_APPS WHERE DB_COMPANY_USER_ID = :a", Conn))
            {
                Command.Parameters.Add(":a", ResolveType(USER_TYPE_ID)).Value = USER_TYPE_ID;
                using (var reader = Command.ExecuteReader())
                    while (reader.Read())
                        collection.Add(new CLIENT_DB_COMPANY_USER_APPS(reader));
            }
            return collection;
        }

        public int USER_TYPE_ID;//	NUMBER(10)	N			
        public int APP_ID;//	NUMBER(10)	N			
        public string CODE;//VARCHAR2(12)	N			
        public string DESCRIPTION;//	VARCHAR2(50)	Y	

        public APP_USER_TYPES()
        {

        }

        public APP_USER_TYPES(OracleDataReader reader)
        {
            USER_TYPE_ID = pUSER_TYPE_ID(reader);
            APP_ID = pAPP_ID(reader);
            CODE = pCODE(reader);
            DESCRIPTION = pDESCRIPTION(reader);
        }

        int pUSER_TYPE_ID(OracleDataReader reader)
        {
            return (reader["USER_TYPE_ID"] != DBNull.Value) ? Convert.ToInt32(reader["USER_TYPE_ID"]) : new int();
        }
        int pAPP_ID(OracleDataReader reader)
        {
            return (reader["APP_ID"] != DBNull.Value) ? Convert.ToInt32(reader["APP_ID"]) : new int();
        }
        string pCODE(OracleDataReader reader)
        {
            return (reader["CODE"] != DBNull.Value) ? Convert.ToString(reader["CODE"]) : string.Empty;
        }
        string pDESCRIPTION(OracleDataReader reader)
        {
            return (reader["DESCRIPTION"] != DBNull.Value) ? Convert.ToString(reader["DESCRIPTION"]) : string.Empty;
        }


        public override bool Insert(OracleConnection Conn)
        {
            string sql = string.Empty;
            sql += "INSERT INTO APP_USER_TYPES (USER_TYPE_ID,APP_ID,CODE,DESCRIPTION)";
            sql += "VALUES (:a, :b, :c, :d ) ";

            using (var Command = new OracleCommand(sql, Conn))
            {
                USER_TYPE_ID = Utilities.SequenceNextValue(Sequences.ID_GEN);
                Command.Parameters.Add(":a", OracleDbType.Int32).Value = USER_TYPE_ID;
                Command.Parameters.Add(":b", OracleDbType.Int32).Value = APP_ID;
                Command.Parameters.Add(":c", OracleDbType.Varchar2).Value = CODE;
                Command.Parameters.Add(":d", OracleDbType.Varchar2).Value = DESCRIPTION;

                var r = Convert.ToBoolean(Command.ExecuteNonQuery());
                if (OnInsert != null) OnInsert(this, EventArgs.Empty);
                return r;
            }
        }

        public override bool Update(OracleConnection Conn)
        {
            string sql = string.Empty;
            sql += "UPDATE APP_USER_TYPES SET ";
            sql += "APP_ID = :b, CODE = :c, DESCRIPTION = :d ";
            sql += "where USER_TYPE_ID = :a ";

            using (var Command = new OracleCommand(sql, Conn))
            {
                Command.Parameters.Add(":b", OracleDbType.Int32).Value = APP_ID;
                Command.Parameters.Add(":c", OracleDbType.Varchar2).Value = CODE;
                Command.Parameters.Add(":d", OracleDbType.Varchar2).Value = DESCRIPTION;
                Command.Parameters.Add(":a", OracleDbType.Int32).Value = USER_TYPE_ID;

                var r = Convert.ToBoolean(Command.ExecuteNonQuery());
                if (OnUpdate != null) OnUpdate(this, EventArgs.Empty);
                return r;
            }
        }

        public override bool Delete(OracleConnection Conn)
        {
            string sql = string.Empty;
            sql += "DELETE FROM APP_USER_TYPES WHERE USER_TYPE_ID = :a";

            using (var Command = new OracleCommand(sql, Conn))
            {
                Command.Parameters.Add(":a", OracleDbType.Int32).Value = USER_TYPE_ID;
                var r = Convert.ToBoolean(Command.ExecuteNonQuery());
                if (OnDelete != null) OnDelete(this, EventArgs.Empty);
                return r;
            }
        }
    }
}