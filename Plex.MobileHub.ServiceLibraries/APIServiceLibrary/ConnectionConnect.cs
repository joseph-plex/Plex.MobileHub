using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plex.MobileHub.Data;
using Plex.MobileHub.Data.Types;
namespace Plex.MobileHub.ServiceLibraries.APIServiceLibrary
{
    public class ConnectionConnect : MethodStrategyBase<bool> 
    {
        public IRepository<CLIENT_DB_COMPANY_USER_APPS> clientDbCompanyUserAppsRepository { get; set; }
        public IRepository<CLIENT_DB_COMPANY_USERS> clientDbCompanyUsersRepository { get; set; }
        public IRepository<CLIENT_DB_COMPANIES> clientDbCompaniesRepository { get; set; }
        public IRepository<CLIENT_USERS> clientUsersRepository { get; set; }
        public IRepository<CLIENT_APPS> clientAppsRepository { get; set; }
        public IRepository<CLIENTS> clientRepository { get; set; }
        public IRepository<APPS> appsRepository { get; set; }


        public bool Strategy(int clientId, int appId, string database, string user, string password)
        {
            //Ensure data inputted is correct.
            var client = clientRepository.Retrieve(p => p.CLIENT_ID == clientId);
            var app = appsRepository.Retrieve(p => p.APP_ID == appId);
            var db = clientDbCompaniesRepository.Retrieve(p => p.CLIENT_ID == clientId && p.COMPANY_CODE == database);
            var consumer = clientUsersRepository.Retrieve(p => p.CLIENT_ID == clientId && p.NAME == user && p.PASSWORD == password);

            if (AnyNull(client, app, db, consumer))
                throw new Exception("Invalid Authentication, one or more of the values is incorrect");

            if (clientAppsRepository.Retrieve(p => p.APP_ID == appId && p.CLIENT_ID == clientId) == null)
                throw new Exception("Client is not authorized to use application");

            //This can happen by mistake but is generally an indication of someone abusing the system
            var permission = clientDbCompanyUsersRepository.Retrieve(p => p.DB_COMPANY_ID == db.DB_COMPANY_ID && consumer.USER_ID == p.USER_ID);
            if (permission == null)
                throw new Exception("User Is not authorized to view this database");

            if (clientDbCompanyUserAppsRepository.Retrieve(p=> p.APP_ID == appId && permission.DB_COMPANY_USER_ID == p.DB_COMPANY_USER_ID) == null)
                throw new Exception("User Database pairing is not authorized to use the application");

            //Indicates success.
            return true;

        }
    }
}
