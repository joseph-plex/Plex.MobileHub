using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Plex.Data;
namespace Plex.MobileHub.ServiceLibrary.Types
{
    public class CLIENT_USERS : RepositoryEntryBase, IRepositoryEntry
    {
        public int USER_ID { get; set; }//	NUMBER(10)	N			
        public int CLIENT_ID { get; set; }//NUMBER(10)	N			
        public string NAME { get; set; }//	VARCHAR2(25)	N			
        public string PASSWORD { get; set; }//VARCHAR2(25)	Y	

        public CLIENT_USERS() : base() {
            primaryKeys.Add("USER_ID");
        }
        public CLIENT_USERS(PlexQueryResultTuple plexTuple) :
            this()
        {
            FromPlexQueryResultTuple(this, plexTuple);
        }

    }
}
