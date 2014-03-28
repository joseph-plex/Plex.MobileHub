using System;
using System.Data;
using System.Collections.Generic;

namespace MobileHub.Data.Tables
{
    public class DEVICE_USER_DATA_QUERIES : PlexxisDataTransferObjects 
    {
        public static event Subscriber OnInsert;
        public static event Subscriber OnUpdate;
        public static event Subscriber OnDelete;


        public static IEnumerable<DEVICE_USER_DATA_QUERIES> GetAll()
        {
            using (var Conn = Utilities.GetConnection(true))
                return GetAll(Conn);
        }

        public static IEnumerable<DEVICE_USER_DATA_QUERIES> GetAll(IDbConnection Conn)
        {
            var collection = new List<DEVICE_USER_DATA_QUERIES>();
            using (var Command = Conn.CreateCommand("SELECT * FROM DEVICE_USER_DATA_QUERIES"))
            using (var reader = Command.ExecuteReader())
                while (reader.Read())
                    collection.Add(new DEVICE_USER_DATA_QUERIES(reader));
            return collection;
        }

        public int DEVICE_USER_DATA_QUERIES_ID;
        public DateTime QUERY_EXECUTION_TIME;		
        public int DEVICE_USER_DATA_ID;				
        public int QUERY_ID;

        public DEVICE_USER_DATA_QUERIES() : base()
        {
            PrimaryKey.Add("DEVICE_USER_DATA_QUERIES_ID");
        }
        public DEVICE_USER_DATA_QUERIES(IDataReader reader) : this()
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