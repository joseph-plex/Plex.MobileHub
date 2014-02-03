using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

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

        public static IEnumerable<APP_USER_TYPES> GetAll(IDbConnection Conn)
        {
            var collection = new List<APP_USER_TYPES>();
            using (var Command = Conn.CreateCommand("SELECT * FROM APP_USER_TYPES"))
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
        public IEnumerable<CLIENT_DB_COMPANY_USER_APPS> GetCLIENT_DB_COMPANY_USER_APPS(IDbConnection Conn)
        {
            var collection = new List<CLIENT_DB_COMPANY_USER_APPS>();
            using (var Command = Conn.CreateCommand("SELECT * FROM CLIENT_DB_COMPANY_USER_APPS WHERE DB_COMPANY_USER_ID = :a", USER_TYPE_ID))
            using (var reader = Command.ExecuteReader())
                while (reader.Read())
                    collection.Add(new CLIENT_DB_COMPANY_USER_APPS(reader));
            return collection;
        }

        public int USER_TYPE_ID;//	NUMBER(10)	N			
        public int APP_ID;//	NUMBER(10)	N			
        public string CODE;//VARCHAR2(12)	N			
        public string DESCRIPTION;//	VARCHAR2(50)	Y	

        public APP_USER_TYPES() : base()
        {
            PrimaryKey.Add("USER_TYPE_ID");
        }

        public APP_USER_TYPES(IDataReader reader)
        {
            AutoFill(reader, this);
        }
    }
}