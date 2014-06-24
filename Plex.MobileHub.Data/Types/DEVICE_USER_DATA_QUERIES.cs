﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plex.MobileHub.Data.Types
{
    public class DEVICE_USER_DATA_QUERIES : RepositoryEntryBase, IRepositoryEntry
    {
        public int DEVICE_USER_DATA_QUERIES_ID { get; set; }
        public DateTime QUERY_EXECUTION_TIME { get; set; }
        public int DEVICE_USER_DATA_ID { get; set; }
        public int QUERY_ID { get; set; }


        public DEVICE_USER_DATA_QUERIES() : base() {
            PrimaryKeys.Add("DEVICE_USER_DATA_QUERIES_ID");
        }
        public DEVICE_USER_DATA_QUERIES(PlexQueryResultTuple plexTuple) : base(plexTuple) { }
    }
}
