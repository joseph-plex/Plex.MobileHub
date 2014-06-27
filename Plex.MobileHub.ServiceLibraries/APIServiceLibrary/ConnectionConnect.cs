using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plex.MobileHub.Data;
using Plex.MobileHub.Data.Types;
namespace Plex.MobileHub.ServiceLibraries.APIServiceLibrary
{

    public class ConnectionConnect : MethodStrategyBase<MethodResult> 
    {
        public IRepository<Consumer> ConsumerRepository { get; set; }

        public IRepository<CLIENT_DB_COMPANY_USER_APPS> clientDbCompanyUserAppsRepository { get; set; }
        public IRepository<CLIENT_DB_COMPANY_USERS> clientDbCompanyUsersRepository { get; set; }
        public IRepository<CLIENT_DB_COMPANIES> clientDbCompaniesRepository { get; set; }
        public IRepository<CLIENT_USERS> clientUsersRepository { get; set; }
        public IRepository<CLIENT_APPS> clientAppsRepository { get; set; }
        public IRepository<CLIENTS> clientRepository { get; set; }
        public IRepository<APPS> appsRepository { get; set; }


        public MethodResult Strategy(int clientId, int appId, string database, string user, string password)
        {
            //Ensure data inputted is correct.
            var client = clientRepository.Retrieve(p => p.CLIENT_ID == clientId);
            var app = appsRepository.Retrieve(p => p.APP_ID == appId);
            var dbs = clientDbCompaniesRepository.RetrieveAll().Where(p => p.CLIENT_ID == clientId && p.COMPANY_CODE == database);
            var person = clientUsersRepository.Retrieve(p => p.CLIENT_ID == clientId && p.NAME == user && p.PASSWORD == password);


            if (AnyNull(client, app, dbs, person) || dbs.Count() == 0)
                throw new Exception("Invalid Authentication, one or more of the values is incorrect");

            if (clientAppsRepository.Retrieve(p => p.APP_ID == appId && p.CLIENT_ID == clientId) == null)
                throw new Exception("Client is not authorized to use application");

            //This can happen by mistake but is generally an indication of someone abusing the system
          
            //var perm = clientDbCompanyUsersRepository.RetrieveAll().Where(p => p.DB_COMPANY_ID == dbs.DB_COMPANY_ID && person.USER_ID == p.USER_ID);
            var perm = clientDbCompanyUsersRepository.RetrieveAll().Where(p=> dbs.Any(db=> db.DB_COMPANY_ID == p.DB_COMPANY_ID && person.USER_ID == p.USER_ID));
            if (perm.Count() == 0)
                throw new Exception("User Is not authorized to view this database");

            //This can technically return more than one; however, only one needs to return in order to prove this connection information is valid. 
            if (clientDbCompanyUserAppsRepository.Retrieve(p=> perm.Any(a => p.APP_ID == appId && a.DB_COMPANY_USER_ID == p.DB_COMPANY_USER_ID)) == null)
                throw new Exception("User Database pairing is not authorized to use the application");

            Consumer consumer = new Consumer()
            {
                ConsumerId = GetConsumerId(),
                AppId = appId,
                ClientId = clientId,
                DatabaseCode = database,
                UserId = person.USER_ID
            };

            ConsumerRepository.Insert(consumer);
            return new MethodResult().Success(consumer.ConsumerId);
        }

        Int32 GetConsumerId()
        {
            int num;
            using(PlexRandomizer pr = new PlexRandomizer())
            {
                do num = pr.GetInt32();
                while (ConsumerRepository.Exists(p => p.ConsumerId == num));
                return num;
            }
        }
    }
}
