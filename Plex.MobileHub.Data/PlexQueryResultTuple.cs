using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

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

        
        public List<Object> Values { get; set; }

        public PlexQueryResultTuple()
        {
            parent = null;
            Values = new List<Object>();
        }
    }
}
