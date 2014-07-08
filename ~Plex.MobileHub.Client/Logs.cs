//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Diagnostics;
//using System.IO;
//using System.Xml.Serialization;

//namespace Client.Core
//{
//    public sealed class Logs
//    {
//        private string SettingsFile = "pmhcSettings.config";
//        public const string Source = "P.M.H.C.";
//        public const string LogName = "P.M.H.C.";

//        //todo analyse and determine a log minimum threshold and throw an error if the log cache amount is set lower than that
//        //todo if Log limit is less than current about of logs, delete the excess
//        static Logs instance = new Logs();
//        public static Logs Instance
//        {
//            get
//            {
//                return instance;
//            }
//        }
     
//        public int LogCacheAmount
//        {
//            get
//            {
//                return settings.LogCacheAmount;
//            }
//            set
//            {
//                if (value > LogSettings.CacheMaximum || value < LogSettings.CacheMinimum)
//                    throw new Exception("Invalid Cache Amount specified");

//                if (value < settings.LogCacheAmount && value < logs.Count)
//                    logs.RemoveRange(0, logs.Count - value);
//                settings.LogCacheAmount = value;
//            }
//        }
//        public bool LogAllErrors
//        {
//            get
//            {
//                return settings.LogAllErrors;
//            }
//            set
//            {
//                settings.LogAllErrors = value;
//            }
//        }
//        public Priority SystemLogLevel
//        {
//            get
//            {
//                return settings.SystemLogLevel;
//            }
//            set
//            {
//                settings.SystemLogLevel = value;
//            }
//        }

//        LogSettings settings = new LogSettings();
//        List<Log> logs = new List<Log>();
        
//        EventLog systemLog = new EventLog() { Source = Source, Log = LogName };
//        private Logs()  
//        {
//            if (File.Exists(SettingsFile))
//                LoadSettings();
//        }

//        public void Add(Log log)
//        {
//            if ((logs.Count + 1) > settings.LogCacheAmount)
//                logs.RemoveAt(0);
//            logs.Add(log);

//            if (IsLoggable(log))
//                systemLog.WriteEvent(new EventInstance(0,0){EntryType = ((log.IsError)?EventLogEntryType.Error: EventLogEntryType.Information)}, log.LogTime.ToString("dd MMM HH:mm:ss"), log.Entry);
//        }
//        public void Add(string Entry) 
//        {
//            Add(Entry, Priority.Low);
//        }
//        public void Add(string Entry, Priority Priority)
//        {
//            Add(Entry, Priority, false);
//        }
//        public void Add(string Entry, Priority Priority, bool IsError)
//        {
//            Add(Entry, Priority, IsError, DateTime.Now);
//        }
//        public void Add(string Entry, Priority Priority, bool IsError, DateTime LogTime)
//        {
//            Add(new Log() { Entry = Entry, Priority = Priority, LogTime = LogTime, IsError = IsError });
//        }

//        public List<Log> GetLogs()
//        {
//            return new List<Log>(logs);
//        }
//        public void LoadSettings()
//        {
//            using (var fs = new FileStream(SettingsFile, FileMode.Open))
//                settings = (LogSettings)new XmlSerializer(typeof(LogSettings)).Deserialize(fs);
//        }

//        public void SaveSettings()
//        {
//            using (var fs = new FileStream(SettingsFile, FileMode.Create))
//                new XmlSerializer(typeof(LogSettings)).Serialize(fs, settings);
//        }

//        bool IsLoggable(Log log)
//        {
//            if(settings.LogAllErrors && log.IsError)
//                return true;
//            if(settings.SystemLogLevel < log.Priority)
//                return true;
//            return false;
//        }
//    }
//    public enum Priority
//    {
//        Lowest,
//        Low,
//        Normal,
//        High,
//        Highest
//    }
//    public class Log
//    {
//        public DateTime LogTime;
//        public String Entry;
//        public Priority Priority;
//        public bool IsError;
//    }
//    public class LogSettings
//    {
//        public const int CacheMaximum = 10000;
//        public const int CacheMinimum = 25;

//        public int LogCacheAmount
//        {
//            get
//            {
//                return logcache;
//            }
//            set
//            {
//                logcache = value;
//            }
//        }
//        public bool LogAllErrors
//        {
//            get
//            {
//                return logerrors;
//            }
//            set
//            {
//                logerrors = value;
//            }
//        }
//        public Priority SystemLogLevel
//        {
//            get 
//            {
//                return priority;
//            }
//            set
//            {
//                priority = value;
//            }
//        }

//        int logcache;
//        bool logerrors;
//        Priority priority;
//        public LogSettings()
//        {
//            LogAllErrors = true;
//            LogCacheAmount = 100;
//            SystemLogLevel = Priority.Normal;
//        }
//    }
//}
