﻿using System;
using System.Data;
using System.Collections.Generic;

namespace Plex.MobileHub.Data.Tables
{
    public class CLIENT_DB_COMPANY_USERS : PlexxisDataTransferObjects
    {
        public static event EventHandler OnInsert;
        public static event EventHandler OnUpdate;
        public static event EventHandler OnDelete;


        public static IEnumerable<CLIENT_DB_COMPANY_USERS> GetAll()
        {
            using (var Conn = Utilities.GetConnection(true))
                return GetAll(Conn);
        }

        public static IEnumerable<CLIENT_DB_COMPANY_USERS> GetAll(IDbConnection Conn)
        {
            var collection = new List<CLIENT_DB_COMPANY_USERS>();
            using (var Command = Conn.CreateCommand("SELECT * FROM CLIENT_DB_COMPANY_USERS"))
            using (var reader = Command.ExecuteReader())
                while (reader.Read())
                    collection.Add(new CLIENT_DB_COMPANY_USERS(reader));
            return collection;
        }


        public int DB_COMPANY_USER_ID { get; set; }//NUMBER(10)	N			
        public int DB_COMPANY_ID { get; set; }//	NUMBER(10)	N			
        public int USER_ID { get; set; }//NUMBER(10)	N			
        public string CONNECT_AS { get; set; }//	VARCHAR2(100)	Y		

        public CLIENT_DB_COMPANY_USERS() : base()
        {
            PrimaryKey.Add("DB_COMPANY_USER_ID");
        }

        public CLIENT_DB_COMPANY_USERS(IDataReader reader) : this()
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