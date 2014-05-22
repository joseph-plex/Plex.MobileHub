using System;
using System.Data;
using System.Collections.Generic;

namespace Plex.MobileHub.Data.Tables
{
    public class DEV_DATA_VER_QUERIES : PlexxisDataTransferObjects
    {
        public static event Subscriber OnInsert;
        public static event Subscriber OnUpdate;
        public static event Subscriber OnDelete;
        public static IEnumerable<DEV_DATA_VER_QUERIES> GetAll()
        {
            using (var Conn = Utilities.GetConnection(true))
                return GetAll(Conn);
        }

        public static IEnumerable<DEV_DATA_VER_QUERIES> GetAll(IDbConnection Conn)
        {
            var collection = new List<DEV_DATA_VER_QUERIES>();
            using (var Command = Conn.CreateCommand("SELECT * FROM DEV_DATA_VER_QUERIES"))
            using (var reader = Command.ExecuteReader())
                while (reader.Read())
                    collection.Add(new DEV_DATA_VER_QUERIES(reader));
            return collection;
        }

        public int DATABASE_VERSION_ID { get; set; }//DATABASE_VERSION_ID	NUMBER(10)	N
        public int QUERY_ID { get; set; }//QUERY_ID	NUMBER(10)	N		
        public DateTime QUERY_EXECUTION_TIME { get; set; }//QUERY_EXECUTION_TIME	DATE	N

        public DEV_DATA_VER_QUERIES()
            : base()
        { 

        }

        public DEV_DATA_VER_QUERIES(IDataReader reader)
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