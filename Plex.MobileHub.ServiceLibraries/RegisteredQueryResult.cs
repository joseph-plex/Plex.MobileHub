using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plex.MobileHub.ServiceLibraries
{
    public class RegisteredQueryResult : QryResult
    {
        public string TableName { get; set; }
        public string QueryName { get; set; }
    }
}
