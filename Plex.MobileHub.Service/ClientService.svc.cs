using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Plex.MobileHub.ServiceLibrary.ClientService;
using Plex.MobileHub.ServiceLibrary;
namespace Plex.MobileHub.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ClientService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ClientService.svc or ClientService.svc.cs at the Solution Explorer and start debugging.
    public class ClientService : IClientService, IDisposable
    {
        Dictionary<Type, Object> repositories;

        public ClientService()
        {
            repositories = new RepositoryFactory().GetRepositories();
        }

        public virtual string LogIn(int ClientId, string ClientKey, string ipAddress, int Port)
        {
            return new LogIn { Repositories = repositories }.Strategy(ClientId, ClientKey, ipAddress, Port);
        }

        public virtual void LogOut(string token)
        {
            new LogOut { Repositories = repositories }.Strategy(token);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
