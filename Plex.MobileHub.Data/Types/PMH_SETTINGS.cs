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
        public int MISC { get; set; }//	NUMBER(10)	N	

        public PMH_SETTINGS() : base() {
            primaryKeys.Add("PMH_ID");
        }
        public PMH_SETTINGS(PlexQueryResultTuple plexTuple) :
            this()
        {
            FromPlexQueryResultTuple(this, plexTuple);
        }
    }
}
