﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plex.MobileHub.Data.Types
{
    public class LOGS : RepositoryEntryBase, IRepositoryEntry
    {
        public int LOG_ID { get; set; }//NUMBER(10)	N			
        public DateTime LOG_DATE { get; set; }//DATE	N			
        public string DESCRIPTION { get; set; }//	VARCHAR2(2048)	N			
        public int? CLIENT_ID { get; set; }//	NUMBER(10)	Y		

        public LOGS() : base() {
            primaryKeys.Add("LOG_ID");
        }
        public LOGS(PlexQueryResultTuple plexTuple) :
            this()
        {
            FromPlexQueryResultTuple(this, plexTuple);
        }
    }
}
