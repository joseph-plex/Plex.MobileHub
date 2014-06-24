using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plex.MobileHub.Data
{
    public class RepositoryEntryBase
    {
        public virtual IList<String> PrimaryKeys { get; protected set; }

        public RepositoryEntryBase() 
        {
            PrimaryKeys = new List<String>();
        }
        public RepositoryEntryBase(PlexQueryResultTuple plexTuple) : this()
        {
            if (plexTuple.parent == null)
                throw new NotSupportedException("This Operation is Not supported by this PlexTuple.");

            Type type = GetType();
            var pInfo = type.GetProperties();
            PlexQueryResult result = plexTuple.parent;

            foreach(var p in pInfo)
            {
                String name = p.Name;
                int index = result.Tuples.IndexOf(plexTuple);
                var conversationType = Nullable.GetUnderlyingType(p.PropertyType) ?? p.PropertyType;
                var value = Convert.ChangeType(result[p.Name, index], conversationType) ;
                p.SetValue(this, value);
            }
        }
    }
}
