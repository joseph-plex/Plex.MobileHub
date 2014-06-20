using System;
using System.Data;
using System.Collections.Generic;

namespace Plex.MobileHub.Data.Tables
{
    public class APP_TABLE_COLUMNS : PlexxisDataTransferObjects
    {
        public static event EventHandler OnInsert;
        public static event EventHandler OnUpdate;
        public static event EventHandler OnDelete;

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

        public int TABLE_ID { get; set; }//NUMBER(10)	N			
        public int TABLE_COLUMN_ID { get; set; }//NUMBER(10)	N			
        public string COLUMN_NAME { get; set; }//VARCHAR2(50)	N			
        public int COLUMN_SEQUENCE { get; set; }//NUMBER(3)	N			
        public string DATA_TYPE { get; set; }//VARCHAR2(50)	Y			
        public int? DATA_LENGTH { get; set; }//NUMBER(10)	Y			
        public int? DATA_PRECISION { get; set; }//NUMBER(2)	Y			
        public int? DATA_SCALE { get; set; }//NUMBER(2)	Y			
        public int? ALLOW_DB_NULL { get; set; }//NUMBER(1)	Y			
        public int? IS_READ_ONLY { get; set; }//NUMBER(1)	Y			
        public int? IS_LONG { get; set; }//NUMBER(1)	Y			
        public int? IS_KEY { get; set; }//NUMBER(1)	Y			
        public string KEY_TYPE { get; set; }//VARCHAR2(20)	Y			
        public int? IS_UNIQUE { get; set; }//	NUMBER(1)	Y			
        public string DESCRIPTION { get; set; }//VARCHAR2(4000)	Y		

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