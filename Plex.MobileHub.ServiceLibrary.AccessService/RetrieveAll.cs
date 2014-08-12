using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plex.Data;
using System.Reflection;
using Plex.MobileHub.ServiceLibrary.Types;
namespace Plex.MobileHub.ServiceLibrary.AccessService
{
    public class RetrieveAll : MethodStrategyBase<__TypeBase[]>
    {

        public static T[] ToArray<T>(Object enumerable)
        {
            return ((IEnumerable<T>)enumerable).ToArray();
        }

        public __TypeBase[] Strategy(String typeName)
        {
            var type = Repositories.Keys.First(p => p.FullName == typeName);
            var Retrieve = Repositories[type].GetType().GetMethod("RetrieveAll");
            var Enumerate = GetType().GetMethod("ToArray").MakeGenericMethod(type);
            var result = Retrieve.Invoke(Repositories[type], null);
            return (__TypeBase[])Enumerate.Invoke(null, new Object[] { result });
        
        }
    }
}
