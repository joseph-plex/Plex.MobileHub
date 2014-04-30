using System;

namespace MobileHubClient.Logs
{
    public class Log
    {
        public DateTime Date = DateTime.Now;

        public virtual String Entry
        {
            get;
            set;
        }

        public virtual bool IsError
        {
            get;
            set;
        }

        public virtual LogPriority Priority
        {
            get;
            set;
        }

        public Log()
        {

        }
        public Log(Exception e)
            : this(e.ToString(),LogPriority.Highest,true)
        {
            
        }
        public Log(String Entry) : this()
        {
            this.Entry = Entry;
        }

        public Log(String Entry, LogPriority Priority) : this(Entry)
        {
            this.Priority = Priority;
        }

        public Log(String Entry, LogPriority Priority, bool IsError) : this(Entry, Priority)
        {
            this.IsError = IsError;
        }

        public Log(String entry, LogPriority priority, bool isError, DateTime logTime)
            : this(entry, priority, isError)
        {
            this.Date = logTime;
        }
    }
}
