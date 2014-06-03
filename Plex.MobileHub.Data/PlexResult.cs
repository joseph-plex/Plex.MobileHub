using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Plex.MobileHub.Data
{
    public class PlexResult : IPlexResult
    {
        public object this[int ColumnIndex, int RowIndex]
        {
            get
            {
                return Tuples[RowIndex].Values[ColumnIndex];
            }
        }
        public object this[string ColumnName, int RowIndex]
        {
            get
            {
                return this[GetColumnIndex(ColumnName), RowIndex];
            }
        }
        public IEnumerable<object> this[int ColumnIndex]
        {
            get 
            {
                foreach (var r in Tuples)
                    yield return r.Values[ColumnIndex];
            }
        }
        public IEnumerable<object> this[string ColumnName]
        {
            get
            {
                return this[GetColumnIndex(ColumnName)];
            }
        }

        public int GetColumnIndex(String ColumnName)
        {
            return Columns.FindIndex((p) => p.ColumnName.Equals(ColumnName, StringComparison.OrdinalIgnoreCase));
        }

        public IList<IPlexResultColumn> Columns { get; set; }
        public IList<IPlexResultTuple> Tuples { get; set; }

        public PlexResult()
        {
            Tuples = (IList<IPlexResultTuple>)new List<PlexResultTuple>();
            Columns = (IList<IPlexResultColumn>) new List<PlexResultColumn>();
        }

        public PlexResult(IDataReader reader) : this()
        {
            Columns.AddRange(PlexResultColumn.FromDataTable(reader.GetSchemaTable()));
            for (var Curr = new PlexResultTuple(); reader.Read(); Tuples.Add(Curr), Curr = new PlexResultTuple())
                for (int i = 0; i < Columns.Count; i++)
                    Curr.Values.Add((reader[Columns[i].ColumnName] != DBNull.Value) ? reader[Columns[i].ColumnName] : null);
        }
    }
}
