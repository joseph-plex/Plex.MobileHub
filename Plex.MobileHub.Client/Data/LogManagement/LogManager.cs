using System;
using System.Linq;
using System.Collections.Generic;

using MobileHubClient.Logs.Behaviour;

namespace MobileHubClient.Logs
{
    public sealed class LogManager
    {
        public static LogManager Instance
        {
            get
            {
                return instance;
            }
        }

        private static LogManager instance = new LogManager();

        public List<IAddBehaviour> AddLogBehaviour
        {
            get;
            private set;
        }

        internal List<Log> Logs;

        private LogManager()
        {
            Logs = new List<Log>();
            AddLogBehaviour = new List<IAddBehaviour>();
            AddLogBehaviour.Add(new DefaultAddBehaviour(this));
            AddLogBehaviour.Add(new EventViewerAddBehaviour()
            {
                CutoffPriority = LogPriority.Lowest,
                LogAllErrors = true,
                SystemLog = new System.Diagnostics.EventLog() { Source = "PMHC", Log = "PMHC" }
            });
        }

        public void Add(Log log) 
        {
            foreach (var behaviour in AddLogBehaviour) behaviour.Add(log);
        }

        public void Add(String log)
        {
            Add(new Log(log));
        }

        public void Add(String log, LogPriority priority)
        {
            Add(new Log(log, priority));
        }

        public void Add(String log, LogPriority priority, bool isError)
        {
            Add(new Log(log, priority, isError));

        }

        public void Add(String log, LogPriority priority, bool isError, DateTime logTime)
        {
            Add(new Log(log, priority, isError, logTime));
        }

        public void Add(Exception e, LogPriority priority = LogPriority.Highest)
        {
            Add(new Log(e.ToString(), priority, true, DateTime.Now));
        }

        public IEnumerable<Log> GetLogs() 
        {
            return (IEnumerable<Log>)Logs.ToList();
        }
    }
}
