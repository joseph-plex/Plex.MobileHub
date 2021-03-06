﻿using System;
using System.Data;
using System.Collections.Generic;

namespace Plex.MobileHub.Data.Tables
{
    public class APP_TABLES : PlexxisDataTransferObjects
    {
        public static event EventHandler OnInsert;
        public static event EventHandler OnUpdate;
        public static event EventHandler OnDelete;

        public static IEnumerable<APP_TABLES> GetAll()
        {
            using (var Conn = Utilities.GetConnection(true))
                return GetAll(Conn);
        }
        public static IEnumerable<APP_TABLES> GetAll(IDbConnection Conn)
        {
            var collection = new List<APP_TABLES>();
            using (var Command = Conn.CreateCommand("SELECT * FROM APP_TABLES"))
            using (var reader = Command.ExecuteReader())
                while (reader.Read())
                    collection.Add(new APP_TABLES(reader));
            return collection;
        }
        public IEnumerable<APP_TABLE_COLUMNS> GetAPP_TABLE_COLUMNS()
        {
            using (var Conn = Utilities.GetConnection(true))
                return GetAPP_TABLE_COLUMNS(Conn);
        }
        public IEnumerable<APP_TABLE_COLUMNS> GetAPP_TABLE_COLUMNS(IDbConnection Conn)
        {
            var collection = new List<APP_TABLE_COLUMNS>();
            using (var Command = Conn.CreateCommand("SELECT * FROM APP_TABLE_COLUMNS WHERE TABLE_ID = :a ", TABLE_ID))
            using (var reader = Command.ExecuteReader())
                while (reader.Read())
                    collection.Add(new APP_TABLE_COLUMNS(reader));
            return collection;
        }


        public int TABLE_ID { get; set; }//NUMBER(10)	N			
        public int APP_ID { get; set; }//	NUMBER(10)	N			
        public string NAME { get; set; }//	VARCHAR2(50)	N			
        public string DESCRIPTION { get; set; }//	VARCHAR2(4000)	Y			
        public int INSERT_ALLOWED { get; set; }//	NUMBER(1)	Y			
        public int UPDATE_ALLOWED { get; set; }//	NUMBER(1)	Y			
        public int DELETE_ALLOWED { get; set; }//	NUMBER(1)	Y			
        public int QUERY_ALLOWED { get; set; }//	NUMBER(1)	Y	

        public APP_TABLES() : base()
        {
            PrimaryKey.Add("TABLE_ID");
        }

        public APP_TABLES(IDataReader reader) : this()
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