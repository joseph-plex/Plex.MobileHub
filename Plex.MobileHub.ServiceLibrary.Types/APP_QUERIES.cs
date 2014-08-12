using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plex.Data;
using System.Runtime.Serialization;

namespace Plex.MobileHub.ServiceLibrary.Types
{
    [DataContract, KnownType(typeof(IRepositoryEntry))]
    public class APP_QUERIES : RepositoryEntryBase, IRepositoryEntry
    {
        [DataMember]
        public int QUERY_ID { get; set; }//NUMBER(10)	N			
        [DataMember]
        public int APP_ID { get; set; }//NUMBER(10)	N			
        [DataMember]
        public string NAME { get; set; }//VARCHAR2(50)	N	
        [DataMember]
        public string TABLE_NAME { get; set; } //VARCHAR2(50) Y
        [DataMember]
        public string DESCRIPTION { get; set; }//VARCHAR2(4000)	Y			
        [DataMember]
        public int TABLE_ID { get; set; }//NUMBER(10)	N			
        [DataMember]
        public int IS_DELTA { get; set; }//NUMBER(1)	N			
        [DataMember]
        public string SQL { get; set; }//VARCHAR2(4000)	N			
        [DataMember]
        public int? SEQ_LIMIT_TIMESPAN { get; set; }//NUMBER(10)	Y			
        [DataMember]
        public int? SEQ_LIMIT { get; set; }//NUMBER(10)	Y		

        public APP_QUERIES()
            : base()
        {
            primaryKeys.Add("QUERY_ID");
        }

        public APP_QUERIES(PlexQueryResultTuple plexTuple) :
            this() 
        {
            FromPlexQueryResultTuple(this, plexTuple);
        }
    }
}
