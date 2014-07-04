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
        public IRepository<APP_QUERIES> APP_QUERIES { get; set; }
        public IRepository<APP_QUERY_COLUMNS> APP_QUERY_COLUMNS { get; set; }
        public IRepository<APP_QUERY_CONDITIONS> APP_QUERY_CONDITIONS { get; set; }
        public IRepository<APP_TABLE_COLUMNS> APP_TABLE_COLUMNS { get; set; }
        public IRepository<APP_TABLES> APP_TABLES { get; set; }
        public IRepository<APP_USER_TYPES> APP_USER_TYPES { get; set; }
        public IRepository<APPS> APPS { get; set; }
        public IRepository<CLIENT_APPS> CLIENT_APPS { get; set; }
        public IRepository<CLIENT_DB_COMPANIES> CLIENT_DB_COMPANIES { get; set; }
        public IRepository<CLIENT_DB_COMPANY_USER_APPS> CLIENT_DB_COMPANY_USER_APPS { get; set; }
        public IRepository<CLIENT_DB_COMPANY_USERS> CLIENT_DB_COMPANY_USERS { get; set; }
        public IRepository<CLIENT_USERS> CLIENT_USERS { get; set; }
        public IRepository<CLIENTS> CLIENTS { get; set; }
        public IRepository<DEV_DATA> DEV_DATA { get; set; }
        public IRepository<DEV_DATA_VER> DEV_DATA_VER { get; set; }
        public IRepository<DEV_DATA_VER_QUERIES> DEV_DATA_VER_QUERIES { get; set; }
        public IRepository<DEVICE_USER_DATA> DEVICE_USER_DATA { get; set; }
        public IRepository<DEVICE_USER_DATA_QUERIES> DEVICE_USER_DATA_QUERIES { get; set; }
        public IRepository<DEVICES> DEVICES { get; set; }
        public IRepository<LOGS> LOGS { get; set; }
        public IRepository<PMH_SETTINGS> PMH_SETTINGS { get; set; }
        public IRepository<QUERY_SEQUENCE_REQUESTS> QUERY_SEQUENCE_REQUESTS { get; set; }

        public IRepository<Consumer> Consumers { get; set; }
        public IRepository<ClientInformation> ClientInfo { get; set; }

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
            connectionConnection.ConsumerRepository = Consumers;

            connectionConnection.clientRepository = CLIENTS;
            connectionConnection.clientAppsRepository = CLIENT_APPS;
            connectionConnection.clientUsersRepository = CLIENT_USERS;
            connectionConnection.clientDbCompaniesRepository = CLIENT_DB_COMPANIES;
            connectionConnection.clientDbCompanyUsersRepository = CLIENT_DB_COMPANY_USERS;
            connectionConnection.clientDbCompanyUserAppsRepository = CLIENT_DB_COMPANY_USER_APPS;

            connectionRelease = new ConnectionRelease();
            connectionRelease.ConsumerRepository = Consumers;

            connectionStatus = new ConnectionStatus();
            connectionStatus.ConsumerRepository = Consumers;

            qryExecute = new QryExecute();
            qryExecute.AppQueryRepository = APP_QUERIES;
            qryExecute.ClientDbCompaniesRepository = CLIENT_DB_COMPANIES;
            qryExecute.ClientInfoRepository = ClientInfo;
            qryExecute.ConsumerRepository = Consumers;

            qryDatabase = new QryDatabase();
            qryDatabase.ConsumerRepository = Consumers;
            qryDatabase.ClientDbCompaniesRepository = CLIENT_DB_COMPANIES;
            qryDatabase.ClientInfoRepository = ClientInfo;

            deviceRequestId = new DeviceRequestId(() => Convert.ToInt32(OracleRepository.GetIDbConnection().Query("select DEVICE_ID.nextval from dual")[0, 0]));
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
