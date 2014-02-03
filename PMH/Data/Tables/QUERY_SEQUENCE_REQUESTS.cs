using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace Plex.PMH.Data.Tables
{
    public class QUERY_SEQUENCE_REQUESTS : PlexxisDataTransferObjects
    {
        public static event Subscriber OnInsert;
        public static event Subscriber OnUpdate;
        public static event Subscriber OnDelete;

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

        public int TQR_ID;//NUMBER(10)	N			
        public int USER_QUERYING;//	NUMBER(10)	Y			
        public int DATABASE_QUERIED;//	NUMBER(10)	Y			
        public int QUERY_SEQUENCING;//NUMBER(10)	Y			
        public DateTime SEQ_QUERY_TIME;//DATE	Y			

        public QUERY_SEQUENCE_REQUESTS() : base()
        {
            PrimaryKey.Add("TQR_ID");
        }

        public QUERY_SEQUENCE_REQUESTS(IDataReader reader)
        {
            AutoFill(reader, this);
        }
    }
}