using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plex.MobileHub.Data
{
    public delegate void RepoEventHandler(object sender, RepositoryOperationEventArgs e);
    public class RepositoryOperationEventArgs : EventArgs
    {
        public IRepositoryEntry Entry { get; set; }
    }
}
