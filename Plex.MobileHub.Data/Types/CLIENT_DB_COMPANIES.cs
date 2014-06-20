﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plex.MobileHub.Data.Types
{
    public class CLIENT_DB_COMPANIES : IRepositoryEntry
    {
        public int DB_COMPANY_ID { get; set; }//	NUMBER(10)	N			
        public string DATABASE_CSTRING { get; set; }//	VARCHAR2(25)	Y			
        public string COMPANY_CODE { get; set; }//	VARCHAR2(12)	Y			
        public int CLIENT_ID { get; set; }//	NUMBER(10)	N			

    }
}
