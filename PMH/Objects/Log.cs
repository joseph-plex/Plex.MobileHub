using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plex.MobileHub.Objects
{
    public class Log
    {
        public LogPriority Priority
        {
            get;
            set;
        }
        public DateTime Date
        {
            get;
            set;
        }
        public String Record
        {
            get;
            set;
        }

        public Log() : this(string.Empty) { }
        public Log(String Log, DateTime Date, LogPriority Priority = LogPriority.Normal)
        {
            this.Record = Log;
            this.Date = Date;
            this.Priority = LogPriority.Normal;
        }
        public Log(String Log, LogPriority Priority = LogPriority.Normal) : this(Log, DateTime.Now, Priority) { }
    }
}