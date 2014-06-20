using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plex.MobileHub.Data.Types
{
    public class QUERY_SEQUENCE_REQUESTS : IRepositoryEntry
    {
        public int TQR_ID { get; set; }//NUMBER(10)	N			
        public int USER_QUERYING { get; set; }//	NUMBER(10)	Y			
        public int DATABASE_QUERIED { get; set; }//	NUMBER(10)	Y			
        public int QUERY_SEQUENCING { get; set; }//NUMBER(10)	Y			
        public DateTime SEQ_QUERY_TIME { get; set; }//DATE	Y	
    }
}
