using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Plex.MobileHub.ServiceLibraries.ClientServiceLibrary;
using Plex.MobileHub.Data;
using Plex.MobileHub.Data.Types;
using Plex.MobileHub.ServiceLibraries;

namespace Plex.MobileHub
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Client" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Client.svc or Client.svc.cs at the Solution Explorer and start debugging.
    public class ClientService :  IClientService
    {
        public IRepository<APP_QUERIES> APP_QUERIES
        {
            get
            {

                return app_queries;
            }
            set
            {
                app_queries = value;
                if (RepositoryChangedEvent != null)
                    RepositoryChangedEvent(this, EventArgs.Empty);
            }
        }
        public IRepository<APP_QUERY_COLUMNS> APP_QUERY_COLUMNS
        {
            get
            {
                return app_query_columns;
            }
            set
            {
                app_query_columns = value;
                if (RepositoryChangedEvent != null)
                    RepositoryChangedEvent(this, EventArgs.Empty);
            }
        }
        public IRepository<APP_QUERY_CONDITIONS> APP_QUERY_CONDITIONS
        {
            get
            {
                return app_query_conditions;
            }
            set
            {
                app_query_conditions = value;
                if (RepositoryChangedEvent != null)
                    RepositoryChangedEvent(this, EventArgs.Empty);
            }
        }
        public IRepository<APP_TABLE_COLUMNS> APP_TABLE_COLUMNS
        {
            get
            {
                return app_table_columns;
            }
            set
            {
                app_table_columns = value;
                if (RepositoryChangedEvent != null)
                    RepositoryChangedEvent(this, EventArgs.Empty);
            }
        }
        public IRepository<APP_TABLES> APP_TABLES
        {
            get
            {
                return app_tables;
            }
            set
            {
                app_tables = value;
                if (RepositoryChangedEvent != null)
                    RepositoryChangedEvent(this, EventArgs.Empty);
            }
        }
        public IRepository<APP_USER_TYPES> APP_USER_TYPES
        {
            get
            {
                return app_user_types;
            }
            set
            {
                app_user_types = value;
                if (RepositoryChangedEvent != null)
                    RepositoryChangedEvent(this, EventArgs.Empty);
            }
        }
        public IRepository<APPS> APPS
        {
            get
            {
                return apps;
            }
            set
            {
                apps = value;
                if (RepositoryChangedEvent != null)
                    RepositoryChangedEvent(this, EventArgs.Empty);
            }
        }
        public IRepository<CLIENT_APPS> CLIENT_APPS
        {
            get
            {
                return client_apps;
            }
            set
            {
                client_apps = value;
                if (RepositoryChangedEvent != null)
                    RepositoryChangedEvent(this, EventArgs.Empty);
            }
        }
        public IRepository<CLIENT_DB_COMPANIES> CLIENT_DB_COMPANIES
        {
            get
            {
                return client_db_companies;
            }
            set
            {
                client_db_companies = value;
                if (RepositoryChangedEvent != null)
                    RepositoryChangedEvent(this, EventArgs.Empty);
            }
        }
        public IRepository<CLIENT_DB_COMPANY_USER_APPS> CLIENT_DB_COMPANY_USER_APPS
        {
            get
            {
                return client_db_company_user_apps;
            }
            set
            {
                client_db_company_user_apps = value;
                if (RepositoryChangedEvent != null)
                    RepositoryChangedEvent(this, EventArgs.Empty);
            }
        }
        public IRepository<CLIENT_DB_COMPANY_USERS> CLIENT_DB_COMPANY_USERS
        {
            get
            {
                return client_db_company_users;
            }
            set
            {
                client_db_company_users = value;
                if (RepositoryChangedEvent != null)
                    RepositoryChangedEvent(this, EventArgs.Empty);
            }
        }
        public IRepository<CLIENT_USERS> CLIENT_USERS
        {
            get
            {
                return client_users;
            }
            set
            {
                client_users = value;
                if (RepositoryChangedEvent != null)
                    RepositoryChangedEvent(this, EventArgs.Empty);
            }
        }
        public IRepository<CLIENTS> CLIENTS
        {
            get
            {
                return clients;
            }
            set
            {
                clients = value;
                if (RepositoryChangedEvent != null)
                    RepositoryChangedEvent(this, EventArgs.Empty);
            }
        }
        public IRepository<DEV_DATA> DEV_DATA
        {
            get
            {
                return dev_data;
            }
            set
            {
                dev_data = value;
                if (RepositoryChangedEvent != null)
                    RepositoryChangedEvent(this, EventArgs.Empty);
            }
        }
        public IRepository<DEV_DATA_VER> DEV_DATA_VER
        {
            get
            {
                return dev_data_ver;

            }
            set
            {
                dev_data_ver = value;
                if (RepositoryChangedEvent != null)
                    RepositoryChangedEvent(this, EventArgs.Empty);
            }
        }
        public IRepository<DEV_DATA_VER_QUERIES> DEV_DATA_VER_QUERIES
        {
            get
            {
                return dev_data_ver_queries;
            }
            set
            {
                dev_data_ver_queries = value;
                if (RepositoryChangedEvent != null)
                    RepositoryChangedEvent(this, EventArgs.Empty);
            }
        }
        public IRepository<DEVICE_USER_DATA> DEVICE_USER_DATA
        {
            get
            {
                return device_user_data;
            }
            set
            {
                device_user_data = value;
                if (RepositoryChangedEvent != null)
                    RepositoryChangedEvent(this, EventArgs.Empty);
            }
        }
        public IRepository<DEVICE_USER_DATA_QUERIES> DEVICE_USER_DATA_QUERIES
        {
            get
            {
                return device_user_data_queries;
            }
            set
            {
                device_user_data_queries = value;
                if (RepositoryChangedEvent != null)
                    RepositoryChangedEvent(this, EventArgs.Empty);
            }
        }
        public IRepository<DEVICES> DEVICES
        {
            get
            {
                return devices;
            }
            set
            {
                devices = value;
                if (RepositoryChangedEvent != null)
                    RepositoryChangedEvent(this, EventArgs.Empty);
            }
        }
        public IRepository<LOGS> LOGS
        {
            get
            {
                return logs;
            }
            set
            {
                logs = value;
                if (RepositoryChangedEvent != null)
                    RepositoryChangedEvent(this, EventArgs.Empty);
            }
        }
        public IRepository<PMH_SETTINGS> PMH_SETTINGS
        {
            get
            {
                return pmh_settings;
            }
            set
            {
                pmh_settings = value;
                if (RepositoryChangedEvent != null)
                    RepositoryChangedEvent(this, EventArgs.Empty);
            }
        }
        public IRepository<QUERY_SEQUENCE_REQUESTS> QUERY_SEQUENCE_REQUESTS
        {
            get
            {
                return query_sequence_requests;
            }
            set
            {
                query_sequence_requests = value;
                if (RepositoryChangedEvent != null)
                    RepositoryChangedEvent(this, EventArgs.Empty);
            }
        }

        public IRepository<Consumer> Consumers
        {
            get
            {
                return consumers;
            }
            set
            {
                consumers = value;
            }
        }
        public IRepository<ClientInformation> ClientInfo
        {
            get
            {
                return clientInfo;
            }
            set
            {
                clientInfo = value;
            }
        }

        event EventHandler RepositoryChangedEvent;

        IRepository<APP_QUERIES> app_queries;
        IRepository<APP_QUERY_COLUMNS> app_query_columns;
        IRepository<APP_QUERY_CONDITIONS> app_query_conditions;
        IRepository<APP_TABLE_COLUMNS> app_table_columns;
        IRepository<APP_TABLES> app_tables;
        IRepository<APP_USER_TYPES> app_user_types;
        IRepository<APPS> apps;
        IRepository<CLIENT_APPS> client_apps;
        IRepository<CLIENT_DB_COMPANIES> client_db_companies;
        IRepository<CLIENT_DB_COMPANY_USER_APPS> client_db_company_user_apps;
        IRepository<CLIENT_DB_COMPANY_USERS> client_db_company_users;
        IRepository<CLIENT_USERS> client_users;
        IRepository<CLIENTS> clients;
        IRepository<DEV_DATA> dev_data;
        IRepository<DEV_DATA_VER> dev_data_ver;
        IRepository<DEV_DATA_VER_QUERIES> dev_data_ver_queries;
        IRepository<DEVICE_USER_DATA> device_user_data;
        IRepository<DEVICE_USER_DATA_QUERIES> device_user_data_queries;
        IRepository<DEVICES> devices;
        IRepository<LOGS> logs;
        IRepository<PMH_SETTINGS> pmh_settings;
        IRepository<QUERY_SEQUENCE_REQUESTS> query_sequence_requests;
        IRepository<Consumer> consumers;
        IRepository<ClientInformation> clientInfo;


        LogIn logIn;

        public ClientService()
        {
            CLIENTS = new OracleRepository<CLIENTS>();
            ClientInfo = Singleton<InMemoryRepository<ClientInformation>>.Instance;
            
            logIn = new LogIn();
            RepositoryChangedEvent += (s, e) => init();
            init();
        }

        void init()
        {
            logIn.ClientsRepository = CLIENTS;
            logIn.ClientInfoRepository = ClientInfo;
        }
        public IClientCallback ClientCallback
        {
            get
            {
                return OperationContext.Current.GetCallbackChannel<IClientCallback>();
            }
        }

        public void LogIn(Int32 clientId, String clientKey)
        {
            logIn.Strategy(clientId, clientKey, ClientCallback);
        }

        public void LogOut(){
            throw new NotImplementedException();
        }

        public void Dispose() {
            throw new NotImplementedException();
        }


        public IEnumerable<APPS> GetAllAPPS()
        {
            return APPS.RetrieveAll();
        }

        public APPS SelectAPPS(Predicate<APPS> predicate)
        {
            return APPS.Retrieve(predicate);
        }

        public IEnumerable<APP_QUERIES> GetAllAPP_QUERIES()
        {
            return APP_QUERIES.RetrieveAll();
        }

        public APP_QUERIES SelectAPP_QUERIES(Predicate<APP_QUERIES> predicate)
        {
            return APP_QUERIES.Retrieve(predicate);
        }

        public IEnumerable<APP_QUERY_COLUMNS> GetAllAPP_QUERY_COLUMNS()
        {
            return APP_QUERY_COLUMNS.RetrieveAll();
        }

        public APP_QUERY_COLUMNS SelectAPP_QUERY_COLUMNS(Predicate<APP_QUERY_COLUMNS> predicate)
        {
            return APP_QUERY_COLUMNS.Retrieve(predicate);
        }

        public IEnumerable<CLIENT_APPS> GetAllCLIENT_APPS()
        {
            return CLIENT_APPS.RetrieveAll();
        }

        public CLIENT_APPS SelectCLIENT_APPS(Predicate<CLIENT_APPS> predicate)
        {
            return CLIENT_APPS.Retrieve(predicate);
        }

        public IEnumerable<CLIENT_DB_COMPANIES> GetAllCLIENT_DB_COMPANIES()
        {
            return CLIENT_DB_COMPANIES.RetrieveAll();
        }

        public CLIENT_DB_COMPANIES SelectCLIENT_DB_COMPANIES(Predicate<CLIENT_DB_COMPANIES> predicate)
        {
            return CLIENT_DB_COMPANIES.Retrieve(predicate);
        }

        public void InsertCLIENT_DB_COMPANIES(CLIENT_DB_COMPANIES value)
        {
            CLIENT_DB_COMPANIES.Insert(value);
        }

        public void UpdateCLIENT_DB_COMPANIES(CLIENT_DB_COMPANIES value)
        {
            CLIENT_DB_COMPANIES.Update(value);
        }

        public void DeleteCLIENT_DB_COMPANIES(Predicate<CLIENT_DB_COMPANIES> predicate)
        {
            CLIENT_DB_COMPANIES.Delete(predicate);
        }

        public IEnumerable<CLIENT_DB_COMPANY_USERS> GetAllCLIENT_DB_COMPANY_USERS()
        {
            return CLIENT_DB_COMPANY_USERS.RetrieveAll();
        }

        public CLIENT_DB_COMPANY_USERS SelectCLIENT_DB_COMPANY_USERS(Predicate<CLIENT_DB_COMPANY_USERS> predicate)
        {
            return CLIENT_DB_COMPANY_USERS.Retrieve(predicate);
        }

        public void InsertCLIENT_DB_COMPANY_USERS(CLIENT_DB_COMPANY_USERS value)
        {
            CLIENT_DB_COMPANY_USERS.Insert(value);
        }

        public void UpdateCLIENT_DB_COMPANY_USERS(CLIENT_DB_COMPANY_USERS value)
        {
            CLIENT_DB_COMPANY_USERS.Update(value);
        }

        public void DeleteCLIENT_DB_COMPANY_USERS(Predicate<CLIENT_DB_COMPANY_USERS> predicate)
        {
            CLIENT_DB_COMPANY_USERS.Delete(predicate);
        }

        public IEnumerable<CLIENT_DB_COMPANY_USER_APPS> GetAllCLIENT_DB_COMPANY_USER_APPS()
        {
            return CLIENT_DB_COMPANY_USER_APPS.RetrieveAll();
        }

        public CLIENT_DB_COMPANY_USER_APPS SelectCLIENT_DB_COMPANY_USER_APPS(Predicate<CLIENT_DB_COMPANY_USER_APPS> predicate)
        {
            return CLIENT_DB_COMPANY_USER_APPS.Retrieve(predicate);
        }

        public void InsertCLIENT_DB_COMPANY_USER_APPS(CLIENT_DB_COMPANY_USER_APPS value)
        {
            CLIENT_DB_COMPANY_USER_APPS.Insert(value);
        }

        public void UpdateCLIENT_DB_COMPANY_USER_APPS(CLIENT_DB_COMPANY_USER_APPS value)
        {
            CLIENT_DB_COMPANY_USER_APPS.Update(value);
        }

        public void DeleteCLIENT_DB_COMPANY_USER_APPS(Predicate<CLIENT_DB_COMPANY_USER_APPS> predicate)
        {
            CLIENT_DB_COMPANY_USER_APPS.Retrieve(predicate);
        }

        public IEnumerable<CLIENT_USERS> GetAllCLIENT_USERS()
        {
            return CLIENT_USERS.RetrieveAll();
        }

        public CLIENT_USERS SelectCLIENT_USERS(Predicate<CLIENT_USERS> predicate)
        {
            return CLIENT_USERS.Retrieve(predicate);
        }

        public void InsertCLIENT_USERS(CLIENT_USERS value)
        {
            CLIENT_USERS.Insert(value);
        }

        public void UpdateCLIENT_USERS(CLIENT_USERS value)
        {
            CLIENT_USERS.Update(value);
        }

        public void DeleteCLIENT_USERS(Predicate<CLIENT_USERS> predicate)
        {
            CLIENT_USERS.Delete(predicate);
        }

        public IEnumerable<LOGS> GetAllLOGS()
        {
            return LOGS.RetrieveAll();
        }

        public LOGS SelectLOGS(Predicate<LOGS> predicate)
        {
            return LOGS.Retrieve(predicate);
        }

        public void InsertLOGS(LOGS value)
        {
            LOGS.Insert(value);
        }
    }
}
