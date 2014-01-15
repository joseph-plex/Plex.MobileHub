using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plex.PMH.Objects.Requests
{
    public class QueryData
    {
        public int QueryId
        {
            get;
            set;
        }

        public DateTime? QueryTime
        {
            get;
            set;
        }
    }

}