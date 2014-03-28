using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MobileHub.Objects
{
    public class Column
    {
        public string ColumnName;
        public int? ColumnSequence;
        public string DataType;
        public int? DataLength;
        public int? DataPrecision;
        public int? DataScale;
        public bool? AllowDbNull;
        public bool? IsReadOnly;
        public bool? IsLong;
        public bool? IsKey;
        public string KeyType;
        public bool? IsUnique;
        public string Description;
    }
}
