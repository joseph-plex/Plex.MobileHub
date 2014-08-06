using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Plex.Data;
namespace Plex.MobileHub.ServiceLibrary.Types
{
    public class CLIENT_APPS : RepositoryEntryBase, IRepositoryEntry
    {
        public int CLIENT_APP_ID { get; set; }//	NUMBER(10)	N			
        public int APP_ID { get; set; }//	NUMBER(10)	N			
        public int CLIENT_ID { get; set; }//	NUMBER(10)	N	

        public CLIENT_APPS() : base() {
            primaryKeys.Add("CLIENT_APP_ID");
        }
        public CLIENT_APPS(PlexQueryResultTuple plexTuple) :
            this()
        {
            FromPlexQueryResultTuple(this, plexTuple);
        }

    }
}
