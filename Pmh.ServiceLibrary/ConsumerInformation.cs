using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plex.Data;

namespace Pmh.ServiceLibrary
{
    public class ConsumerInformation : RepositoryEntryBase, IRepositoryEntry
    {
        public Int32 ConsumerId { get; set; }
        public Int32 AppId { get; set; }
        public Int32 ClientId { get; set; }
        public String DatabaseCode { get; set; }
        public Int32 UserId { get; set; }

        public ConsumerInformation()
            : base()
        {
            primaryKeys.Add("ConsumerId");
        }
    }
}
