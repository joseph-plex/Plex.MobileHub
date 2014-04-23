using System;
using System.Collections.Generic;
using System.Data;
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

        public Result()
        {
        }

        public Result(IDataReader reader)
        {
            Columns = new List<Col>(GetColumnData(reader.GetSchemaTable()));
            for (var Curr = new Row(); reader.Read(); Rows.Add(Curr), Curr = new Row())
                for (int i = 0; i < Columns.Count; i++)
                    Curr.Values.Add((reader[Columns[i].ColumnName] != DBNull.Value) ? reader[Columns[i].ColumnName] : null);
        }       
        static Col GetColumnData(DataRow Collection)
        {
            return new Col()
            {
                ColumnName = (Collection[0] != DBNull.Value) ? Convert.ToString(Collection[0]) : string.Empty,
                IsUnique = (Collection[5] != DBNull.Value) ? Convert.ToBoolean(Collection[5]) : (bool?)null,

                IsKey = (Collection[6] != DBNull.Value) ? Convert.ToBoolean(Collection[6]) : (bool?)null,
                DataType = (Collection[11] != DBNull.Value) ? Collection[11].ToString() : string.Empty,
                IsReadOnly = (Collection[18] != DBNull.Value) ? Convert.ToBoolean(Collection[18]) : (bool?)null,
                IsLong = (Collection[19] != DBNull.Value) ? Convert.ToBoolean(Collection[19]) : (bool?)null,
            };
        }
        static IEnumerable<Col> GetColumnData(DataTable dataTable)
        {
            foreach (var Row in dataTable.Rows)
                yield return GetColumnData((DataRow)Row);
        }
    }
}