using System;
using System.Collections.Generic;

namespace Plex.PMH.Objects
{
    public class QueryResult
    {
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
