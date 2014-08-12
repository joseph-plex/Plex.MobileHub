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
    public class CLIENT_USERS : __TypeBase
    {
        [DataMember]
        public int USER_ID { get; set; }//	NUMBER(10)	N			
        [DataMember]
        public int CLIENT_ID { get; set; }//NUMBER(10)	N			
        [DataMember]
        public string NAME { get; set; }//	VARCHAR2(25)	N			
        [DataMember]
        public string PASSWORD { get; set; }//VARCHAR2(25)	Y	

        public CLIENT_USERS() : base() {
            primaryKeys.Add("USER_ID");
        }
        public CLIENT_USERS(PlexQueryResultTuple plexTuple) :
            this()
        {
            FromPlexQueryResultTuple(this, plexTuple);
        }

    }
}
