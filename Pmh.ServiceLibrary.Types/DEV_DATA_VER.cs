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
    public class DEV_DATA_VER : __TypeBase
    {
        [DataMember]
        public int DEV_DATA_VER_ID { get; set; }//DEV_DATA_VER_ID	NUMBER(10)	N	
        [DataMember]
        public int DEV_DATA_ID { get; set; }//DEV_DATA_ID	NUMBER(10)	N

        public DEV_DATA_VER() : base() {
            primaryKeys.Add("DEV_DATA_VER_ID");
        }
        public DEV_DATA_VER(PlexQueryResultTuple plexTuple) :
            this()
        {
            FromPlexQueryResultTuple(this, plexTuple);
        }
    }
}
