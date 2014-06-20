using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plex.MobileHub.Data.Types
{
    class APP_USER_TYPES
    {
        public int USER_TYPE_ID { get; set; }//	NUMBER(10)	N			
        public int APP_ID { get; set; }//	NUMBER(10)	N			
        public string CODE { get; set; }//VARCHAR2(12)	N			
        public string DESCRIPTION { get; set; }//	VARCHAR2(50)	Y	
    }
}
