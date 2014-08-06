using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plex.Data;

namespace Plex.MobileHub.ServiceLibrary
{
    public class ConsumerInformation : RepositoryEntryBase, IRepositoryEntry
    {
        public int ConsumerId { get; set; }
        public int AppId { get; set; }
        public int ClientId { get; set; }
        public String DatabaseCode { get; set; }
        public int UserId { get; set; }

        public ConsumerInformation()
            : base()
        {
            primaryKeys.Add("ConsumerId");
        }
    }
}
