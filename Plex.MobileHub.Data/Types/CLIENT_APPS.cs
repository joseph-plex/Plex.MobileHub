﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plex.MobileHub.Data.Types
{
    public class CLIENT_APPS : IRepositoryEntry
    {
        public int CLIENT_APP_ID { get; set; }//	NUMBER(10)	N			
        public int APP_ID { get; set; }//	NUMBER(10)	N			
        public int CLIENT_ID { get; set; }//	NUMBER(10)	N	

    }
}