using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Plex.MobileHub.Data
{
    public class PlexQueryResult : IPlexQueryResult<PlexQueryResultColumn, PlexQueryResultTuple>
    {
        public Object this[Int32 ColumnIndex, Int32 RowIndex]
        {
            get
            {
                return Tuples[RowIndex].Values[ColumnIndex];
            }
        }
        public Object this[String ColumnName, Int32 RowIndex]
        {
            get
            {
                return this[GetColumnIndex(ColumnName), RowIndex];
            }
        }
        public IEnumerable<Object> this[Int32 ColumnIndex]
        {
            get
            {
                foreach (var row in Tuples)
                    yield return row.Values[ColumnIndex];
            }
        }
        public IEnumerable<Object> this[String ColumnName]
        {
            get
            {
                return this[GetColumnIndex(ColumnName)];
            }
        }
     
        public virtual IList<PlexQueryResultColumn> Columns { get; set; }
        public virtual IList<PlexQueryResultTuple> Tuples { get; set; }

        #region Constructors
        public PlexQueryResult()
        {
            Columns = new List<PlexQueryResultColumn>();
            Tuples = new List<PlexQueryResultTuple>();
        }
        public PlexQueryResult(IDataReader reader) : this()
        {
            var schemaTable = reader.GetSchemaTable();
            foreach (var column in GetColumnData(schemaTable))
                Columns.Add(column);

            for (var Curr = new PlexQueryResultTuple() { parent = this }; reader.Read(); Tuples.Add(Curr), Curr = new PlexQueryResultTuple() { parent = this })
                for (int i = 0; i < Columns.Count; i++)
                    Curr.Values.Add((reader[Columns[i].ColumnName] != DBNull.Value) ? reader[Columns[i].ColumnName] : null);
        }
        #endregion

        public int GetColumnIndex(String ColumnName)
        {
            //This can be optimized but was done in the interest of fast coding and readability
            return Columns.IndexOf(Columns.First((p) => p.ColumnName.Equals(ColumnName, StringComparison.OrdinalIgnoreCase)));
        }

        IEnumerable<PlexQueryResultColumn> GetColumnData(DataTable dataTable)
        {
            for (int i = 0; i < dataTable.Rows.Count; i++)
                yield return new PlexQueryResultColumn((DataRow)dataTable.Rows[i]);
        }
    }
}
