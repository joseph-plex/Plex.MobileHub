using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plex.MobileHub.Data
{
    public class RepositoryOperationEventArgs : EventArgs
    {
        public virtual IRepositoryEntry Entry { get; set; }
    }
}
