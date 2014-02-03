    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Data;

    namespace Plex.PMH.Data.Tables
    {
        public class PMH_SETTINGS : PlexxisDataTransferObjects
        {
            public static event Subscriber OnInsert;
            public static event Subscriber OnUpdate;
            public static event Subscriber OnDelete;

            public int PMH_ID;//	NUMBER(10)	N			

            public static IEnumerable<PMH_SETTINGS> GetAll()
            {
                using (var Conn = Utilities.GetConnection(true))
                    return GetAll(Conn);
            }

            public static IEnumerable<PMH_SETTINGS> GetAll(IDbConnection Conn)
            {
                var collection = new List<PMH_SETTINGS>();
                using (var Command = Conn.CreateCommand("SELECT * FROM PMH_SETTINGS"))
                using (var reader = Command.ExecuteReader())
                    while (reader.Read())
                        collection.Add(new PMH_SETTINGS(reader));
                return collection;
            }

            public PMH_SETTINGS() : base()
            {
                PrimaryKey.Add("PMH_ID");
            }

            public PMH_SETTINGS(IDataReader reader)
            {
                AutoFill(reader, this);
            }
        }
    }