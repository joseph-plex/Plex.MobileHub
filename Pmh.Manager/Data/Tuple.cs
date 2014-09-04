using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pmh.Manager.ManagementWebservice;

namespace Pmh.Manager.Data
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

        public static Tuple FromRow(ManagementWebservice.Tuple row)
        {
            return new Tuple()
            {
                Values = row.Values.ToList()
            };
        }
        public static ManagementWebservice.Tuple ToRow(Tuple row)
        {
            return new ManagementWebservice.Tuple()
            {
                Values = row.Values.ToArray()
            };
        }

        public ManagementWebservice.Tuple ToRow()
        {
            return ToRow(this);
        }
    }
}
