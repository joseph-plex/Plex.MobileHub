﻿using System;
using System.Data;
using System.Collections.Generic;

namespace Plex.MobileHub.Data.Tables
{
    public class CLIENTS : PlexxisDataTransferObjects
    {
        //Events 
        public static event EventHandler OnInsert;
        public static event EventHandler OnUpdate;
        public static event EventHandler OnDelete;

        public static IEnumerable<CLIENTS> GetAll()
        {
            using (var Conn = Utilities.GetConnection(true))
                return GetAll(Conn);
        }

        public static IEnumerable<CLIENTS> GetAll(IDbConnection Conn)
        {
            var collection = new List<CLIENTS>();
            using (var Command = Conn.CreateCommand("SELECT * FROM CLIENTS"))
            using (var reader = Command.ExecuteReader())
                while (reader.Read())
                    collection.Add(new CLIENTS(reader));
            return collection;
        }

        public IEnumerable<CLIENT_USERS> GetCLIENT_USERS()
        {
            using (var Conn = Utilities.GetConnection(true))
                return GetCLIENT_USERS(Conn);
        }

        public IEnumerable<CLIENT_USERS> GetCLIENT_USERS(IDbConnection Conn)
        {
            var collection = new List<CLIENT_USERS>();
            using (var Command = Conn.CreateCommand("SELECT * FROM CLIENT_USERS WHERE CLIENT_ID  = :a", CLIENT_ID))
            using (var reader = Command.ExecuteReader())
                while (reader.Read())
                    collection.Add(new CLIENT_USERS(reader));
            return collection;
        }

        public IEnumerable<CLIENT_APPS> GetCLIENT_APPS()
        {
            using (var Conn = Utilities.GetConnection(true))
                return GetCLIENT_APPS(Conn);
        }

        public IEnumerable<CLIENT_APPS> GetCLIENT_APPS(IDbConnection Conn)
        {
            var collection = new List<CLIENT_APPS>();
            using (var Command = Conn.CreateCommand("SELECT * FROM CLIENT_APPS  WHERE CLIENT_ID  = :a", CLIENT_ID))
            using (var reader = Command.ExecuteReader())
                while (reader.Read())
                    collection.Add(new CLIENT_APPS(reader));
            
            return collection;
        }

        public IEnumerable<CLIENT_DB_COMPANIES> GetCLIENT_DB_COMPANIES()
        {
            using (var Conn = Utilities.GetConnection(true))
                return GetCLIENT_DB_COMPANIES(Conn);
        }

        public IEnumerable<CLIENT_DB_COMPANIES> GetCLIENT_DB_COMPANIES(IDbConnection Conn)
        {  
            var collection = new List<CLIENT_DB_COMPANIES>();
            using (var Command = Conn.CreateCommand("SELECT * FROM CLIENT_DB_COMPANIES  WHERE CLIENT_ID  = :a", CLIENT_ID))
            using (var reader = Command.ExecuteReader())
                while (reader.Read())
                    collection.Add(new CLIENT_DB_COMPANIES(reader));
            
            return collection;
        }

        public int CLIENT_ID { get; set; }			
        public string CLIENT_KEY { get; set; }	
        public string DESCRIPTION { get; set; }
        public int? CLIENT_INSTANCE_ID { get; set; }
        public string CLIENT_IP_ADDRESS { get; set; }	
        public int? CLIENT_PORT { get; set; }

        public CLIENTS() : base()
        {
            PrimaryKey.Add("CLIENT_ID");
        }

        public CLIENTS(IDataReader reader) : this()
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