using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plex.MobileHub.ServiceLibraries.ClientServiceLibrary
{
    public class DefaultClientCallback : IClientCallback
    {
        public delegate void iud();
        public iud strategy;

        public virtual void IUD()
        {
            strategy();
        }

        public virtual QryResult Query(string connectionString, string query, params object[] arguments)
        {
            throw new NotImplementedException();
        }

        public virtual RegisteredQueryResult ExecuteRegisteredQuery(string connetionString, string queryName, DateTime? time = null)
        {
            throw new NotImplementedException();
        }

        public virtual void Synchronize()
        {
            throw new NotImplementedException();
        }

        public virtual void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
