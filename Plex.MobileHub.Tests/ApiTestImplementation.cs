using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Plex.MobileHub.ServiceLibraries.APIServiceLibrary;
using Plex.MobileHub.ServiceLibraries;
using Plex.MobileHub.Data;
using Plex.MobileHub.Data.Types;

namespace Plex.MobileHub.Tests
{
    public class ApiTestImplementation : IApiService
    {
        public MethodResult ConnectionConnect(int clientId, int appId, string database, string user, string password)
        {
            throw new NotImplementedException();
        }

        public MethodResult ConnectionRelease(int connectionId)
        {
            throw new NotImplementedException();
        }

        public MethodResult ConnectionStatus(int connectionId)
        {
            throw new NotImplementedException();
        }

        public RegisteredQueryResult QryExecute(int connectionId, string QueryName, DateTime? Time = null)
        {
            throw new NotImplementedException();
        }

        public QryResult QueryDatabase(int connectionId, string Query, params object[] arguments)
        {
            throw new NotImplementedException();
        }

        public MethodResult DeviceRequestId(int connectionId)
        {
            throw new NotImplementedException();
        }

        public DeviceSynchronizeMethodResult DeviceSynchronize(int connectionId, int versionId)
        {
            throw new NotImplementedException();
        }

        public MethodResult IUD(int connection, object IUDData)
        {
            throw new NotImplementedException();
        }

        public IRepository<APP_QUERIES> APP_QUERIES
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public IRepository<APP_QUERY_COLUMNS> APP_QUERY_COLUMNS
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public IRepository<APP_QUERY_CONDITIONS> APP_QUERY_CONDITIONS
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public IRepository<APP_TABLE_COLUMNS> APP_TABLE_COLUMNS
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public IRepository<APP_TABLES> APP_TABLES
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public IRepository<APP_USER_TYPES> APP_USER_TYPES
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public IRepository<APPS> APPS
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public IRepository<CLIENT_APPS> CLIENT_APPS
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public IRepository<CLIENT_DB_COMPANIES> CLIENT_DB_COMPANIES
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public IRepository<CLIENT_DB_COMPANY_USER_APPS> CLIENT_DB_COMPANY_USER_APPS
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public IRepository<CLIENT_DB_COMPANY_USERS> CLIENT_DB_COMPANY_USERS
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public IRepository<CLIENT_USERS> CLIENT_USERS
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public IRepository<CLIENTS> CLIENTS
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public IRepository<DEV_DATA> DEV_DATA
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public IRepository<DEV_DATA_VER> DEV_DATA_DEV
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public IRepository<DEV_DATA_VER_QUERIES> DEV_DATA_VER_QUERIES
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public IRepository<DEVICE_USER_DATA> DEVICE_USER_DATA
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public IRepository<DEVICE_USER_DATA_QUERIES> DEVICE_USER_DATA_QUERIES
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public IRepository<DEVICES> DEVICES
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public IRepository<LOGS> LOGS
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public IRepository<PMH_SETTINGS> PMH_SETTINGS
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public IRepository<QUERY_SEQUENCE_REQUESTS> QUERY_SEQUENCE_REQUESTS
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
