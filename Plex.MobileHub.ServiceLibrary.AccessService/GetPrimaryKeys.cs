using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plex.MobileHub.ServiceLibrary;
using Plex.Data;

namespace Plex.MobileHub.ServiceLibrary.AccessService
{
    public class GetPrimaryKeys : MethodStrategyBase<IList<String>>
    {
        public IList<string> Strategy(String typeName)
        {
            var keys = Repositories.Keys;
            Type type = keys.First(p => p.FullName == typeName);
            var e = Repositories[type] as IRepositoryEntry;
            return e.GetPrimaryKeys();
        }
    }
}
