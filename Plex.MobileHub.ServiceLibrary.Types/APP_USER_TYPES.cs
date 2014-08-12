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
    public class APP_USER_TYPES : __TypeBase
    {
        [DataMember]
        public int USER_TYPE_ID { get; set; }//	NUMBER(10)	N			
        [DataMember]
        public int APP_ID { get; set; }//	NUMBER(10)	N			
        [DataMember]
        public string CODE { get; set; }//VARCHAR2(12)	N			
        [DataMember]
        public string DESCRIPTION { get; set; }//	VARCHAR2(50)	Y	


        public APP_USER_TYPES() : base() {
            primaryKeys.Add("USER_TYPE_ID");
        }
        public APP_USER_TYPES(PlexQueryResultTuple plexTuple) :
            this()
        {
            FromPlexQueryResultTuple(this, plexTuple);
        }

    }
}
