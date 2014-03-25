using System;
using System.Data;
using System.Collections.Generic;

namespace MobileHub.Data.Tables
{
    public class APP_QUERIES : PlexxisDataTransferObjects
    {
        public static event Subscriber OnInsert;
        public static event Subscriber OnUpdate;
        public static event Subscriber OnDelete;

        public static IEnumerable<APP_QUERIES> GetAll()
        {
            using (var Conn = Utilities.GetConnection(true))
                return GetAll(Conn);
        }

        public static IEnumerable<APP_QUERIES> GetAll(IDbConnection Conn)
        {
            List<APP_QUERIES> collection = new List<APP_QUERIES>();
            using (var Command = Conn.CreateCommand("SELECT * FROM APP_QUERIES"))
            using (var reader = Command.ExecuteReader())
                while (reader.Read())
                    collection.Add(new APP_QUERIES(reader));
            return collection;
        }

        public IEnumerable<APP_QUERY_COLUMNS> GetAPP_QUERY_COLUMNS()
        {
            using (var Conn = Utilities.GetConnection(true))
                return GetAPP_QUERY_COLUMNS(Conn);
        }

        public IEnumerable<APP_QUERY_COLUMNS> GetAPP_QUERY_COLUMNS(IDbConnection Conn)
        {
            List<APP_QUERY_COLUMNS> collection = new List<APP_QUERY_COLUMNS>();
            using (var Command = Conn.CreateCommand("SELECT * FROM APP_QUERY_COLUMNS WHERE QUERY_ID = :a", QUERY_ID))
            using (var reader = Command.ExecuteReader())
                while (reader.Read())
                    collection.Add(new APP_QUERY_COLUMNS(reader));
            return collection;
        }

        public IEnumerable<APP_QUERY_CONDITIONS> GetAPP_QUERY_CONDITIONS()
        {
            using (var Conn = Utilities.GetConnection(true))
                return GetAPP_QUERY_CONDITIONS(Conn);
        }

        public IEnumerable<APP_QUERY_CONDITIONS> GetAPP_QUERY_CONDITIONS(IDbConnection Conn)
        {
            List<APP_QUERY_CONDITIONS> collection = new List<APP_QUERY_CONDITIONS>();
            using (var Command = Conn.CreateCommand("SELECT * FROM APP_QUERY_CONDITIONS WHERE QUERY_ID = :a", QUERY_ID))
            using (var reader = Command.ExecuteReader())
                while (reader.Read())
                    collection.Add(new APP_QUERY_CONDITIONS(reader));
            return collection;
        }

        public int QUERY_ID;//NUMBER(10)	N			
        public int APP_ID;//NUMBER(10)	N			
        public string NAME;//VARCHAR2(50)	N			
        public string DESCRIPTION;//VARCHAR2(4000)	Y			
        public int TABLE_ID;//NUMBER(10)	N			
        public int IS_DELTA;//NUMBER(1)	N			
        public string SQL;//VARCHAR2(4000)	N			
        public int? SEQ_LIMIT_TIMESPAN;//NUMBER(10)	Y			
        public int? SEQ_LIMIT;//NUMBER(10)	Y		

        public APP_QUERIES() : base()
        {
            PrimaryKey.Add("QUERY_ID");
        }

        public APP_QUERIES(IDataReader reader) : this()
        {
            AutoFill(reader, this);
        }

        public override bool Insert(IDbConnection Conn)
        {
            var r = base.Insert(Conn);
            if(OnInsert!= null)
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