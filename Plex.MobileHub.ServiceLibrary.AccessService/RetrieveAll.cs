using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plex.Data;
using System.Reflection;
namespace Plex.MobileHub.ServiceLibrary.AccessService
{
    public class RetrieveAll : MethodStrategyBase<IRepositoryEntry[]>
    {
        public IRepositoryEntry[] Strategy(String typeName)
        {
            var type = Repositories.Keys.First(p => p.FullName == typeName);
            var Retrieve = Repositories[type].GetType().GetMethod("Insert", new Type[] { type });
            return (IRepositoryEntry[]) Retrieve.Invoke(Repositories[type], null);
        }
    }
}
