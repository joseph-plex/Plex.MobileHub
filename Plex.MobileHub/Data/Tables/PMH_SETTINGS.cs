using System;
using System.Data;
using System.Collections.Generic;

namespace Plex.MobileHub.Data.Tables
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

            public PMH_SETTINGS(IDataReader reader):this()
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