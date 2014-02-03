using System;
using System.Data;
using System.Collections.Generic;

namespace Plex.PMH.Data.Tables
{
    public class CLIENT_USERS : PlexxisDataTransferObjects
    {
        public static event Subscriber OnInsert;
        public static event Subscriber OnUpdate;
        public static event Subscriber OnDelete;


        public static IEnumerable<CLIENT_USERS> GetAll()
        {
            using (var Conn = Utilities.GetConnection(true))
                return GetAll(Conn);
        }

        public static IEnumerable<CLIENT_USERS> GetAll(IDbConnection Conn)
        {
            var collection = new List<CLIENT_USERS>();
            using (var Command = Conn.CreateCommand("SELECT * FROM CLIENT_USERS"))
            using (var reader = Command.ExecuteReader())
                while (reader.Read())
                    collection.Add(new CLIENT_USERS(reader));
            return collection;
        }

        public IEnumerable<CLIENT_DB_COMPANY_USERS> GetCLIENT_DB_COMPANY_USERS()
        {
            using (var Conn = Utilities.GetConnection(true))
                return GetCLIENT_DB_COMPANY_USERS(Conn);
        }
        public IEnumerable<CLIENT_DB_COMPANY_USERS> GetCLIENT_DB_COMPANY_USERS(IDbConnection Conn)
        {
            var collection = new List<CLIENT_DB_COMPANY_USERS>();
            using (var Command = Conn.CreateCommand("SELECT * FROM CLIENT_DB_COMPANY_USERS WHERE CLIENT_ID = :a", CLIENT_ID))
            using (var reader = Command.ExecuteReader())
                while (reader.Read())
                    collection.Add(new CLIENT_DB_COMPANY_USERS(reader));
            return collection;
        }


        public int USER_ID;//	NUMBER(10)	N			
        public int CLIENT_ID;//NUMBER(10)	N			
        public string NAME;//	VARCHAR2(25)	N			
        public string PASSWORD;//VARCHAR2(25)	Y			

        public CLIENT_USERS() : base()
        {
            PrimaryKey.Add("USER_ID");
        }

        public CLIENT_USERS(IDataReader reader)
        {
            AutoFill(reader, this);
        }

        public override bool Insert(IDbConnection Conn)
        {
            var r = base.Insert();
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