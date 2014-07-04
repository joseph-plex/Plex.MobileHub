using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plex.MobileHub.Data.Types
{
    public class DEV_DATA_VER : RepositoryEntryBase, IRepositoryEntry
    {
        public int DEV_DATA_VER_ID { get; set; }//DEV_DATA_VER_ID	NUMBER(10)	N	
        public int DEV_DATA_ID { get; set; }//DEV_DATA_ID	NUMBER(10)	N

        public DEV_DATA_VER() : base() { }
        public DEV_DATA_VER(PlexQueryResultTuple plexTuple) :
            this()
        {
            FromPlexQueryResultTuple(this, plexTuple);
        }
    }
}
