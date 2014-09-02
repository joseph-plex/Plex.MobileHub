﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

using Plex.Data;
using Plex.MobileHub.ServiceLibrary;
namespace Plex.MobileHub.ServiceLibrary.Types
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