using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

using Plex.Data;
namespace Pmh.ServiceLibrary.Types
{
    [DataContract]
    public class APP_TABLES : __TypeBase
    {
        [DataMember]
        public int TABLE_ID { get; set; }//NUMBER(10)	N			
        [DataMember]
        public int APP_ID { get; set; }//	NUMBER(10)	N			
        [DataMember]
        public string NAME { get; set; }//	VARCHAR2(50)	N			
        [DataMember]
        public string DESCRIPTION { get; set; }//	VARCHAR2(4000)	Y			
        [DataMember]
        public int INSERT_ALLOWED { get; set; }//	NUMBER(1)	Y			
        [DataMember]
        public int UPDATE_ALLOWED { get; set; }//	NUMBER(1)	Y			
        [DataMember]
        public int DELETE_ALLOWED { get; set; }//	NUMBER(1)	Y			
        [DataMember]
        public int QUERY_ALLOWED { get; set; }//	NUMBER(1)	Y
	
        public APP_TABLES() : base () {
            primaryKeys.Add("TABLE_ID");
        }
        public APP_TABLES(PlexQueryResultTuple plexTuple) :
            this() 
        {
            FromPlexQueryResultTuple(this, plexTuple);
        }
    }
}
