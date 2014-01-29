using System.Collections.Generic;

namespace Plex.PMH.Objects
{
    public class Row
    {
        public int DBAction;
        public int RowVersion;
        public List<object> Values = new List<object>();
    }
}
