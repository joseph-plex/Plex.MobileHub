using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
namespace Plex.MobileHub.ServiceLibraries
{
    public abstract class  MethodStrategyBase<T> : IMethodStrategy<T>
    {
        public static Boolean AnyNull(params object[] arguments)
        {
            foreach (var o in arguments)
                if (o == null)
                    return true;
            return false;
        }

        public virtual T Strategy(params object[] parameters)
        {
            Type type = GetType();
            String MemberName = MethodBase.GetCurrentMethod().Name;
            T result = (T)type.InvokeMember(MemberName, BindingFlags.InvokeMethod | BindingFlags.Public, Type.DefaultBinder, this, parameters);
            return result;
        }

    }
}
