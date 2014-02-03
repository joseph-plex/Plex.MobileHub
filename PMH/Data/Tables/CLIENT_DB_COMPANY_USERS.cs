using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace Plex.PMH.Data.Tables
{
    public class CLIENT_DB_COMPANY_USERS : PlexxisDataTransferObjects
    {
        public static event Subscriber OnInsert;
        public static event Subscriber OnUpdate;
        public static event Subscriber OnDelete;


        public static IEnumerable<CLIENT_DB_COMPANY_USERS> GetAll()
        {
            using (var Conn = Utilities.GetConnection(true))
                return GetAll(Conn);
        }

        public static IEnumerable<CLIENT_DB_COMPANY_USERS> GetAll(IDbConnection Conn)
        {
            var collection = new List<CLIENT_DB_COMPANY_USERS>();
            using (var Command = Conn.CreateCommand("SELECT * FROM CLIENT_DB_COMPANY_USERS"))
            using (var reader = Command.ExecuteReader())
                while (reader.Read())
                    collection.Add(new CLIENT_DB_COMPANY_USERS(reader));
            return collection;
        }


        public int DB_COMPANY_USER_ID	;//NUMBER(10)	N			
        public int DB_COMPANY_ID;//	NUMBER(10)	N			
        public int USER_ID	;//NUMBER(10)	N			
        public string CONNECT_AS;//	VARCHAR2(100)	Y		

        public CLIENT_DB_COMPANY_USERS() : base()
        {
            PrimaryKey.Add("DB_COMPANY_USER_ID");
        }

        public CLIENT_DB_COMPANY_USERS(IDataReader reader)
        {
            AutoFill(reader, this);
        }
    }
}