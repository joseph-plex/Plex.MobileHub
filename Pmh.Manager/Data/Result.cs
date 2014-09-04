using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using Pmh.Manager.ManagementWebservice;

namespace Pmh.Manager.Data
{
    using Res = ManagementWebservice.Result;
    public class Result
    {
        #region Static Methods
        public static Result FromQueryResult(Res queryResult)
        {
            if (queryResult == null)
                throw new ArgumentException("q cannot be null");

            var r = new Result();

            foreach (var v in queryResult.Columns)
                r.Columns.Add(Column.ToColumn(v));

            foreach (var v in queryResult.Rows)
                r.Rows.Add(Tuple.FromRow(v));
            return r;
        }

        public static Res ToQueryResult(Result result)
        {
            var rows = new List<ManagementWebservice.Tuple>();
            var cols = new List<Col>();

            foreach (var v in result.Columns)
                cols.Add(Column.FromColumn(v));

            foreach (var v in result.Rows)
                rows.Add(v.ToRow());

            return new Res()
            {
                Rows = rows.ToArray(),
                Columns = cols.ToArray()
            };
        }
        #endregion

        #region Indexers
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

        #endregion

        #region Properties
        public List<Column> Columns{ get; set; }
        public List<Tuple> Rows { get; set; }
        #endregion

        #region Constructors
        public Result()
        {
            Columns = new List<Column>();
            Rows = new List<Tuple>();
        }
        #endregion

        #region Public Method
        public int GetColumnIndex(String columnName)
        {
            return Columns.FindIndex((p) => p.ColumnName.Equals(columnName, StringComparison.OrdinalIgnoreCase));
        }

        public Res ToQueryResult()
        {
            return ToQueryResult(this);
        }

        public override string ToString()
        {
            var stringwriter = new StringWriter();
            new XmlSerializer(this.GetType()).Serialize(stringwriter, this);
            return stringwriter.ToString();
        }

        #endregion
    }
}
