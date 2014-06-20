using System;
using System.Data;
using System.Collections.Generic;

namespace Plex.MobileHub.Data.Tables
{
    public class QUERY_SEQUENCE_REQUESTS : PlexxisDataTransferObjects
    {
        public static event EventHandler OnInsert;
        public static event EventHandler OnUpdate;
        public static event EventHandler OnDelete;

        public static IEnumerable<QUERY_SEQUENCE_REQUESTS> GetAll()
        {
            using (var Conn = Utilities.GetConnection(true))
                return GetAll(Conn);
        }

        public static IEnumerable<QUERY_SEQUENCE_REQUESTS> GetAll(IDbConnection Conn)
        {
            var collection = new List<QUERY_SEQUENCE_REQUESTS>();
            using (var Command = Conn.CreateCommand("SELECT * FROM QUERY_SEQUENCE_REQUESTS"))
            using (var reader = Command.ExecuteReader())
                while (reader.Read())
                    collection.Add(new QUERY_SEQUENCE_REQUESTS(reader));
            return collection;
        }

        public int TQR_ID { get; set; }//NUMBER(10)	N			
        public int USER_QUERYING { get; set; }//	NUMBER(10)	Y			
        public int DATABASE_QUERIED { get; set; }//	NUMBER(10)	Y			
        public int QUERY_SEQUENCING { get; set; }//NUMBER(10)	Y			
        public DateTime SEQ_QUERY_TIME { get; set; }//DATE	Y			

        public QUERY_SEQUENCE_REQUESTS() : base()
        {
            PrimaryKey.Add("TQR_ID");
        }

        public QUERY_SEQUENCE_REQUESTS(IDataReader reader) : this()
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