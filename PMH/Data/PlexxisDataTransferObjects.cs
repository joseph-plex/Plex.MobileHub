using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Client.SqlGen;
namespace Plex.PMH.Data
{
    public abstract class PlexxisDataTransferObjects : IPlexxisDatabaseRow
    {
        //Constructors
        public PlexxisDataTransferObjects() { }

        //Methods
        public abstract bool Insert(OracleConnection Conn);
        public abstract bool Update(OracleConnection Conn);
        public abstract bool Delete(OracleConnection Conn);

        public bool Insert()
        {
            using (var Conn = Utilities.GetConnection(true))
                return Insert(Conn);
        }

        public bool Update()
        {
            using (var Conn = Utilities.GetConnection(true))
                return Update(Conn);
        }
        
        public bool Delete()
        {
            using (var Conn = Utilities.GetConnection(true))
                return Delete(Conn);
        }

        protected static OracleDbType ResolveType(object o)
        {
            if (o is string)
                return OracleDbType.Varchar2;
            if (o is DateTime || o is DateTime?)
                return OracleDbType.Date;
            if (o is Int64 || o is Int64?)
                return OracleDbType.Int64;
            if (o is Int32 || o is Int32? || o is bool || o is bool?)
                return OracleDbType.Int32;
            if (o is Int16 || o is Int16?)
                return OracleDbType.Int16;
            if (o is byte || o is byte?)
                return OracleDbType.Byte;
            if (o is decimal || o is decimal?)
                return OracleDbType.Decimal;
            if (o is float || o is float?)
                return OracleDbType.Single;
            if (o is double || o is double?)
                return OracleDbType.Double;
            if (o is byte[])
                return OracleDbType.Blob;
            return OracleDbType.Varchar2;
        }

    }
}