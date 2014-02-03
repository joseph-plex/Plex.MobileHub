﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace Plex.PMH.Data.Tables
{
    public class APPS : PlexxisDataTransferObjects
    {
        public static event Subscriber OnInsert;
        public static event Subscriber OnUpdate;
        public static event Subscriber OnDelete;

        public static IEnumerable<APPS> GetAll()
        {
            using (var Conn = Utilities.GetConnection(true))
                return GetAll(Conn);
        }
        public static IEnumerable<APPS> GetAll(IDbConnection Conn)
        {
            var collection = new List<APPS>();
            using (var Command = Conn.CreateCommand("SELECT * FROM APPS"))
            using (var reader = Command.ExecuteReader())
                while (reader.Read())
                    collection.Add(new APPS(reader));
            return collection;
        }

        public IEnumerable<APP_QUERIES> GetAPP_QUERIES()
        {
            using (var Conn = Utilities.GetConnection(true))
                return GetAPP_QUERIES(Conn);
        }
        public IEnumerable<APP_QUERIES> GetAPP_QUERIES(IDbConnection Conn)
        {
            var collection = new List<APP_QUERIES>();
            using (var Command = Conn.CreateCommand("SELECT * FROM APP_QUERIES where APP_ID = :a", APP_ID))
            using (var reader = Command.ExecuteReader())
                while (reader.Read())
                    collection.Add(new APP_QUERIES(reader));
            return collection;
        }

        public IEnumerable<APP_USER_TYPES> GetAPP_USER_TYPES()
        {
            using (var Conn = Utilities.GetConnection(true))
                return GetAPP_USER_TYPES(Conn);
        }
        public IEnumerable<APP_USER_TYPES> GetAPP_USER_TYPES(IDbConnection Conn)
        {
            var collection = new List<APP_USER_TYPES>();
            using (var Command = Conn.CreateCommand("SELECT * FROM APP_USER_TYPES where APP_ID = :a", APP_ID))
            using (var reader = Command.ExecuteReader())
                while (reader.Read())
                    collection.Add(new APP_USER_TYPES(reader));
            return collection;

        }

        public IEnumerable<APP_TABLES> GetAPP_TABLES()
        {
            using (var Conn = Utilities.GetConnection(true))
                return GetAPP_TABLES(Conn);
        }
        public IEnumerable<APP_TABLES> GetAPP_TABLES(IDbConnection Conn)
        {
            var collection = new List<APP_TABLES>();
            using (var Command = Conn.CreateCommand("SELECT * FROM APP_TABLES where APP_ID = :a", APP_ID))
            using (var reader = Command.ExecuteReader())
                while (reader.Read())
                    collection.Add(new APP_TABLES(reader));
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
            using (var Command = Conn.CreateCommand("SELECT * FROM CLIENT_APPS where APP_ID = :a", APP_ID))
            using (var reader = Command.ExecuteReader())
                while (reader.Read())
                    collection.Add(new CLIENT_APPS(reader));
            return collection;
        }

        public IEnumerable<CLIENT_DB_COMPANY_USER_APPS> GetCLIENT_DB_COMPANY_USER_APPS()
        {
            using (var Conn = Utilities.GetConnection(true))
                return GetCLIENT_DB_COMPANY_USER_APPS(Conn);
        }
        public IEnumerable<CLIENT_DB_COMPANY_USER_APPS> GetCLIENT_DB_COMPANY_USER_APPS(IDbConnection Conn)
        {
            var collection = new List<CLIENT_DB_COMPANY_USER_APPS>();
            using (var Command = Conn.CreateCommand("SELECT * FROM CLIENT_DB_COMPANY_USER_APPS where APP_ID = :a", APP_ID))
            using (var reader = Command.ExecuteReader())
                while (reader.Read())
                    collection.Add(new CLIENT_DB_COMPANY_USER_APPS(reader));
            return collection;
        }

        public int APP_ID;
        public string AUTH_KEY;
        public string TITLE;
        public string DESCRIPTION;
        public int IS_CLIENT_CUSTOM_APP;

        public APPS() : base() 
        {
            PrimaryKey.Add("APP_ID");
        }
        public APPS(IDataReader reader)
        {
            AutoFill(reader, this);
        }
    }
}