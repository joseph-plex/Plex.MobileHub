using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace Plex.PMH.Objects
{
    public class DeviceSynchroData
    {
        public int UserDataId
        {
            get;
            set;
        }

        public DateTime ExecInitiation
        {
            get;
            set;
        }
        public DateTime ExecCompletion
        {
            get;
            set;
        }
        public List<QueryResult> SyncData
        {
            get;
            set;
        }

        public DeviceSynchroData()
        {
            UserDataId = new int();
            SyncData = new List<QueryResult>();
            ExecInitiation = new DateTime();
            ExecCompletion = new DateTime();
        }
    }
}