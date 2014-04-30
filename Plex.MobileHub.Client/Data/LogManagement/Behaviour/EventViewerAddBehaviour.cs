using System;
using System.Diagnostics;

namespace MobileHubClient.Logs.Behaviour
{
    public class EventViewerAddBehaviour : IAddBehaviour
    {
        public EventLog SystemLog
        {
            get;
            set;
        }
        public bool LogAllErrors
        {
            get;
            set;
        }
        public LogPriority CutoffPriority
        {
            get;
            set;
        }

        public EventViewerAddBehaviour()
        {
            LogAllErrors = false;
            CutoffPriority = LogPriority.Low;
        }
        public void Add(Log log)
        {
            if (log == null) throw new ArgumentException("log cannot be null");
            if (log.Priority >= CutoffPriority)
                Write(log);
            else if (LogAllErrors && log.IsError)
                Write(log);
        }

        private void Write(Log Entry)
        {
            SystemLog.WriteEvent(
               new EventInstance(0, 0)
               {
                   EntryType = (Entry.IsError) ? EventLogEntryType.Error : EventLogEntryType.Information,
               },
               Entry.Date.ToLongDateString(),
               Entry.Entry
           );
        }
    }
}
