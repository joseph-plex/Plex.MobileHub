using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Plex.MobileHub.Data;
using Plex.MobileHub.ServiceLibraries.ClientServiceLibrary;
namespace Plex.MobileHub.ServiceLibraries
{
    public class ClientInformation : IRepositoryEntry
    {
        public Int32 ClientId { get; set; }
        public IClientCallback Callback {get; set; }
        public IList<String> GetPrimaryKeys()
        {
            List<String> keys = new List<string>();
            keys.Add("ClientId");
            return keys;
        }
    }
}
