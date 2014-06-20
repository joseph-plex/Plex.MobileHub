using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plex.MobileHub.Data.Types
{
    class LOGS : IRepositoryEntry
    {
        public int LOG_ID { get; set; }//NUMBER(10)	N			
        public DateTime LOG_DATE { get; set; }//DATE	N			
        public string DESCRIPTION { get; set; }//	VARCHAR2(2048)	N			
        public int? CLIENT_ID { get; set; }//	NUMBER(10)	Y		
    }
}
