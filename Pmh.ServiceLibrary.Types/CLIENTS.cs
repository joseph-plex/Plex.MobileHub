using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

using Plex.Data;
using Pmh.ServiceLibrary;
namespace Pmh.ServiceLibrary.Types
{
    [DataContract]
    public class CLIENTS : __TypeBase
    {
        [DataMember]
        public int CLIENT_ID { get; set; }
        [DataMember]
        public string CLIENT_KEY { get; set; }
        [DataMember]
        public string DESCRIPTION { get; set; }
        [DataMember]
        public int? CLIENT_INSTANCE_ID { get; set; }
        [DataMember]
        public string CLIENT_IP_ADDRESS { get; set; }
        [DataMember]
        public int? CLIENT_PORT { get; set; }
        [DataMember]
        //warning this needs to have a unique key in the database
        public string CLIENT_TOKEN {get;set;}

        public CLIENTS() : base() {
            primaryKeys.Add("CLIENT_ID");
        }
        public CLIENTS(PlexQueryResultTuple plexTuple) :
            this()
        {
            FromPlexQueryResultTuple(this, plexTuple);
        }

    }
}
