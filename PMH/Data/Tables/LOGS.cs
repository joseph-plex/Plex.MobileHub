using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace Plex.PMH.Data.Tables
{
    public class LOGS : PlexxisDataTransferObjects
    {
        public static event Subscriber OnInsert;
        public static event Subscriber OnUpdate;
        public static event Subscriber OnDelete;


        public static IEnumerable<LOGS> GetAll()
        {
            using (var Conn = Utilities.GetConnection(true))
                return GetAll(Conn);
        }

        public static IEnumerable<LOGS> GetAll(IDbConnection Conn)
        {
            var collection = new List<LOGS>();
            using (var Command = Conn.CreateCommand("SELECT * FROM LOGS"))
            using (var reader = Command.ExecuteReader())
                while (reader.Read())
                    collection.Add(new LOGS(reader));
            return collection;
        }

        public int LOG_ID;//NUMBER(10)	N			
        public DateTime LOG_DATE;//DATE	N			
        public string DESCRIPTION;//	VARCHAR2(2048)	N			
        public int? CLIENT_ID;//	NUMBER(10)	Y			

        public LOGS() : base()
        {
            PrimaryKey.Add("LOG_ID");
        }

        public LOGS(IDataReader reader)
        {
            AutoFill(reader, this);
        }
    }
}