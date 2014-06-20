using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plex.MobileHub.Data.Types
{
    public class CLIENT_DB_COMPANY_USERS : RepositoryEntryBase, IRepositoryEntry
    {
        public int DB_COMPANY_USER_ID { get; set; }//NUMBER(10)	N			
        public int DB_COMPANY_ID { get; set; }//	NUMBER(10)	N			
        public int USER_ID { get; set; }//NUMBER(10)	N			
        public string CONNECT_AS { get; set; }//	VARCHAR2(100)	Y		
        
        public CLIENT_DB_COMPANY_USERS() : base() { }
        public CLIENT_DB_COMPANY_USERS(PlexQueryResultTuple plexTuple) : base(plexTuple) { }

    }
}
