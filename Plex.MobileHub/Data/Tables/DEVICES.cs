using System;
using System.Data;
using System.Collections.Generic;


namespace Plex.MobileHub.Data.Tables
{
    public class DEVICES : PlexxisDataTransferObjects
    {
        public static event Subscriber OnInsert;
        public static event Subscriber OnUpdate;
        public static event Subscriber OnDelete;

        public static IEnumerable<DEVICES> GetAll()
        {
            using (var Conn = Utilities.GetConnection(true))
                return GetAll(Conn);
        }

        public static IEnumerable<DEVICES> GetAll(IDbConnection Conn)
        {
            var collection = new List<DEVICES>();
            using (var Command = Conn.CreateCommand("SELECT * FROM DEVICES"))
            using (var reader = Command.ExecuteReader())
                while (reader.Read())
                    collection.Add(new DEVICES(reader));
            return collection;
        }

        public int DEVICE_ID { get; set; }
        public int APP_ID { get; set; }

        public DEVICES() : base()
        {
            PrimaryKey.Add("DEVICE_ID");
        }

        public DEVICES(IDataReader reader) : this()
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