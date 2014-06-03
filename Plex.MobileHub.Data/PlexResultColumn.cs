using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Plex.MobileHub.Data
{
    public class PlexResultColumn : IPlexResultColumn
    {
        public static IEnumerable<PlexResultColumn> FromDataTable(DataTable dataTable)
        {
            foreach (DataRow Row in dataTable.Rows)
                yield return new PlexResultColumn(Row);
        }

        public virtual string ColumnName { get; set; }
        public virtual int? ColumnSequence { get; set; }
        public virtual string DataType { get; set; }
        public virtual int? DataLength { get; set; }
        public virtual int? DataPrecision { get; set; }
        public virtual int? DataScale { get; set; }
        public virtual bool? AllowDbNull { get; set; }
        public virtual bool? IsReadOnly { get; set; }
        public virtual bool? IsLong { get; set; }
        public virtual bool? IsKey { get; set; }
        public virtual string KeyType { get; set; }
        public virtual bool? IsUnique { get; set; }
        public virtual string Description { get; set; }

        public PlexResultColumn()
        {
        }

        public PlexResultColumn(DataRow Collection) : this()
        {
            ColumnName = (Collection[0] != DBNull.Value) ? Convert.ToString(Collection[0]) : string.Empty;
            IsUnique = (Collection[5] != DBNull.Value) ? Convert.ToBoolean(Collection[5]) : (bool?)null;
            IsKey = (Collection[6] != DBNull.Value) ? Convert.ToBoolean(Collection[6]) : (bool?)null;
            DataType = (Collection[11] != DBNull.Value) ? Collection[11].ToString() : string.Empty;
            IsReadOnly = (Collection[18] != DBNull.Value) ? Convert.ToBoolean(Collection[18]) : (bool?)null;
            IsLong = (Collection[19] != DBNull.Value) ? Convert.ToBoolean(Collection[19]) : (bool?)null;
        }

       
    }
}
