﻿using System;
using System.Data;
using System.Collections.Generic;

namespace Plex.MobileHub.Data.Tables
{
    public class APP_QUERY_COLUMNS : PlexxisDataTransferObjects
    {
        public static event EventHandler OnInsert;
        public static event EventHandler OnUpdate;
        public static event EventHandler OnDelete;

        public static IEnumerable<APP_QUERY_COLUMNS> GetAll()
        {
            using (var Conn = Utilities.GetConnection(true))
                return GetAll(Conn);
        }

        public static IEnumerable<APP_QUERY_COLUMNS> GetAll(IDbConnection Conn)
        {
            var collection = new List<APP_QUERY_COLUMNS>();
            using (var Command = Conn.CreateCommand("SELECT * FROM APP_QUERY_COLUMNS"))
            using (var reader = Command.ExecuteReader())
                while (reader.Read())
                    collection.Add(new APP_QUERY_COLUMNS(reader));
            return collection;
        }

        public int COLUMN_ID { get; set; }//	NUMBER(10)	N			
        public int QUERY_ID { get; set; }//	NUMBER(10)	N			
        public string COLUMN_NAME { get; set; }//	VARCHAR2(50)	N			
        public int COLUMN_SEQUENCE { get; set; }//	NUMBER(3)	N	

        public APP_QUERY_COLUMNS(): base()
        {
            PrimaryKey.Add("COLUMN_ID");
        }

        public APP_QUERY_COLUMNS(IDataReader reader): this()
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