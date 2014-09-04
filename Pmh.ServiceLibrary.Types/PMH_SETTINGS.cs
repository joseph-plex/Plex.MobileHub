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
    public class PMH_SETTINGS : __TypeBase
    {
        [DataMember]
        public int PMH_ID { get; set; }//	NUMBER(10)	N		
        [DataMember]
        public int MISC { get; set; }//	NUMBER(10)	N	

        public PMH_SETTINGS() : base() {
            primaryKeys.Add("PMH_ID");
        }
        public PMH_SETTINGS(PlexQueryResultTuple plexTuple) :
            this()
        {
            FromPlexQueryResultTuple(this, plexTuple);
        }
    }
}
