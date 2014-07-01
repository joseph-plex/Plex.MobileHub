using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plex.MobileHub.Data;

namespace Plex.MobileHub.ServiceLibraries
{
    public class OracleRepositoryOperationEventArgs : RepositoryOperationEventArgs
    {
        public Boolean Transaction { get; set; }
    }
}
