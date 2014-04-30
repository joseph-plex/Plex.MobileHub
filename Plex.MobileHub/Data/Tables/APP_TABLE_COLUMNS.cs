using System;
using System.Data;
using System.Collections.Generic;

namespace Plex.MobileHub.Data.Tables
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
        public APP_TABLE_COLUMNS(IDataReader reader) : this()
        {
            AutoFill(reader, this);
        }
        public override bool Insert(IDbConnection Conn)
        {
            var r = base.Insert(Conn);
            if (OnInsert != null)
                OnInsert(this, EventArgs.Empty);
            return r;
        }

        public override bool Update(IDbConnection Conn)
        {
            var r = base.Update(Conn);
            if (OnUpdate != null)
                OnUpdate(this, EventArgs.Empty);
            return r;
        }

        public override bool Delete(IDbConnection Conn)
        {
            var r = base.Delete(Conn);
            if (OnDelete != null)
                OnDelete(this, EventArgs.Empty);
            return r;
        }
    }
}