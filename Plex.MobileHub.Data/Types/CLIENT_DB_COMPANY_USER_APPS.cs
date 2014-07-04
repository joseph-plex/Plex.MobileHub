using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plex.MobileHub.Data.Types
{
    public class CLIENT_DB_COMPANY_USER_APPS : RepositoryEntryBase, IRepositoryEntry
    {
        public int DB_COMPANY_USER_APP_ID { get; set; }//	NUMBER(10)	N			
        public int DB_COMPANY_USER_ID { get; set; }//	NUMBER(10)	N			
        public int APP_ID { get; set; }//	NUMBER(10)	N			
        public int? APP_USER_TYPE_ID { get; set; }//	NUMBER(10)	Y	

        public CLIENT_DB_COMPANY_USER_APPS() : base() {
            primaryKeys.Add("DB_COMPANY_USER_APP_ID");
        }
        public CLIENT_DB_COMPANY_USER_APPS(PlexQueryResultTuple plexTuple) :
            this()
        {
            FromPlexQueryResultTuple(this, plexTuple);
        }
    }
}
