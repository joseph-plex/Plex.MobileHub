using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plex.MobileHub.Data.Types
{
    public class DEVICE_USER_DATA : RepositoryEntryBase, IRepositoryEntry
    {
        public int USER_DATA_ID { get; set; }
        public int? DEVICE_ID { get; set; }
        public int USER_PERMISSION { get; set; }
        public DateTime? EXECUTION_INITIATION { get; set; }
        public DateTime? EXECUTION_COMPLETION { get; set; }

        public DEVICE_USER_DATA() : base() {
            PrimaryKeys.Add("USER_DATA_ID");
        }
        public DEVICE_USER_DATA(PlexQueryResultTuple plexTuple) : base(plexTuple) { }

    }
}
