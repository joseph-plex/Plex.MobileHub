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
    public class CLIENT_DB_COMPANY_USERS : __TypeBase
    {
        [DataMember]
        public int DB_COMPANY_USER_ID { get; set; }//NUMBER(10)	N			
        [DataMember]
        public int DB_COMPANY_ID { get; set; }//	NUMBER(10)	N			
        [DataMember]
        public int USER_ID { get; set; }//NUMBER(10)	N			
        [DataMember]
        public string CONNECT_AS { get; set; }//	VARCHAR2(100)	Y		
        
        public CLIENT_DB_COMPANY_USERS() : base() {
            primaryKeys.Add("DB_COMPANY_USER_ID");
        }
        public CLIENT_DB_COMPANY_USERS(PlexQueryResultTuple plexTuple) :
            this()
        {
            FromPlexQueryResultTuple(this, plexTuple);
        }

    }
}
