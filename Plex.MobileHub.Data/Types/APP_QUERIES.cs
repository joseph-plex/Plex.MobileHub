using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plex.MobileHub.Data.Types
{
    public class APP_QUERIES : RepositoryEntryBase, IRepositoryEntry
    {
        public int QUERY_ID { get; set; }//NUMBER(10)	N			
        public int APP_ID { get; set; }//NUMBER(10)	N			
        public string NAME { get; set; }//VARCHAR2(50)	N	
        public string TABLE_NAME { get; set; } //VARCHAR2(50) Y
        public string DESCRIPTION { get; set; }//VARCHAR2(4000)	Y			
        public int TABLE_ID { get; set; }//NUMBER(10)	N			
        public int IS_DELTA { get; set; }//NUMBER(1)	N			
        public string SQL { get; set; }//VARCHAR2(4000)	N			
        public int? SEQ_LIMIT_TIMESPAN { get; set; }//NUMBER(10)	Y			
        public int? SEQ_LIMIT { get; set; }//NUMBER(10)	Y		

        public APP_QUERIES()
            : base()
        {
            primaryKeys.Add("QUERY_ID");
        }

        public APP_QUERIES(PlexQueryResultTuple plexTuple) :
            this() 
        {
            FromPlexQueryResultTuple(this, plexTuple);
        }
    }
}
