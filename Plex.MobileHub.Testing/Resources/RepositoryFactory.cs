using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plex.Data;
using Plex.MobileHub.ServiceLibrary;
using Plex.MobileHub.ServiceLibrary.Types;


namespace Plex.MobileHub.Testing.Resources
{
    public class RepositoryFactory
    {
        protected Dictionary<Type, Object> Repositories { get; set; }

        public RepositoryFactory()
        {

            Repositories = new Dictionary<Type, Object>();
            Repositories.Add(typeof(APP_QUERIES), new InMemoryRepository<APP_QUERIES>());
            Repositories.Add(typeof(APP_QUERY_COLUMNS), new InMemoryRepository<APP_QUERY_COLUMNS>());
            Repositories.Add(typeof(APP_QUERY_CONDITIONS), new InMemoryRepository<APP_QUERY_CONDITIONS>());
            Repositories.Add(typeof(APP_TABLE_COLUMNS), new InMemoryRepository<APP_TABLE_COLUMNS>());

            Repositories.Add(typeof(APP_TABLES), new InMemoryRepository<APP_TABLES>());
            Repositories.Add(typeof(APP_USER_TYPES), new InMemoryRepository<APP_USER_TYPES>());
            Repositories.Add(typeof(APPS), new InMemoryRepository<APPS>());
            Repositories.Add(typeof(CLIENT_APPS), new InMemoryRepository<CLIENT_APPS>());


            Repositories.Add(typeof(CLIENT_DB_COMPANIES), new InMemoryRepository<CLIENT_DB_COMPANIES>());
            Repositories.Add(typeof(CLIENT_DB_COMPANY_USER_APPS), new InMemoryRepository<CLIENT_DB_COMPANY_USER_APPS>());
            Repositories.Add(typeof(CLIENT_DB_COMPANY_USERS), new InMemoryRepository<CLIENT_DB_COMPANY_USERS>());
            Repositories.Add(typeof(CLIENT_USERS), new InMemoryRepository<CLIENT_USERS>());


            Repositories.Add(typeof(CLIENTS), new InMemoryRepository<CLIENTS>());
            Repositories.Add(typeof(DEV_DATA), new InMemoryRepository<DEV_DATA>());
            Repositories.Add(typeof(DEV_DATA_VER), new InMemoryRepository<DEV_DATA_VER>());
            Repositories.Add(typeof(DEV_DATA_VER_QUERIES), new InMemoryRepository<DEV_DATA_VER_QUERIES>());


            Repositories.Add(typeof(DEVICE_USER_DATA), new InMemoryRepository<DEVICE_USER_DATA>());
            Repositories.Add(typeof(DEVICE_USER_DATA_QUERIES), new InMemoryRepository<DEVICE_USER_DATA_QUERIES>());
            Repositories.Add(typeof(DEVICES), new InMemoryRepository<DEVICES>());
            Repositories.Add(typeof(LOGS), new InMemoryRepository<LOGS>());

            Repositories.Add(typeof(PMH_SETTINGS), new InMemoryRepository<PMH_SETTINGS>());
            Repositories.Add(typeof(QUERY_SEQUENCE_REQUESTS), new InMemoryRepository<QUERY_SEQUENCE_REQUESTS>());

            Repositories.Add(typeof(ClientInformation),new InMemoryRepository<ClientInformation>());
            Repositories.Add(typeof(ConsumerInformation), new InMemoryRepository<ConsumerInformation>());
        }
        public virtual IRepository<C> GetRepository<C>() where C : IRepositoryEntry, new()
        {
            Dictionary<Type, Object> r;
            if (!(r = Repositories).ContainsKey(typeof(C)))
                throw new NotSupportedException();
            return r[typeof(C)] as IRepository<C>;
        }

        public Dictionary<Type, Object> GetRepositories()
        {
            return Repositories;
        }
    }
}
