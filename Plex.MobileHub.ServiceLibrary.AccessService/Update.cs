using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Plex.MobileHub.ServiceLibrary.AccessService
{

    public class Update : MethodStrategyBase<Object>
    {
        public static T Cast<T>(object o)
        {
            return (T)o;
        }
        public void Strategy(String typeName, object entry)
        {
            var type = Repositories.Keys.First(p => p.FullName == typeName);
            var Update = Repositories[type].GetType().GetMethod("Update");
            var castMethod = GetType().GetMethod("Cast").MakeGenericMethod(type);
            Update.Invoke(Repositories[type], new object[] { entry });
        }
    }
}
