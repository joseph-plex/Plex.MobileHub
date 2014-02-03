using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace Plex.PMH.Data.Tables
{
    public class APP_TABLE_COLUMNS : PlexxisDataTransferObjects
    {
        public static event Subscriber OnInsert;
        public static event Subscriber OnUpdate;
        public static event Subscriber OnDelete;

        public static IEnumerable<APP_TABLE_COLUMNS> GetAll()
        {
            using (var Conn = Utilities.GetConnection(true))
                return GetAll(Conn);
        }

        public static IEnumerable<APP_TABLE_COLUMNS> GetAll(IDbConnection Conn)
        {
            var collection = new List<APP_TABLE_COLUMNS>();
            using (var Command = Conn.CreateCommand("SELECT * FROM APP_TABLE_COLUMNS"))
            using (var reader = Command.ExecuteReader())
                while (reader.Read())
                    collection.Add(new APP_TABLE_COLUMNS(reader));
            return collection;
        }

        public int TABLE_ID;//NUMBER(10)	N			
        public int TABLE_COLUMN_ID;//NUMBER(10)	N			
        public string  COLUMN_NAME;//VARCHAR2(50)	N			
        public int COLUMN_SEQUENCE;//NUMBER(3)	N			
        public string DATA_TYPE;//VARCHAR2(50)	Y			
        public int? DATA_LENGTH;//NUMBER(10)	Y			
        public int? DATA_PRECISION;//NUMBER(2)	Y			
        public int? DATA_SCALE;//NUMBER(2)	Y			
        public int? ALLOW_DB_NULL;//NUMBER(1)	Y			
        public int? IS_READ_ONLY;//NUMBER(1)	Y			
        public int? IS_LONG;//NUMBER(1)	Y			
        public int? IS_KEY;//NUMBER(1)	Y			
        public string KEY_TYPE;//VARCHAR2(20)	Y			
        public int? IS_UNIQUE;//	NUMBER(1)	Y			
        public string DESCRIPTION;//VARCHAR2(4000)	Y		

        public APP_TABLE_COLUMNS() : base()
        {
            PrimaryKey.Add("TABLE_COLUMN_ID");
        }
        public APP_TABLE_COLUMNS(IDataReader reader)
        {
            AutoFill(reader, this);
        }
    }
}