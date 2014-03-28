using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MobileHub.Objects
{
    public class IUDData
    {
        public String TableName;
        public List<String> ColumnNames = new List<String>();
        public List<Row> Rows = new List<Row>();
    }
}