using System;
using System.Data;
using System.Collections.Generic;

namespace Plex.MobileHub.Data.Tables
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

        public int USER_TYPE_ID { get; set; }//	NUMBER(10)	N			
        public int APP_ID { get; set; }//	NUMBER(10)	N			
        public string CODE { get; set; }//VARCHAR2(12)	N			
        public string DESCRIPTION { get; set; }//	VARCHAR2(50)	Y	

        public APP_USER_TYPES() : base()
        {
            PrimaryKey.Add("USER_TYPE_ID");
        }

        public APP_USER_TYPES(IDataReader reader) : this()
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