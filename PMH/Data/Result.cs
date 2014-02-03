using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plex.PMH.Data
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
                int ColumnIndex = Columns.FindIndex((p) => p.ColumnName.Equals(ColumnName, StringComparison.OrdinalIgnoreCase));
                return Rows[RowIndex].Values[ColumnIndex];
            }
        }

        public List<Col> Columns = new List<Col>();
        public List<Row> Rows = new List<Row>();
    }
}