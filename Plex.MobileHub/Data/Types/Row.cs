using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plex.MobileHub.Data.Types
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
    }
}