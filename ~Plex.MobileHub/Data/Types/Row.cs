using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plex.MobileHub.Data.Types
{
    public class Tuple
    {
        public object this[int i]
        {
            get
            {
                return Values[i];
            }
            set
            {
                Values[i] = value;
            }
        }
        public List<object> Values = new List<object>();
    }
}