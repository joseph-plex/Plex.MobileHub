using System;
using System.Collections.Generic;
using MobileHubClient.PMH;
namespace MobileHubClient.Data
{
    public class Row
    {
        public object this[int i]
        {
            get
            {
                return Values[i];
            }
        }
        public List<object> Values = new List<object>();
        public static Row fromPMHRow(PMH.Row row)
        {
            return new Row()
            {
                Values = new List<object>(row.Values)
            };
        }
        public static PMH.Row ToPMHRow(Row row)
        {
            return new PMH.Row()
            {
                Values = row.Values.ToArray()
            };
        }

        public PMH.Row ToPMHRow()
        {
            return ToPMHRow(this);
        }
    }
}
