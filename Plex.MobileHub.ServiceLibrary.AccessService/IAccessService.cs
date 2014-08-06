using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Plex.MobileHub.ServiceLibrary.AccessService;
using Plex.Data;
using Plex.MobileHub.ServiceLibrary.Types;
namespace Plex.MobileHub.ServiceLibrary.AccessService
{

    public interface IAccessService 
    {

        //Get the PrimaryKeys of the Type
        //IList<String> GetPrimaryKey(String TypeName);
        //void Insert(String TypeName, Expression)
        

        //event EventHandler<RepositoryOperationEventArgs> InsertEvent;
        //event EventHandler<RepositoryOperationEventArgs> UpdateEvent;
        //event EventHandler<RepositoryOperationEventArgs> DeleteEvent;
        IList<String> GetPrimayKeys(String typeName);
        void Insert(String typeName, Object Entry);
        void Update(String typeName, Object Entry);
        void Delete(String typeName, Object[] Entry);
        Object[] RetrieveAll(String TypeName);


        //IList<String> PrimaryKeys { get; }

        //void Insert(APP_QUERIES Entry);
        //void Update(T Entry);
        //void Delete(Predicate<T> predicate);
        //bool Exists(Predicate<T> predicate);
        //T Retrieve(Predicate<T> predicate);

        //int Count();
        //int Count(Predicate<T> predicate);

        //IEnumerable<T> RetrieveAll();

        //repositories = new Dictionary<Type, Object>();
        //repositories.Add(typeof(APP_QUERIES), new OracleRepository<APP_QUERIES>());
        //repositories.Add(typeof(APP_QUERY_COLUMNS), new OracleRepository<APP_QUERY_COLUMNS>());
        //repositories.Add(typeof(APP_QUERY_CONDITIONS), new OracleRepository<APP_QUERY_CONDITIONS>());
        //repositories.Add(typeof(APP_TABLE_COLUMNS), new OracleRepository<APP_TABLE_COLUMNS>());

        //repositories.Add(typeof(APP_TABLES), new OracleRepository<APP_TABLES>());
        //repositories.Add(typeof(APP_USER_TYPES), new OracleRepository<APP_USER_TYPES>());
        //repositories.Add(typeof(APPS), new OracleRepository<APPS>());
        //repositories.Add(typeof(CLIENT_APPS), new OracleRepository<CLIENT_APPS>());


        //repositories.Add(typeof(CLIENT_DB_COMPANIES), new OracleRepository<CLIENT_DB_COMPANIES>());
        //repositories.Add(typeof(CLIENT_DB_COMPANY_USER_APPS), new OracleRepository<CLIENT_DB_COMPANY_USER_APPS>());
        //repositories.Add(typeof(CLIENT_DB_COMPANY_USERS), new OracleRepository<CLIENT_DB_COMPANY_USERS>());
        //repositories.Add(typeof(CLIENT_USERS), new OracleRepository<CLIENT_USERS>());


        //repositories.Add(typeof(CLIENTS), new OracleRepository<CLIENTS>());
        //repositories.Add(typeof(DEV_DATA), new OracleRepository<DEV_DATA>());
        //repositories.Add(typeof(DEV_DATA_VER), new OracleRepository<DEV_DATA_VER>());
        //repositories.Add(typeof(DEV_DATA_VER_QUERIES), new OracleRepository<DEV_DATA_VER_QUERIES>());


        //repositories.Add(typeof(DEVICE_USER_DATA), new OracleRepository<DEVICE_USER_DATA>());
        //repositories.Add(typeof(DEVICE_USER_DATA_QUERIES), new OracleRepository<DEVICE_USER_DATA_QUERIES>());
        //repositories.Add(typeof(DEVICES), new OracleRepository<DEVICES>());
        //repositories.Add(typeof(LOGS), new OracleRepository<LOGS>());

        //repositories.Add(typeof(PMH_SETTINGS), new OracleRepository<PMH_SETTINGS>());
        //repositories.Add(typeof(QUERY_SEQUENCE_REQUESTS), new OracleRepository<QUERY_SEQUENCE_REQUESTS>());

        //repositories.Add(typeof(ClientInformation), Singleton<InMemoryRepository<ClientInformation>>.Instance);
        //repositories.Add(typeof(ConsumerInformation), Singleton<InMemoryRepository<ConsumerInformation>>.Instance);

    }
}
