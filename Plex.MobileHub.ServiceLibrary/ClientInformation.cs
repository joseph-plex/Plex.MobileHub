using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Plex.Data;
using System.Threading.Tasks;

namespace Plex.MobileHub.ServiceLibrary
{
    public class ClientInformation : RepositoryEntryBase, IRepositoryEntry
    {
        public int ClientId { get; set; }
        public IClientCallback Callback { get; set; }

        public ClientInformation()
            : base()
        {
            primaryKeys.Add("ClientId");
        }
    }
}
