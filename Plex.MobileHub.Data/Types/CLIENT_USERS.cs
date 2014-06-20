using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plex.MobileHub.Data.Types
{
    class CLIENT_USERS
    {
        public int USER_ID { get; set; }//	NUMBER(10)	N			
        public int CLIENT_ID { get; set; }//NUMBER(10)	N			
        public string NAME { get; set; }//	VARCHAR2(25)	N			
        public string PASSWORD { get; set; }//VARCHAR2(25)	Y	
    }
}
