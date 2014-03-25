using System;
using System.Data;
using System.Collections.Generic;

namespace MobileHub.Data.Tables
{
    public class CLIENT_APPS : PlexxisDataTransferObjects
    {
        public static event Subscriber OnInsert;
        public static event Subscriber OnUpdate;
        public static event Subscriber OnDelete;

        public int CLIENT_APP_ID;//	NUMBER(10)	N			
        public int APP_ID;//	NUMBER(10)	N			
        public int CLIENT_ID;//	NUMBER(10)	N	

        public static IEnumerable<CLIENT_APPS> GetAll()
        {
            using (var Conn = Utilities.GetConnection(true))
                return GetAll(Conn);
        }

        public static IEnumerable<CLIENT_APPS> GetAll(IDbConnection Conn)
        {
            var collection = new List<CLIENT_APPS>();
            using (var Command = Conn.CreateCommand("SELECT * FROM CLIENT_APPS"))
            using (var reader = Command.ExecuteReader())
                while (reader.Read())
                    collection.Add(new CLIENT_APPS(reader));
            return collection;
        }

        public CLIENT_APPS() : base()
        {
            PrimaryKey.Add("CLIENT_APP_ID");
        }

        public CLIENT_APPS(IDataReader reader)
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