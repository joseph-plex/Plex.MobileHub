using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace Plex.PMH.Data.Tables
{
    public class CLIENTS : PlexxisDataTransferObjects
    {
        //Events 
        public static event Subscriber OnInsert;
        public static event Subscriber OnUpdate;
        public static event Subscriber OnDelete;

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
            //using (var Command = new OracleCommand(, Conn))
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

        public int CLIENT_ID;			
        public string CLIENT_KEY;		
        public string DESCRIPTION;

        public CLIENTS() : base()
        {
            PrimaryKey.Add("CLIENT_ID");
        }
        public CLIENTS(IDataReader reader)
        {
            AutoFill(reader, this);
        }
    }
}