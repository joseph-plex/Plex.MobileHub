using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plex.PMH.Objects
{
    public class QueryDefinition
    {
        public String QueryName;
        public String TableName;
        public Boolean TrackDelta;
        public List<String> ColumnNames = new List<String>();
        public List<Condition> WhereClauses = new List<Condition>();
        public String Sql;
        public String Description;
    }
}