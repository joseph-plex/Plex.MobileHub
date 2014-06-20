﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plex.MobileHub.Data
{
    public class PlexQueryResultTuple : IPlexQueryResultTuple
    {
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
            Values = new List<Object>();
        }
    }
}
