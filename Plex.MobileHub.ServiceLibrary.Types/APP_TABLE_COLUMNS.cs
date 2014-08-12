using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plex.Data;
using System.Runtime.Serialization;

namespace Plex.MobileHub.ServiceLibrary.Types
{
    [DataContract]
    public class APP_TABLE_COLUMNS : __TypeBase
    {
        [DataMember]
        public int TABLE_ID { get; set; }//NUMBER(10)	N			
        [DataMember]
        public int TABLE_COLUMN_ID { get; set; }//NUMBER(10)	N			
        [DataMember]
        public string COLUMN_NAME { get; set; }//VARCHAR2(50)	N			
        [DataMember]
        public int COLUMN_SEQUENCE { get; set; }//NUMBER(3)	N			
        [DataMember]
        public string DATA_TYPE { get; set; }//VARCHAR2(50)	Y			
        [DataMember]
        public int? DATA_LENGTH { get; set; }//NUMBER(10)	Y			
        [DataMember]
        public int? DATA_PRECISION { get; set; }//NUMBER(2)	Y			
        [DataMember]
        public int? DATA_SCALE { get; set; }//NUMBER(2)	Y			
        [DataMember]
        public int? ALLOW_DB_NULL { get; set; }//NUMBER(1)	Y			
        [DataMember]
        public int? IS_READ_ONLY { get; set; }//NUMBER(1)	Y			
        [DataMember]
        public int? IS_LONG { get; set; }//NUMBER(1)	Y			
        [DataMember]
        public int? IS_KEY { get; set; }//NUMBER(1)	Y			
        [DataMember]
        public string KEY_TYPE { get; set; }//VARCHAR2(20)	Y			
        [DataMember]
        public int? IS_UNIQUE { get; set; }//	NUMBER(1)	Y			
        [DataMember]
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
