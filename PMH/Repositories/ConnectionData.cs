using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;

namespace Plex.PMH.Repositories
{
    public class ConnectionData
    {
        public int ClientId;
        public string Key;
        
        public DateTime InitTime;
        public Stopwatch LastCheck;
    }
}