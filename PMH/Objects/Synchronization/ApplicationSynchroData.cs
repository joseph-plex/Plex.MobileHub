using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MobileHub.Objects.Synchronization
{
    public class ApplicationSynchroData
    {
        public int ApplicationId;
        public string ApplicationTitle;
        public string ApplicationDescription;

        public List<QuerySynchroData> ApplicationQueries = new List<QuerySynchroData>();

    }
}