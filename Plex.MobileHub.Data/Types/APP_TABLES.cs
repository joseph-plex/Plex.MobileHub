﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plex.MobileHub.Data.Types
{
    public class APP_TABLES : RepositoryEntryBase, IRepositoryEntry
    {
        public int TABLE_ID { get; set; }//NUMBER(10)	N			
        public int APP_ID { get; set; }//	NUMBER(10)	N			
        public string NAME { get; set; }//	VARCHAR2(50)	N			
        public string DESCRIPTION { get; set; }//	VARCHAR2(4000)	Y			
        public int INSERT_ALLOWED { get; set; }//	NUMBER(1)	Y			
        public int UPDATE_ALLOWED { get; set; }//	NUMBER(1)	Y			
        public int DELETE_ALLOWED { get; set; }//	NUMBER(1)	Y			
        public int QUERY_ALLOWED { get; set; }//	NUMBER(1)	Y
	
        public APP_TABLES() : base () {
            primaryKeys.Add("TABLE_ID");
        }
        public APP_TABLES(PlexQueryResultTuple plexTuple) : base(plexTuple) {
            
        }
    }
}
