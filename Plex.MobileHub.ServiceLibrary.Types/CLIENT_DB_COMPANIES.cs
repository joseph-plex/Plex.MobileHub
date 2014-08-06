using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plex.Data;

namespace Plex.MobileHub.ServiceLibrary.Types
{
    public class CLIENT_DB_COMPANIES : RepositoryEntryBase, IRepositoryEntry
    {
        public int DB_COMPANY_ID { get; set; }//	NUMBER(10)	N			
        public string DATABASE_CSTRING { get; set; }//	VARCHAR2(25)	Y			
        public string COMPANY_CODE { get; set; }//	VARCHAR2(12)	Y			
        public int CLIENT_ID { get; set; }//	NUMBER(10)	N			

        public CLIENT_DB_COMPANIES() : base() {
            primaryKeys.Add("DB_COMPANY_ID");
        }
        public CLIENT_DB_COMPANIES(PlexQueryResultTuple plexTuple) :
            this()
        {
            FromPlexQueryResultTuple(this, plexTuple);
        }
    }
}
