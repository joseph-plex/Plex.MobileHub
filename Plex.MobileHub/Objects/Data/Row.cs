using System.Collections.Generic;

namespace Plex.MobileHub.Objects
{
    public class Row
    {
        public int DBAction { get; set; }
        public int RowVersion { get; set; }
        public List<object> Values { get; set; }

        public Row()
        {
            Values = new List<object>();
        }
    }
}
