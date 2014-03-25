using System;
using System.Collections.Generic;

namespace MobileHub.Data.Types
{
    public class Result
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
                return this[Columns.FindIndex((p) => p.ColumnName.Equals(ColumnName, StringComparison.OrdinalIgnoreCase)), RowIndex];
            }
        }
        public IEnumerable<object> this[int ColumnIndex]
        {
            get 
            {
                foreach (var row in Rows)
                    yield return row.Values[ColumnIndex];
            }
        }
        public IEnumerable<object> this[string ColumnName]
        {
            get
            {
                return this[Columns.FindIndex((p) => p.ColumnName.Equals(ColumnName, StringComparison.OrdinalIgnoreCase))];
            }
        }

        public int GetColumnIndex(String ColumnName)
        {
            return Columns.FindIndex((p) => p.ColumnName.Equals(ColumnName, StringComparison.OrdinalIgnoreCase));
        }

        public List<Col> Columns = new List<Col>();
        public List<Row> Rows = new List<Row>();
    }
}