using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plex.Data;
using System.Reflection;

namespace Plex.MobileHub.ServiceLibrary
{
    public abstract class MethodStrategyBase<T>  : IMethodStrategy<T>
    {
        public virtual Dictionary<Type, Object> Repositories { get { return repositories; } set { repositories = value; } }
        private Dictionary<Type, Object> repositories = new Dictionary<Type, Object>();
        public virtual IRepository<C> GetRepository<C>()
           where C : IRepositoryEntry, new()
        {
            Dictionary<Type, Object> r;
            if (!(r = Repositories).ContainsKey(typeof(C)))
                throw new NotSupportedException();
            return r[typeof(C)] as IRepository<C>;
        }

        public static Boolean AnyNull(params object[] arguments)
        {
            foreach (var o in arguments)
                if (o == null)
                    return true;
            return false;
        }

        public virtual T Strategy(params object[] parameters)
        {
            String MemberName = MethodBase.GetCurrentMethod().Name;
            T result = (T)GetType().InvokeMember(MemberName, BindingFlags.InvokeMethod | BindingFlags.Public, Type.DefaultBinder, this, parameters);
            return result;
        }
      
    }
}
