using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plex.MobileHub.Data.Types
{
    public class APP_QUERY_CONDITIONS : RepositoryEntryBase, IRepositoryEntry
    {
        public int CONDITION_ID { get; set; }//	NUMBER(10)	N			
        public int QUERY_ID { get; set; }//	NUMBER(10)	N			
        public string COLUMN_NAME { get; set; }//	VARCHAR2(30)	N			
        public string COLUMN_NVL { get; set; }//	VARCHAR2(1000)	Y			
        public string COLUMN_VALUE { get; set; }//	VARCHAR2(1000)	N			
        public string OPERATOR { get; set; }//	VARCHAR2(12)	N		

        public APP_QUERY_CONDITIONS() : base() 
        {
            primaryKeys.Add("CONDITION_ID");
        }

        public APP_QUERY_CONDITIONS(PlexQueryResultTuple plexTuple) : base(plexTuple) { }


    }
}
