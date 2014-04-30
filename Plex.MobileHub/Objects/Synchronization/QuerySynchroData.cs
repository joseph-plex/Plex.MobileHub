using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plex.MobileHub.Objects.Synchronization
{
    public class QuerySynchroData
    {
        public int QueryId;
        public string Description;
        public string TableName;

        public List<QueryColumnSynchroData> Cols = new List<QueryColumnSynchroData>();
        public List<QueryConditionSynchroData> Conds = new List<QueryConditionSynchroData>();
    }
}