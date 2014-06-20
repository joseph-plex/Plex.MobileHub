using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plex.MobileHub.Data.Types
{
    public class DEV_DATA_VER_QUERIES : IRepositoryEntry
    {
        public int DATABASE_VERSION_ID { get; set; }//DATABASE_VERSION_ID	NUMBER(10)	N
        public int QUERY_ID { get; set; }//QUERY_ID	NUMBER(10)	N		
        public DateTime QUERY_EXECUTION_TIME { get; set; }//QUERY_EXECUTION_TIME	DATE	N

    }
}
