using System;
using System.Data;
using System.Collections.Generic;

namespace Plex.MobileHub.Data.Tables
{
    public class DEVICE_USER_DATA : PlexxisDataTransferObjects
    {

        public static event Subscriber OnInsert;
        public static event Subscriber OnUpdate;
        public static event Subscriber OnDelete;

        public static IEnumerable<DEVICE_USER_DATA> GetAll()
        {
            using (var Conn = Utilities.GetConnection(true))
                return GetAll(Conn);
        }

        public static IEnumerable<DEVICE_USER_DATA> GetAll(IDbConnection Conn)
        {
            var collection = new List<DEVICE_USER_DATA>();
            using (var Command = Conn.CreateCommand("SELECT * FROM DEVICE_USER_DATA"))
            using (var reader = Command.ExecuteReader())
                while (reader.Read())
                    collection.Add(new DEVICE_USER_DATA(reader));
            return collection;
        }

        public int USER_DATA_ID { get; set; }
        public int? DEVICE_ID { get; set; }
        public int USER_PERMISSION { get; set; }
        public DateTime? EXECUTION_INITIATION { get; set; }
        public DateTime? EXECUTION_COMPLETION { get; set; }

        public DEVICE_USER_DATA() : base()
        {
            PrimaryKey.Add("USER_DATA_ID");
        }

        public DEVICE_USER_DATA(IDataReader reader):this()
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