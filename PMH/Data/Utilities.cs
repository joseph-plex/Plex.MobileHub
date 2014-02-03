using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.DataAccess.Client;
using System.Data;

using System.Reflection;
using Plex.PMH.Objects;

namespace Plex.PMH.Data
{
    public delegate void Subscriber(Object sender, EventArgs e);

    public static class Utilities
    {
        public static String ConnectionString
        {
            get;
            private set;
        }

        static Utilities()
        {
            string User = "C##PMH";
            string Pass = "!!!plex!!!sa";
            string Source = "(DESCRIPTION = (ADDRESS_LIST = (ADDRESS = (PROTOCOL = TCP)(HOST = 10.0.1.96)(PORT = 1521)))(CONNECT_DATA = (SERVICE_NAME = PLE1LIVE)))";
            Utilities.ConnectionString = "User Id=" + User + ";" + "Password=" + Pass + ";" + "Data Source=" + Source + ";";

        }

        public static IDbConnection GetConnection(bool IsOpened = false)
        {
            var conn = new OracleConnection(ConnectionString);
            if (IsOpened) conn.Open();
            return conn;
        }

        public static IEnumerable<Type> GetTypesInNamespace(string NameSpace)
        {
            return Assembly.GetExecutingAssembly().GetTypes().Where(t => String.Equals(t.Namespace, NameSpace, StringComparison.Ordinal));
        }

        public static IEnumerable<FieldInfo> GetVariables(string FullAssemblyName)
        {
            Type t = Assembly.GetExecutingAssembly().GetType(FullAssemblyName);
            return t.GetFields();
        }

        public static int GetNextSequenceValue(SequenceType Seq)
        {
            switch (Seq)
            {
                case SequenceType.DEVICE_ID:
                    return Convert.ToInt32(Utilities.GetConnection(true).Query("select DEVICE_ID.nextval from dual").Rows.First()[0]);
                case SequenceType.ID_GEN:
                    return Convert.ToInt32(Utilities.GetConnection(true).Query("select ID_GEN.nextval from dual").Rows.First()[0]);
                default:
                    throw new NotImplementedException();
            }
        }

        public static bool AreObjectsCorrect()
        {
            var Types = GetTypesInNamespace("Plex.PMH.Data.Tables").ToList();
            using (var Conn = GetConnection(true))
            {
                Result r = GetColumnData(Conn);
                var TIndex = r.GetColumnIndex("TABLE_NAME");
                var CIndex = r.GetColumnIndex("COLUMN_NAME");

                foreach(var TableName in GetTableNames(Conn))
                {
                    var TableColumns = r.Rows.FindAll((p)=> Convert.ToString(p[TIndex]) == TableName);

                    var Index = Types.FindIndex((p)=>p.Name == TableName);
                    if(Index == -1) throw new Exception("The Table " + TableName + " Does not have a variable representation");
                  
                    foreach (var row in TableColumns)
                        if(! Types[Index].GetFields().Any((p)=>p.Name == Convert.ToString(row[CIndex])))
                            throw new Exception("The Column " + row[CIndex] + " In Table " + TableName +  " Does not have a variable representation");
                }
            }
            return true;
        }

        public static bool AreTablesCorrect()
        {
            using (var Conn = GetConnection(true))
            {
                Result r = GetColumnData(Conn);
                var TIndex = r.GetColumnIndex("TABLE_NAME");
                var CIndex = r.GetColumnIndex("COLUMN_NAME");
                foreach (var t in GetTypesInNamespace("Plex.PMH.Data.Tables"))
                {
                    var Fields = t.GetFields();
                    var TableColumns = r.Rows.FindAll((p) => Convert.ToString(p[TIndex]) == t.Name);

                    foreach (var f in Fields)
                        if (!TableColumns.Any((p) => Convert.ToString(p[CIndex]) == f.Name))
                            throw new Exception("The Field " + f.Name  + " in the type " + t.Name + " does not have a database representation");
                }
            }
            return true;
        }

        public static IEnumerable<string> GetTableNames(IDbConnection Conn)
        {
            foreach (var s in Conn.Query("select TABLE_NAME from user_tables")["TABLE_NAME"])
                yield return s.ToString();
        }

        public static Result GetColumnData(IDbConnection Conn)
        {
            return Conn.Query("select TABLE_NAME,COLUMN_NAME from user_tab_columns");
        }


    }

    public enum SequenceType
    {
        DEVICE_ID,
        ID_GEN
    }
}