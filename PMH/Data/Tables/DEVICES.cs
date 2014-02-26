using System;
using System.Data;
using System.Collections.Generic;


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