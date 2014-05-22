using System;
using System.Data;
using System.Collections.Generic;

namespace Plex.MobileHub.Data.Tables
{
    public class CLIENT_DB_COMPANIES : PlexxisDataTransferObjects
    {
        public static event Subscriber OnInsert;
        public static event Subscriber OnUpdate;
        public static event Subscriber OnDelete;

        public static IEnumerable<CLIENT_DB_COMPANIES> GetAll()
        {
            using (var Conn = Utilities.GetConnection(true))
                return GetAll(Conn);
        }

        public static IEnumerable<CLIENT_DB_COMPANIES> GetAll(IDbConnection Conn)
        {
            var collection = new List<CLIENT_DB_COMPANIES>();
            using (var Command = Conn.CreateCommand("SELECT * FROM CLIENT_DB_COMPANIES"))
            using (var reader = Command.ExecuteReader())
                while (reader.Read())
                    collection.Add(new CLIENT_DB_COMPANIES(reader));
        
            return collection;
        }

        public IEnumerable<CLIENT_DB_COMPANY_USERS> GetCLIENT_DB_COMPANY_USERS()
        {
            using (var Conn = Utilities.GetConnection(true))
                return GetCLIENT_DB_COMPANY_USERS(Conn);
        }
        public IEnumerable<CLIENT_DB_COMPANY_USERS> GetCLIENT_DB_COMPANY_USERS(IDbConnection Conn)
        {
            var collection = new List<CLIENT_DB_COMPANY_USERS>();
            using (var Command = Conn.CreateCommand("SELECT * FROM CLIENT_DB_COMPANY_USERS WHERE DB_COMPANY_ID = :a",DB_COMPANY_ID))
            using (var reader = Command.ExecuteReader())
                while (reader.Read())
                    collection.Add(new CLIENT_DB_COMPANY_USERS(reader));
            return collection;
        }

        public int DB_COMPANY_ID { get; set; }//	NUMBER(10)	N			
        public string DATABASE_SID { get; set; }//	VARCHAR2(25)	Y			
        public int? COMPANY_ID { get; set; }//	NUMBER(10)	Y			
        public string COMPANY_CODE { get; set; }//	VARCHAR2(12)	Y			
        public int CLIENT_ID { get; set; }//	NUMBER(10)	N			

        public CLIENT_DB_COMPANIES() : base()
        {
            PrimaryKey.Add("DB_COMPANY_ID");
        }

        public CLIENT_DB_COMPANIES(IDataReader reader) : this()
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