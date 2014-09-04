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
    public class DEV_DATA : __TypeBase
    {
        [DataMember]
        public int DEVICE_DATABASE_ID { get; set; } //DEVICE_DATABASE_ID	NUMBER(10)	N
        [DataMember]
        public int USER_ID { get; set; } //USER_ID	NUMBER(10)	N	
        [DataMember]
        public int APP_ID { get; set; } //APP_ID	NUMBER(10)	N	
        [DataMember]
        public int CLIENT_ID { get; set; } //CLIENT_ID	NUMBER(10)	N	

        public DEV_DATA() : base() 
        {
            primaryKeys.Add("DEVICE_DATABASE_ID");
        }
        public DEV_DATA(PlexQueryResultTuple plexTuple) :
            this()
        {
            FromPlexQueryResultTuple(this, plexTuple);
        }
    }
}
