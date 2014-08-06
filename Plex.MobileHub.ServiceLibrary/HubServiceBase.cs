using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Plex.Data;
using Plex.MobileHub.ServiceLibrary.Types;

namespace Plex.MobileHub.ServiceLibrary
{
    public abstract class HubServiceBase
    {
        protected Dictionary<Type, Object> Repositories { get; set; }
        public HubServiceBase()
        {
            Repositories = new Dictionary<Type, Object>();
        }
    }
}
