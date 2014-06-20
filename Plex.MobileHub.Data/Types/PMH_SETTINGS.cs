using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plex.MobileHub.Data.Types
{
    public class PMH_SETTINGS : RepositoryEntryBase, IRepositoryEntry
    {
        public int PMH_ID { get; set; }//	NUMBER(10)	N		

        public PMH_SETTINGS() : base() { }
        public PMH_SETTINGS(PlexQueryResultTuple plexTuple) : base(plexTuple) { }
    }
}
