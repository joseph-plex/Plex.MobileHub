using System.Collections.Generic;

namespace Plex.MobileHub.Objects
{
    public class Row
    {
        public int DBAction;
        public int RowVersion;
        public List<object> Values = new List<object>();
    }
}
