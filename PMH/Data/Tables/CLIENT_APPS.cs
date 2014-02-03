using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace Plex.PMH.Data.Tables
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
    }
}