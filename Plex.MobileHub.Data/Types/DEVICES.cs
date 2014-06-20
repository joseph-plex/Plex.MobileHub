using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plex.MobileHub.Data.Types
{
    public class DEVICES : IRepositoryEntry
    {
        public int DEVICE_ID { get; set; }
        public int APP_ID { get; set; }
    }
}
