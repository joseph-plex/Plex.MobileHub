using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plex.Data;
using System.Runtime.Serialization;

namespace Pmh.ServiceLibrary.Types
{
    [DataContract]
    public class CLIENT_DB_COMPANIES : __TypeBase
    {
        [DataMember]
        public int DB_COMPANY_ID { get; set; }//	NUMBER(10)	N			
        [DataMember]
        public string DATABASE_CSTRING { get; set; }//	VARCHAR2(25)	Y			
        [DataMember]
        public string COMPANY_CODE { get; set; }//	VARCHAR2(12)	Y			
        [DataMember]
        public int CLIENT_ID { get; set; }//	NUMBER(10)	N			

        public CLIENT_DB_COMPANIES() : base() {
            primaryKeys.Add("DB_COMPANY_ID");
        }
        public CLIENT_DB_COMPANIES(PlexQueryResultTuple plexTuple) :
            this()
        {
            FromPlexQueryResultTuple(this, plexTuple);
        }
    }
}
