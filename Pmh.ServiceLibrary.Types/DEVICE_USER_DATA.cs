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
    public class DEVICE_USER_DATA : __TypeBase
    {
        [DataMember]
        public int USER_DATA_ID { get; set; }
        [DataMember]
        public int? DEVICE_ID { get; set; }
        [DataMember]
        public int USER_PERMISSION { get; set; }
        [DataMember]
        public DateTime? EXECUTION_INITIATION { get; set; }
        [DataMember]
        public DateTime? EXECUTION_COMPLETION { get; set; }

        public DEVICE_USER_DATA() : base() {
            primaryKeys.Add("USER_DATA_ID");
        }
        public DEVICE_USER_DATA(PlexQueryResultTuple plexTuple) :
            this()
        {
            FromPlexQueryResultTuple(this, plexTuple);
        }

    }
}
