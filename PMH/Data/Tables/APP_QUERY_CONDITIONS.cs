using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace Plex.PMH.Data.Tables
{
    public class APP_QUERY_CONDITIONS : PlexxisDataTransferObjects
    {
        public static event Subscriber OnInsert;
        public static event Subscriber OnUpdate;
        public static event Subscriber OnDelete;

        public static IEnumerable<APP_QUERY_CONDITIONS> GetAll()
        {
            using (var Conn = Utilities.GetConnection(true))
                return GetAll(Conn);
        }

        public static IEnumerable<APP_QUERY_CONDITIONS> GetAll(IDbConnection Conn)
        {
            var collection = new List<APP_QUERY_CONDITIONS>();
            using (var Command = Conn.CreateCommand("SELECT * FROM APP_QUERY_CONDITIONS"))
            using (var reader = Command.ExecuteReader())
                while (reader.Read())
                    collection.Add(new APP_QUERY_CONDITIONS(reader));
            return collection;
        }

        public int CONDITION_ID;//	NUMBER(10)	N			
        public int QUERY_ID;//	NUMBER(10)	N			
        public string COLUMN_NAME;//	VARCHAR2(30)	N			
        public string COLUMN_NVL;//	VARCHAR2(1000)	Y			
        public string COLUMN_VALUE;//	VARCHAR2(1000)	N			
        public string OPERATOR;//	VARCHAR2(12)	N		

        public APP_QUERY_CONDITIONS() : base()
        {
            PrimaryKey.Add("CONDITION_ID");
        }

        public APP_QUERY_CONDITIONS(IDataReader reader)
        {
            AutoFill(reader, this);
        }
    }
}