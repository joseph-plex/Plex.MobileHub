using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pmh.ServiceLibrary
{
    public class DeviceSynchronizeMethodResult : MethodResult
    {

        public DateTime StartTimeStamp;
        public DateTime CompletionTimeStamp;

        public List<RegisteredQueryResult> Results = new List<RegisteredQueryResult>();
    }
}