using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;


namespace Plex.PMH.Data.Tables
{
    public class DEVICES : PlexxisDataTransferObjects
    {
        public static event Subscriber OnInsert;
        public static event Subscriber OnUpdate;
        public static event Subscriber OnDelete;

        public static IEnumerable<DEVICES> GetAll()
        {
            throw new NotImplementedException();
        }

        public static IEnumerable<DEVICES> GetAll(IDbConnection Conn)
        {
            throw new NotImplementedException();
        }

        public int DEVICE_ID;

        public DEVICES() : base()
        {
            PrimaryKey.Add("DEVICE_ID");
        }

        public DEVICES(IDataReader reader)
        {
            AutoFill(reader, this);
        }
    }
}