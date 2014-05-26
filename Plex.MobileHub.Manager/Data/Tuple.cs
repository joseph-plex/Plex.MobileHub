using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Plex.MobileHub.Manager.ManagementWebservice;

namespace Plex.MobileHub.Manager.Data
{
    public class Tuple
    {
        public object this[int i]
        {
            get
            {
                return Values[i];
            }
        }
        public List<object> Values = new List<object>();

        public static Tuple FromRow(Row row)
        {
            return new Tuple()
            {
                Values = row.Values.ToList()
            };
        }
        public static Row ToRow(Tuple row)
        {
            return new Row()
            {
                Values = row.Values.ToArray()
            };
        }

        public Row ToRow()
        {
            return ToRow(this);
        }
    }
}
