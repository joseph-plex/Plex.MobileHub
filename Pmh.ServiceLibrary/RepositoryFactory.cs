using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pmh.ServiceLibrary.Types;

namespace Pmh.ServiceLibrary
{
    public class RepositoryFactory : IDisposable
    {

        Dictionary<Type, Object> repositories;
        public Boolean Disposed { get; protected set; } 

        public RepositoryFactory()
        {
            Disposed = false;
            repositories = new Dictionary<Type, Object>();
            repositories.Add(typeof(APP_QUERIES), new OracleRepository<APP_QUERIES>());
            repositories.Add(typeof(APP_QUERY_COLUMNS), new OracleRepository<APP_QUERY_COLUMNS>());
            repositories.Add(typeof(APP_QUERY_CONDITIONS), new OracleRepository<APP_QUERY_CONDITIONS>());
            repositories.Add(typeof(APP_TABLE_COLUMNS), new OracleRepository<APP_TABLE_COLUMNS>());

            repositories.Add(typeof(APP_TABLES), new OracleRepository<APP_TABLES>());
            repositories.Add(typeof(APP_USER_TYPES), new OracleRepository<APP_USER_TYPES>());
            repositories.Add(typeof(APPS), new OracleRepository<APPS>());
            repositories.Add(typeof(CLIENT_APPS), new OracleRepository<CLIENT_APPS>());


            repositories.Add(typeof(CLIENT_DB_COMPANIES), new OracleRepository<CLIENT_DB_COMPANIES>());
            repositories.Add(typeof(CLIENT_DB_COMPANY_USER_APPS), new OracleRepository<CLIENT_DB_COMPANY_USER_APPS>());
            repositories.Add(typeof(CLIENT_DB_COMPANY_USERS), new OracleRepository<CLIENT_DB_COMPANY_USERS>());
            repositories.Add(typeof(CLIENT_USERS), new OracleRepository<CLIENT_USERS>());


            repositories.Add(typeof(CLIENTS), new OracleRepository<CLIENTS>());
            repositories.Add(typeof(DEV_DATA), new OracleRepository<DEV_DATA>());
            repositories.Add(typeof(DEV_DATA_VER), new OracleRepository<DEV_DATA_VER>());
            repositories.Add(typeof(DEV_DATA_VER_QUERIES), new OracleRepository<DEV_DATA_VER_QUERIES>());


            repositories.Add(typeof(DEVICE_USER_DATA), new OracleRepository<DEVICE_USER_DATA>());
            repositories.Add(typeof(DEVICE_USER_DATA_QUERIES), new OracleRepository<DEVICE_USER_DATA_QUERIES>());
            repositories.Add(typeof(DEVICES), new OracleRepository<DEVICES>());
            repositories.Add(typeof(LOGS), new OracleRepository<LOGS>());

            repositories.Add(typeof(PMH_SETTINGS), new OracleRepository<PMH_SETTINGS>());
            repositories.Add(typeof(QUERY_SEQUENCE_REQUESTS), new OracleRepository<QUERY_SEQUENCE_REQUESTS>());

            repositories.Add(typeof(ClientCallback), Singleton<InMemoryRepository<ClientCallback>>.Instance);
            repositories.Add(typeof(ConsumerInformation), Singleton<InMemoryRepository<ConsumerInformation>>.Instance);
        }

        public Dictionary<Type, Object> GetRepositories()
        {
            return repositories;
        }

        protected virtual void Dispose(Boolean disposing)
        {
            if (Disposed)
            {
                if(disposing)
                {
                    foreach(var obj in repositories.Values)
                    {
                        var repo = obj as OracleRepository;
                        if (repo != null)
                            repo.Close();
                    }
                }
                Disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #region Finalizer
        ~RepositoryFactory()
        {
            Dispose(false);
        }
        #endregion
    }
}
