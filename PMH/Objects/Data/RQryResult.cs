using System;
using System.Collections.Generic;

namespace MobileHub.Objects
{
    /// <summary>
    /// Registered Query Result, this contains queries from the database.
    /// </summary>
    public class RQryResult : QryResult
    {
        public string TableName;
        public string QueryName;
    }
}
