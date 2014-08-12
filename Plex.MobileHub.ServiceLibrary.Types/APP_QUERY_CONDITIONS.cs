using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using Plex.Data;

namespace Plex.MobileHub.ServiceLibrary.Types
{
    [DataContract]
    public class APP_QUERY_CONDITIONS : __TypeBase
    {
        [DataMember]
        public int CONDITION_ID { get; set; }//	NUMBER(10)	N			
        [DataMember]
        public int QUERY_ID { get; set; }//	NUMBER(10)	N			
        [DataMember]
        public string COLUMN_NAME { get; set; }//	VARCHAR2(30)	N			
        [DataMember]
        public string COLUMN_NVL { get; set; }//	VARCHAR2(1000)	Y			
        [DataMember]
        public string COLUMN_VALUE { get; set; }//	VARCHAR2(1000)	N			
        [DataMember]
        public string OPERATOR { get; set; }//	VARCHAR2(12)	N		

        public APP_QUERY_CONDITIONS() : base() 
        {
            primaryKeys.Add("CONDITION_ID");
        }

        public APP_QUERY_CONDITIONS(PlexQueryResultTuple plexTuple) :
            this() 
        {
            FromPlexQueryResultTuple(this, plexTuple);
        }


    }
}
