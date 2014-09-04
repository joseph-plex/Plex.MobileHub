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
    public class APPS : __TypeBase
    {
        [DataMember]
        public int APP_ID { get; set; }
        [DataMember]
        public string AUTH_KEY { get; set; }
        [DataMember]
        public string TITLE { get; set; }
        [DataMember]
        public string DESCRIPTION { get; set; }
        [DataMember]
        public int IS_CLIENT_CUSTOM_APP { get; set; }


        public APPS() : base() {
            primaryKeys.Add("APP_ID");
        }
        public APPS(PlexQueryResultTuple plexTuple) :
            this()
        {
            FromPlexQueryResultTuple(this, plexTuple);
        }
    }
}
