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
    public class LOGS : __TypeBase
    {
        [DataMember]
        public int LOG_ID { get; set; }//NUMBER(10)	N			
        [DataMember]
        public DateTime LOG_DATE { get; set; }//DATE	N			
        [DataMember]
        public string DESCRIPTION { get; set; }//	VARCHAR2(2048)	N			
        [DataMember]
        public int? CLIENT_ID { get; set; }//	NUMBER(10)	Y		

        public LOGS() : base() {
            primaryKeys.Add("LOG_ID");
        }
        public LOGS(PlexQueryResultTuple plexTuple) :
            this()
        {
            FromPlexQueryResultTuple(this, plexTuple);
        }
    }
}
