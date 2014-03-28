using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MobileHub.Objects.ResultTypes
{
    public class DeviceSynchronizeMethodResult : MethodResult
    {

        public DateTime StartTimeStamp;
        public DateTime CompletionTimeStamp;

        public List<QueryResult> Results = new List<QueryResult>();

    }
}