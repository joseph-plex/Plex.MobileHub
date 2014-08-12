using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Plex.Data;

namespace Plex.MobileHub.ServiceLibrary.Types
{
    [DataContract]
    public class DEVICE_USER_DATA_QUERIES : __TypeBase
    {
        [DataMember]
        public int DEVICE_USER_DATA_QUERIES_ID { get; set; }
        [DataMember]
        public DateTime QUERY_EXECUTION_TIME { get; set; }
        [DataMember]
        public int DEVICE_USER_DATA_ID { get; set; }
        [DataMember]
        public int QUERY_ID { get; set; }


        public DEVICE_USER_DATA_QUERIES() : base() {
            primaryKeys.Add("DEVICE_USER_DATA_QUERIES_ID");
        }
        public DEVICE_USER_DATA_QUERIES(PlexQueryResultTuple plexTuple) :
            this()
        {
            FromPlexQueryResultTuple(this, plexTuple);
        }
    }
}
