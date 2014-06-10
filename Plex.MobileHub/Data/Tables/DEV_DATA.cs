using System;
using System.Data;
using System.Collections.Generic;

namespace Plex.MobileHub.Data.Tables
{
    public class DEV_DATA : PlexxisDataTransferObjects
    {
        public static event EventHandler OnInsert;
        public static event EventHandler OnUpdate;
        public static event EventHandler OnDelete;
        public static IEnumerable<DEV_DATA> GetAll()
        {
            using (var Conn = Utilities.GetConnection(true))
                return GetAll(Conn);
        }

        public static IEnumerable<DEV_DATA> GetAll(IDbConnection Conn)
        {
            var collection = new List<DEV_DATA>();
            using (var Command = Conn.CreateCommand("SELECT * FROM DEV_DATA"))
            using (var reader = Command.ExecuteReader())
                while (reader.Read())
                    collection.Add(new DEV_DATA(reader));
            return collection;
        }
        public DEV_DATA()
            : base()
        { 

        }

        public int DEVICE_DATABASE_ID { get; set; } //DEVICE_DATABASE_ID	NUMBER(10)	N
        public int USER_ID { get; set; } //USER_ID	NUMBER(10)	N	
        public int APP_ID { get; set; } //APP_ID	NUMBER(10)	N	
        public int CLIENT_ID { get; set; } //CLIENT_ID	NUMBER(10)	N		

        public DEV_DATA(IDataReader reader)
            : this()
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