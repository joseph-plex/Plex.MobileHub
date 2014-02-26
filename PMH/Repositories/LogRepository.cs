using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Xml.Serialization;

namespace Plex.PMH.Repositories
{
    public class Logs
    {
        public static int count = 0;
        private static Logs LogRepo = new Logs();
        public static Logs Instance
        {
            get
            {
                return LogRepo;
            }
        }

        private const string FileName = "LogData.bin";

        public List<Log> logs;


        private Logs()
        {
            logs = new List<Log>();
        }

        public void Add(Log log)
        {
            logs.Add(log);
        }
        public void Add(object Log)
        {
            Add(new Log(Log.ToString()));
        }
        public void Add(object Log, LogPriority Priority = LogPriority.Normal)
        {
            Add(new Log(Log.ToString(), Priority));

        }
        public void Add(object Log, DateTime Date, LogPriority Priority = LogPriority.Normal) 
        {
            Add(new Log(Log.ToString(), Date, Priority));
        }
        public List<Log> GetLogs()
        {
            return new List<Log>(logs) ;
        }
    }
}