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
        public virtual IRepository<C> GetRepository<C>()
           where C : IRepositoryEntry, new()
        {
            Dictionary<Type, Object> r;
            if (!(r = Repositories).ContainsKey(typeof(C)))
                throw new NotSupportedException();
            return r[typeof(C)] as IRepository<C>;
        }

        public HubServiceBase()
        {
            Repositories = new Dictionary<Type, Object>();
        }
    }
}
