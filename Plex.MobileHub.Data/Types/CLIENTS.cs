using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plex.MobileHub.Data.Types
{
    public class CLIENTS : RepositoryEntryBase, IRepositoryEntry
    {
        public int CLIENT_ID { get; set; }
        public string CLIENT_KEY { get; set; }
        public string DESCRIPTION { get; set; }
        public int? CLIENT_INSTANCE_ID { get; set; }
        public string CLIENT_IP_ADDRESS { get; set; }
        public int? CLIENT_PORT { get; set; }

        public CLIENTS() : base() { }
        public CLIENTS(PlexQueryResultTuple plexTuple) : base(plexTuple) { }

    }
}
