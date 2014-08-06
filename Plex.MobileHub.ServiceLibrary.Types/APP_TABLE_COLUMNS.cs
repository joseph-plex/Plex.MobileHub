using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plex.Data;

namespace Plex.MobileHub.ServiceLibrary.Types
{
    public class APP_TABLE_COLUMNS : RepositoryEntryBase, IRepositoryEntry
    {
        public int TABLE_ID { get; set; }//NUMBER(10)	N			
        public int TABLE_COLUMN_ID { get; set; }//NUMBER(10)	N			
        public string COLUMN_NAME { get; set; }//VARCHAR2(50)	N			
        public int COLUMN_SEQUENCE { get; set; }//NUMBER(3)	N			
        public string DATA_TYPE { get; set; }//VARCHAR2(50)	Y			
        public int? DATA_LENGTH { get; set; }//NUMBER(10)	Y			
        public int? DATA_PRECISION { get; set; }//NUMBER(2)	Y			
        public int? DATA_SCALE { get; set; }//NUMBER(2)	Y			
        public int? ALLOW_DB_NULL { get; set; }//NUMBER(1)	Y			
        public int? IS_READ_ONLY { get; set; }//NUMBER(1)	Y			
        public int? IS_LONG { get; set; }//NUMBER(1)	Y			
        public int? IS_KEY { get; set; }//NUMBER(1)	Y			
        public string KEY_TYPE { get; set; }//VARCHAR2(20)	Y			
        public int? IS_UNIQUE { get; set; }//	NUMBER(1)	Y			
        public string DESCRIPTION { get; set; }//VARCHAR2(4000)	Y		

        public APP_TABLE_COLUMNS() : base() 
        {
            primaryKeys.Add("TABLE_COLUMN_ID");
        }

        public APP_TABLE_COLUMNS(PlexQueryResultTuple plexTuple) :
            this()
        {
            FromPlexQueryResultTuple(this, plexTuple);
        }
    }
}
