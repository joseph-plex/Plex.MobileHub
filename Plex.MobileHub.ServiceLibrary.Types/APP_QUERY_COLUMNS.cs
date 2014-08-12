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
    public class APP_QUERY_COLUMNS : __TypeBase
    {
        [DataMember]
        public int COLUMN_ID { get; set; }// NUMBER(10)	N			
        [DataMember]
        public int QUERY_ID { get; set; }// NUMBER(10)	N			
        [DataMember]
        public string COLUMN_NAME { get; set; }//VARCHAR2(50)	N			
        [DataMember]
        public int COLUMN_SEQUENCE { get; set; }//NUMBER(3)	N	

        public APP_QUERY_COLUMNS() : base() 
        { 
            primaryKeys.Add("COLUMN_ID");
        }
        public APP_QUERY_COLUMNS(PlexQueryResultTuple plexTuple) :
            this()
        {
            FromPlexQueryResultTuple(this, plexTuple);
        }
    }
}
