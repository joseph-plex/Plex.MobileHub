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
        IRepository<CLIENT_DB_COMPANY_USER_APPS> clientDbCompanyUserAppsRepository;
        IRepository<CLIENT_DB_COMPANY_USERS> clientDbCompanyUsersRepository;
        IRepository<CLIENT_DB_COMPANIES> clientDbCompaniesRepository;
        IRepository<CLIENT_USERS> clientUsersRepository;
        IRepository<CLIENT_APPS> clientAppsRepository;
        IRepository<CLIENTS> clientRepository;
        IRepository<APPS> appsRepository;

        public ConnectionConnect(
            IRepository<APPS> appsRepo,
            IRepository<CLIENTS> clientsRepo,
            IRepository<CLIENT_APPS> clientAppRepo,
            IRepository<CLIENT_USERS> clientUserRepo,
            IRepository<CLIENT_DB_COMPANIES> clientDbCompaniesRepo,
            IRepository<CLIENT_DB_COMPANY_USERS> clientsDbCompaniesRepo,
            IRepository<CLIENT_DB_COMPANY_USER_APPS> clientDbCompanyUserAppsRepo)
        {
            appsRepository = appsRepo;
            clientRepository = clientsRepo;
            clientAppsRepository = clientAppRepo;
            clientUsersRepository = clientUserRepo;
            clientDbCompaniesRepository = clientDbCompaniesRepo;
            clientDbCompanyUsersRepository = clientsDbCompaniesRepo;
            clientDbCompanyUserAppsRepository = clientDbCompanyUserAppsRepo;
        }

        public Object Strategy(int clientId, int appId, string database, string user, string password)
        {
            //todo Implement this
            return new Object();
        }

        //CLIENTS ClientGet(int ClientId)
        //{
            //var Clients = new List<CLIENTS>(CLIENTS.GetAll());
            //var ClientIndex = Clients.FindIndex((p) => p.CLIENT_ID == ClientId);
            //if (ClientIndex == -1) throw new InvalidClientException();
            //return Clients[ClientIndex];
        //}
    }
}
