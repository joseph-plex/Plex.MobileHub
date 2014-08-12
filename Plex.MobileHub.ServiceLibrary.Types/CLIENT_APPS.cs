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
    public class CLIENT_APPS : __TypeBase
    {
        [DataMember]
        public int CLIENT_APP_ID { get; set; }//	NUMBER(10)	N			
        [DataMember]
        public int APP_ID { get; set; }//	NUMBER(10)	N			
        [DataMember]
        public int CLIENT_ID { get; set; }//	NUMBER(10)	N	

        public CLIENT_APPS() : base() {
            primaryKeys.Add("CLIENT_APP_ID");
        }
        public CLIENT_APPS(PlexQueryResultTuple plexTuple) :
            this()
        {
            FromPlexQueryResultTuple(this, plexTuple);
        }

    }
}
