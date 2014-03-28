using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MobileHub.Objects
{
    public class TableDefinition
    {
        public string TableName;

        public bool AllowInsert;
        public bool AllowDelete;
        public bool AllowUpdate;
        public bool AllowQuery;

        public List<Column> Columns = new List<Column>();
    }
}