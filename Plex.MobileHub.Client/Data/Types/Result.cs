using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using MobileHubClient.PMH;
namespace MobileHubClient.Data.Types
{
    public class Result
    {
        public object this[int columnIndex, int rowIndex]
        {
            get
            {
                return Rows[rowIndex].Values[columnIndex];
            }
        }
        public object this[string columnName, int rowIndex]
        {
            get
            {
                return this[Columns.FindIndex((p) => p.ColumnName.Equals(columnName, StringComparison.OrdinalIgnoreCase)), rowIndex];
            }
        }
        public IEnumerable<object> this[int columnIndex]
        {
            get
            {
                foreach (var row in Rows)
                    yield return row.Values[columnIndex];
            }
        }
        public IEnumerable<object> this[string columnName]
        {
            get
            {
                return this[Columns.FindIndex((p) => p.ColumnName.Equals(columnName, StringComparison.OrdinalIgnoreCase))];
            }
        }

        public int GetColumnIndex(String columnName)
        {
            return Columns.FindIndex((p) => p.ColumnName.Equals(columnName, StringComparison.OrdinalIgnoreCase));
        }

        public List<Col> Columns = new List<Col>();
        public List<Row> Rows = new List<Row>();

        public static Result FromResult(RQryResult queryResult)
        {
            if (queryResult == null) throw new ArgumentException("q cannot be null");
            var r = new Result();
            foreach (var v in queryResult.Columns)
                r.Columns.Add(Col.FromColumn(v));
            foreach (var v in queryResult.Rows)
                r.Rows.Add(Row.fromPMHRow(v));
            return r;
        }

        public static RQryResult ToQueryResult(Result result)
        {
            var rows = new List<PMH.Row>();
            var cols = new List<Column>();
            foreach (var v in result.Columns)
                cols.Add(Col.ToColumn(v));
            foreach (var v in result.Rows)
                rows.Add(v.ToPMHRow());
            return new RQryResult()
            {
                Rows = rows.ToArray(),
                Columns = cols.ToArray()
            };
        }

        public RQryResult ToQueryResult()
        {
            var rows = new List<PMH.Row>();
            var cols = new List<Column>();
            foreach (var v in this.Columns)
                cols.Add(Col.ToColumn(v));
            foreach (var v in this.Rows)
                rows.Add(v.ToPMHRow());
            return new RQryResult()
            {
                Rows = rows.ToArray(),
                Columns = cols.ToArray()
            };
        }

        public override string ToString()
        {
            var stringwriter = new StringWriter();
            new XmlSerializer(this.GetType()).Serialize(stringwriter, this);
            return stringwriter.ToString();
        }
    }
}
