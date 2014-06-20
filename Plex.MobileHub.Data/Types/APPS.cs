﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plex.MobileHub.Data.Types
{
    public class APPS : IRepositoryEntry
    {
        public int APP_ID { get; set; }
        public string AUTH_KEY { get; set; }
        public string TITLE { get; set; }
        public string DESCRIPTION { get; set; }
        public int IS_CLIENT_CUSTOM_APP { get; set; }
    }
}