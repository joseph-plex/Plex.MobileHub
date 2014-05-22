using System;
using System.Data;
using System.Collections.Generic;

namespace Plex.MobileHub.Data.Tables
{
    public class CLIENT_DB_COMPANY_USER_APPS : PlexxisDataTransferObjects
    {
        public static event Subscriber OnInsert;
        public static event Subscriber OnUpdate;
        public static event Subscriber OnDelete;


        public static IEnumerable<CLIENT_DB_COMPANY_USER_APPS> GetAll()
        {
            using (var Conn = Utilities.GetConnection(true))
                return GetAll(Conn);
        }

        public static IEnumerable<CLIENT_DB_COMPANY_USER_APPS> GetAll(IDbConnection Conn)
        {
            var collection = new List<CLIENT_DB_COMPANY_USER_APPS>();
            using (var Command = Conn.CreateCommand("SELECT * FROM CLIENT_DB_COMPANY_USER_APPS"))
            using (var reader = Command.ExecuteReader())
                while (reader.Read())
                    collection.Add(new CLIENT_DB_COMPANY_USER_APPS(reader));
            return collection;
        }

        public int DB_COMPANY_USER_APP_ID { get; set; }//	NUMBER(10)	N			
        public int DB_COMPANY_USER_ID { get; set; }//	NUMBER(10)	N			
        public int APP_ID { get; set; }//	NUMBER(10)	N			
        public int? APP_USER_TYPE_ID { get; set; }//	NUMBER(10)	Y			

        public CLIENT_DB_COMPANY_USER_APPS() : base()
        {
            PrimaryKey.Add("DB_COMPANY_USER_APP_ID");
        }

        public CLIENT_DB_COMPANY_USER_APPS(IDataReader reader) : this()
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