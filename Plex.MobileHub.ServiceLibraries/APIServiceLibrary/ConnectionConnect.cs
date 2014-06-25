using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plex.MobileHub.Data;
using Plex.MobileHub.Data.Types;
namespace Plex.MobileHub.ServiceLibraries.APIServiceLibrary
{
    class ConnectionConnect : MethodStrategyBase<Object> //todo change this to method result
    {
        public IRepository<CLIENT_DB_COMPANY_USER_APPS> clientDbCompanyUserAppsRepository { get; set; }
        public IRepository<CLIENT_DB_COMPANY_USERS> clientDbCompanyUsersRepository { get; set; }
        public IRepository<CLIENT_DB_COMPANIES> clientDbCompaniesRepository { get; set; }
        public IRepository<CLIENT_USERS> clientUsersRepository { get; set; }
        public IRepository<CLIENT_APPS> clientAppsRepository { get; set; }
        public IRepository<CLIENTS> clientRepository { get; set; }
        public IRepository<APPS> appsRepository { get; set; }


        public Object Strategy(int clientId, int appId, string database, string user, string password)
        {
            //todo Implement this
            return new Object();
        }

    }
}
