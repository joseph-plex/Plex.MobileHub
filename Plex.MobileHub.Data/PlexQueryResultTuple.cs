using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plex.MobileHub.Data
{
    public class PlexQueryResultTuple : IPlexQueryResultTuple
    {
        internal PlexQueryResult parent;
         
        public Object this[int i]
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

        public IList<Object> Values { get; set; }

        public PlexQueryResultTuple()
        {
            parent = null;
            Values = new List<Object>();
        }
    }
}
