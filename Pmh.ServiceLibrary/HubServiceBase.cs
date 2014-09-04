using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Plex.Data;
using Pmh.ServiceLibrary.Types;

namespace Pmh.ServiceLibrary
{
    public abstract class HubServiceBase
    {
        protected Dictionary<Type, Object> Repositories { get { return repositories; } set { repositories = value; } }
        Dictionary<Type, Object> repositories;
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
            repositories = new Dictionary<Type, Object>();
        }
    }
}
