using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Plex.MobileHub.ServiceLibraries.APIServiceLibrary;
using Plex.MobileHub.ServiceLibraries;
using Plex.MobileHub.Data;
using Plex.MobileHub.Data.Types;

namespace Plex.MobileHub
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Api : IApiService
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


        ConnectionConnect connectionConnection;
        ConnectionRelease connectionRelease;
        ConnectionStatus connectionStatus;
        QryExecute qryExecute;
        QryDatabase qryDatabase;
        DeviceRequestId deviceRequestId;

        public Api()
        {

            //Defaults for Repositories
            APP_QUERIES = new OracleRepository<APP_QUERIES>();
            APP_QUERY_COLUMNS = new OracleRepository<APP_QUERY_COLUMNS>();
            APP_QUERY_CONDITIONS = new OracleRepository<APP_QUERY_CONDITIONS>();
            APP_TABLE_COLUMNS = new OracleRepository<APP_TABLE_COLUMNS>();
            APP_TABLES = new OracleRepository<APP_TABLES>();
            APP_USER_TYPES = new OracleRepository<APP_USER_TYPES>();
            APPS = new OracleRepository<APPS>();
            CLIENT_APPS = new OracleRepository<CLIENT_APPS>();
            CLIENT_DB_COMPANIES = new OracleRepository<CLIENT_DB_COMPANIES>();
            CLIENT_DB_COMPANY_USER_APPS = new OracleRepository<CLIENT_DB_COMPANY_USER_APPS>();
            CLIENT_DB_COMPANY_USERS = new OracleRepository<CLIENT_DB_COMPANY_USERS>();
            CLIENT_USERS = new OracleRepository<CLIENT_USERS>();
            CLIENTS = new OracleRepository<CLIENTS>();
            DEV_DATA = new OracleRepository<DEV_DATA>();
            DEV_DATA_VER = new OracleRepository<DEV_DATA_VER>();
            DEV_DATA_VER_QUERIES = new OracleRepository<DEV_DATA_VER_QUERIES>();
            DEVICE_USER_DATA = new OracleRepository<DEVICE_USER_DATA>();
            DEVICE_USER_DATA_QUERIES = new OracleRepository<DEVICE_USER_DATA_QUERIES>();
            DEVICES = new OracleRepository<DEVICES>();
            LOGS = new OracleRepository<LOGS>();
            PMH_SETTINGS = new OracleRepository<PMH_SETTINGS>();
            QUERY_SEQUENCE_REQUESTS = new OracleRepository<QUERY_SEQUENCE_REQUESTS>();

            Consumers = Singleton<InMemoryRepository<Consumer>>.Instance;
            ClientInfo = Singleton<InMemoryRepository<ClientInformation>>.Instance;

            //This Strategies aren't designed to be changed.
            connectionConnection = new ConnectionConnect();
            connectionRelease = new ConnectionRelease();
            connectionStatus = new ConnectionStatus();
            qryExecute = new QryExecute();
            qryDatabase = new QryDatabase();
            deviceRequestId = new DeviceRequestId(() => Convert.ToInt32(OracleRepository.GetIDbConnection().Query("select DEVICE_ID.nextval from dual")[0, 0]));

            //This is to keep the behavior repositories in sync with the service repositories.
            RepositoryChangedEvent += (s,e) => init();
            init();
        }


        public void init()
        {
            connectionConnection.ConsumerRepository = Consumers;

            connectionConnection.appsRepository = APPS;
            connectionConnection.clientRepository = CLIENTS;
            connectionConnection.clientAppsRepository = CLIENT_APPS;
            connectionConnection.clientUsersRepository = CLIENT_USERS;
            connectionConnection.clientDbCompaniesRepository = CLIENT_DB_COMPANIES;
            connectionConnection.clientDbCompanyUsersRepository = CLIENT_DB_COMPANY_USERS;
            connectionConnection.clientDbCompanyUserAppsRepository = CLIENT_DB_COMPANY_USER_APPS;

            connectionStatus.ConsumerRepository = Consumers;

            connectionRelease.ConsumerRepository = Consumers;

            qryExecute.AppQueryRepository = APP_QUERIES;
            qryExecute.ClientDbCompaniesRepository = CLIENT_DB_COMPANIES;
            qryExecute.ClientInfoRepository = ClientInfo;
            qryExecute.ConsumerRepository = Consumers;

            qryDatabase.ConsumerRepository = Consumers;
            qryDatabase.ClientDbCompaniesRepository = CLIENT_DB_COMPANIES;
            qryDatabase.ClientInfoRepository = ClientInfo;

            deviceRequestId.DevDataRepository = DEV_DATA;
            deviceRequestId.ConsumerRepository = Consumers;

        }
        
        public virtual MethodResult ConnectionConnect(int clientId, int appId, string database, string user, string password)
        {
            return connectionConnection.Strategy(clientId, appId, database, user, password);
        }

        public virtual MethodResult ConnectionRelease(int connectionId)
        {
            return connectionRelease.Strategy(connectionId);
        }

        public virtual MethodResult ConnectionStatus(int connectionId)
        {
            return connectionStatus.Strategy(connectionId);
        }

        public virtual RegisteredQueryResult QryExecute(int connectionId, string queryName, DateTime? time = null)
        {
            return qryExecute.Strategy(connectionId, queryName, time);
        }

        public virtual QryResult QueryDatabase(int connectionId, string Query, params object[] arguments)
        {
            return qryDatabase.Strategy(connectionId, Query, arguments);
        }

        public virtual MethodResult DeviceRequestId(int connectionId)
        {
            return deviceRequestId.Strategy(connectionId);
        }

        public virtual DeviceSynchronizeMethodResult DeviceSynchronize(int connectionId, int versionId)
        {
            throw new NotImplementedException();
        }

        public virtual MethodResult IUD(int connection, object IUDData)
        {
            throw new NotImplementedException();
        }

    }
}
