using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Plex.Data;
namespace Plex.MobileHub.ServiceLibrary.Types
{
    public class DEVICES : RepositoryEntryBase, IRepositoryEntry
    {
        public int DEVICE_ID { get; set; }
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
