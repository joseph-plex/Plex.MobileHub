using System.Collections.Generic;

namespace MobileHub.Objects
{
    public class Row
    {
        public int DBAction;
        public int RowVersion;
        public List<object> Values = new List<object>();
    }
}
