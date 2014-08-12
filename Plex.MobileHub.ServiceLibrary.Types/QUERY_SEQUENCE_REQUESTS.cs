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
    public class QUERY_SEQUENCE_REQUESTS : __TypeBase
    {
        [DataMember]
        public int TQR_ID { get; set; }//NUMBER(10)	N			
        [DataMember]
        public int USER_QUERYING { get; set; }//	NUMBER(10)	Y			
        [DataMember]
        public int DATABASE_QUERIED { get; set; }//	NUMBER(10)	Y			
        [DataMember]
        public int QUERY_SEQUENCING { get; set; }//NUMBER(10)	Y			
        [DataMember]
        public DateTime SEQ_QUERY_TIME { get; set; }//DATE	Y	

        public QUERY_SEQUENCE_REQUESTS() : base() {
            primaryKeys.Add("TQR_ID");
        }
        public QUERY_SEQUENCE_REQUESTS(PlexQueryResultTuple plexTuple) :
            this()
        {
            FromPlexQueryResultTuple(this, plexTuple);
        }
    }
}
