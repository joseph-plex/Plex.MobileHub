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
    public class DEVICES : __TypeBase
    {
        [DataMember]
        public int DEVICE_ID { get; set; }
        [DataMember]
        public int APP_ID { get; set; }

        public DEVICES()
            : base()
        {
            primaryKeys.Add("DEVICE_ID");
        }
        public DEVICES(PlexQueryResultTuple plexTuple) :
            this()
        {
            FromPlexQueryResultTuple(this, plexTuple);
        }
    }
}
