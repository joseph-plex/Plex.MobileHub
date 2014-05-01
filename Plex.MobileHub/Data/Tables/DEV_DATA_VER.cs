using System;
using System.Data;
using System.Collections.Generic;


namespace Plex.MobileHub.Data.Tables
{
    public class DEV_DATA_VER : PlexxisDataTransferObjects
    {
        public static event Subscriber OnInsert;
        public static event Subscriber OnUpdate;
        public static event Subscriber OnDelete;

        public int DEV_DATA_VER_ID;//DEV_DATA_VER_ID	NUMBER(10)	N	
        public int DEV_DATA_ID;//DEV_DATA_ID	NUMBER(10)	N

        public static IEnumerable<DEV_DATA_VER> GetAll()
        {
            using (var Conn = Utilities.GetConnection(true))
                return GetAll(Conn);
        }

        public static IEnumerable<DEV_DATA_VER> GetAll(IDbConnection Conn)
        {
            var collection = new List<DEV_DATA_VER>();
            using (var Command = Conn.CreateCommand("SELECT * FROM DEV_DATA_VER"))
            using (var reader = Command.ExecuteReader())
                while (reader.Read())
                    collection.Add(new DEV_DATA_VER(reader));
            return collection;
        }
        public DEV_DATA_VER()
            : base()
        { 

        }

        public DEV_DATA_VER(IDataReader reader)
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