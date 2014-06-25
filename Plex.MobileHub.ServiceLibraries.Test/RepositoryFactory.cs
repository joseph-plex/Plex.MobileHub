using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plex.MobileHub.Data.Types;

namespace Plex.MobileHub.ServiceLibraries.Test
{
    public class RepositoryFactory
    {
        public InMemoryRepository<APP_QUERIES> APP_QUERIES() 
        {
            InMemoryRepository<APP_QUERIES> repo = new InMemoryRepository<APP_QUERIES>();
            return repo;
        }
        public InMemoryRepository<APP_QUERY_COLUMNS> APP_QUERY_COLUMNS() {
            InMemoryRepository<APP_QUERY_COLUMNS> repo = new InMemoryRepository<APP_QUERY_COLUMNS>();
            return repo;
        }
        public InMemoryRepository<APP_QUERY_CONDITIONS> APP_QUERY_CONDITIONS() {
            InMemoryRepository<APP_QUERY_CONDITIONS> repo = new InMemoryRepository<APP_QUERY_CONDITIONS>();
            return repo;
        }
        public InMemoryRepository<APP_TABLE_COLUMNS> APP_TABLE_COLUMNS()
        {
            InMemoryRepository<APP_TABLE_COLUMNS> repo = new InMemoryRepository<APP_TABLE_COLUMNS>();
            return repo;
        }
        public InMemoryRepository<APP_TABLES> APP_TABLES()
        {
            InMemoryRepository<APP_TABLES> repo = new InMemoryRepository<APP_TABLES>();
            return repo;
        }
        public InMemoryRepository<APP_USER_TYPES> APP_USER_TYPES()
        {
            InMemoryRepository<APP_USER_TYPES> repo = new InMemoryRepository<APP_USER_TYPES>();
            return repo;
        }
        public InMemoryRepository<APPS> APP_QUERIES()
        {
            InMemoryRepository<APPS> repo = new InMemoryRepository<APPS>();
            APPS v = new APPS();
            v.APP_ID = 1;
            v.AUTH_KEY = "AuthKey";
            v.DESCRIPTION = "This is a test application";
            v.IS_CLIENT_CUSTOM_APP = 0;
            v.TITLE = "APP1";

            repo.Insert(v);
            return repo;
        }

        public InMemoryRepository<CLIENT_APPS> CLIENT_APPS()
        {
            InMemoryRepository<CLIENT_APPS> repo = new InMemoryRepository<CLIENT_APPS>();
            CLIENT_APPS v = new CLIENT_APPS();
            v.APP_ID = v.CLIENT_ID = v.CLIENT_APP_ID = 1;

            repo.Insert(v);
            return repo;
        }
        public InMemoryRepository<CLIENT_DB_COMPANIES> CLIENT_DB_COMPANIES()
        {
            InMemoryRepository<CLIENT_DB_COMPANIES> repo = new InMemoryRepository<CLIENT_DB_COMPANIES>();
            CLIENT_DB_COMPANIES v = new CLIENT_DB_COMPANIES();
            v.CLIENT_ID = 1;
            v.DB_COMPANY_ID = 1;
            v.DATABASE_CSTRING = "This is a connection String";
            v.COMPANY_CODE = "PDRYWALL";

            repo.Insert(v);
            return repo;
        }
        public InMemoryRepository<CLIENT_DB_COMPANY_USER_APPS> CLIENT_DB_COMPANY_USER_APPS()
        {
            InMemoryRepository<CLIENT_DB_COMPANY_USER_APPS> repo = new InMemoryRepository<CLIENT_DB_COMPANY_USER_APPS>();
            CLIENT_DB_COMPANY_USER_APPS v = new CLIENT_DB_COMPANY_USER_APPS();
            v.APP_ID = 1;
            v.APP_USER_TYPE_ID = null;
            v.DB_COMPANY_USER_ID = 1;
            v.DB_COMPANY_USER_APP_ID = 1;

            repo.Insert(v);
            return repo;
        }
        public InMemoryRepository<CLIENT_DB_COMPANY_USERS> CLIENT_DB_COMPANY_USERS()
        {
            InMemoryRepository<CLIENT_DB_COMPANY_USERS> repo = new InMemoryRepository<CLIENT_DB_COMPANY_USERS>();
            CLIENT_DB_COMPANY_USERS v = new CLIENT_DB_COMPANY_USERS();
            v.USER_ID = 1;
            v.DB_COMPANY_USER_ID = 1;
            v.DB_COMPANY_ID = 1;
            v.CONNECT_AS = null;

            repo.Insert(v);
            return repo;
        }
        public InMemoryRepository<CLIENT_USERS> CLIENT_USERS()
        {
            InMemoryRepository<CLIENT_USERS> repo = new InMemoryRepository<CLIENT_USERS>();
            CLIENT_USERS v = new CLIENT_USERS();
            v.CLIENT_ID = 1;
            v.NAME = "Joseph";
            v.PASSWORD = "Morain";
            v.USER_ID = 1;

            repo.Insert(v);
            return repo;
        }
        public InMemoryRepository<CLIENTS> CLIENTS()
        {
            InMemoryRepository<CLIENTS> repo = new InMemoryRepository<CLIENTS>();
            CLIENTS v = new CLIENTS();
            v.CLIENT_ID = 1;
            v.CLIENT_KEY = "Client1";
            v.DESCRIPTION = "This is a client";

            repo.Insert(v);
            return repo;
        }
        public InMemoryRepository<DEV_DATA> DEV_DATA()
        {
            InMemoryRepository<DEV_DATA> repo = new InMemoryRepository<DEV_DATA>();
            return repo;
        }
        public InMemoryRepository<DEV_DATA_VER> DEV_DATA_VER()
        {
            InMemoryRepository<DEV_DATA_VER> repo = new InMemoryRepository<DEV_DATA_VER>();
            return repo;
        }
        public InMemoryRepository<DEV_DATA_VER_QUERIES> DEV_DATA_VER_QUERIES()
        {
            InMemoryRepository<DEV_DATA_VER_QUERIES> repo = new InMemoryRepository<DEV_DATA_VER_QUERIES>();
            return repo;
        }
        public InMemoryRepository<DEVICE_USER_DATA> DEVICE_USER_DATA()
        {
            InMemoryRepository<DEVICE_USER_DATA> repo = new InMemoryRepository<DEVICE_USER_DATA>();
            return repo;
        }
        public InMemoryRepository<DEVICE_USER_DATA_QUERIES> DEVICE_USER_DATA_QUERIES()
        {
            InMemoryRepository<DEVICE_USER_DATA_QUERIES> repo = new InMemoryRepository<DEVICE_USER_DATA_QUERIES>();
            return repo;
        }
        public InMemoryRepository<DEVICES> DEVICES()
        {
            InMemoryRepository<DEVICES> repo = new InMemoryRepository<DEVICES>();
            return repo;
        }
        public InMemoryRepository<LOGS> LOGS()
        {
            InMemoryRepository<LOGS> repo = new InMemoryRepository<LOGS>();
            return repo;
        }
        public InMemoryRepository<PMH_SETTINGS> PMH_SETTINGS()
        {
            InMemoryRepository<PMH_SETTINGS> repo = new InMemoryRepository<PMH_SETTINGS>();
            return repo;
        }
        public InMemoryRepository<QUERY_SEQUENCE_REQUESTS> QUERY_SEQUENCE_REQUESTS()
        {
            InMemoryRepository<QUERY_SEQUENCE_REQUESTS> repo = new InMemoryRepository<QUERY_SEQUENCE_REQUESTS>();
            return repo;
        }
    }
}
