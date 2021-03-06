﻿using System;
using System.Collections.Generic;
using System.Data;

namespace Plex.MobileHub.Data.Tables
{
    public class LOGS : PlexxisDataTransferObjects
    {
        public static event EventHandler OnInsert;
        public static event EventHandler OnUpdate;
        public static event EventHandler OnDelete;


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

        public int LOG_ID { get; set; }//NUMBER(10)	N			
        public DateTime LOG_DATE { get; set; }//DATE	N			
        public string DESCRIPTION { get; set; }//	VARCHAR2(2048)	N			
        public int? CLIENT_ID { get; set; }//	NUMBER(10)	Y			

        public LOGS() : base()
        {
            PrimaryKey.Add("LOG_ID");
        }

        public LOGS(IDataReader reader) : this()
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