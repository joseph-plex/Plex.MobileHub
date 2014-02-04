using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;

namespace Plex.PMH.Repositories
{
    public class ConnectionData
    {
        public Int32 ClientId;
        public String Key;
        
        public DateTime InitTime;
        public Stopwatch LastCheck;

        public String IPv4;
        public String IPv6;
        public Int32 Port;
    }
}