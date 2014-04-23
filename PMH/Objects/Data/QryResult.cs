using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MobileHub.Objects
{
    public class QryResult : MethodResult
    {
        public DateTime StartTimeStamp;
        public DateTime CompletionTimeStamp;

        public int DBErrorCode;
        public string DBErrorMessage;

        public object this[int ColumnIndex, int RowIndex]
        {
            get
            {
                return Rows[RowIndex].Values[ColumnIndex];
            }
        }
        public object this[string ColumnName, int RowIndex]
        {
            get
            {
                int ColumnIndex = Columns.FindIndex((p) => p.ColumnName.Equals(ColumnName, StringComparison.OrdinalIgnoreCase));
                return Rows[RowIndex].Values[ColumnIndex];
            }
        }

        public List<Column> Columns = new List<Column>();
        public List<Row> Rows = new List<Row>();
    }
}