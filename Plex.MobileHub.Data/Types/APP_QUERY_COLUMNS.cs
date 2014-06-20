﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plex.MobileHub.Data.Types
{
    public class APP_QUERY_COLUMNS : IRepositoryEntry
    {
        public int COLUMN_ID { get; set; }// NUMBER(10)	N			
        public int QUERY_ID { get; set; }// NUMBER(10)	N			
        public string COLUMN_NAME { get; set; }//VARCHAR2(50)	N			
        public int COLUMN_SEQUENCE { get; set; }//NUMBER(3)	N	

    }
}
