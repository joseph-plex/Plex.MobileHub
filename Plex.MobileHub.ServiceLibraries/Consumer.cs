using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Plex.MobileHub.Data;
using Plex.MobileHub.Data.Types;
namespace Plex.MobileHub.ServiceLibraries
{
    public class Consumer : IRepositoryEntry
    {
        public int ConsumerId { get; set; }
        public int ClientId { get; set; }
        public string DatabaseCode { get; set; }
        public int AppId { get; set; }
        public int UserId { get; set; }

        public IList<String> GetPrimaryKeys()
        {
            return new List<String>(new String[] { "ConsumerId" });
        }
    }
}
