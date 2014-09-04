using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Pmh.ServiceLibrary.Behaviors.Client;
using Pmh.ServiceLibrary;
namespace Pmh.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Behaviors.Client" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Behaviors.Client.svc or Behaviors.Client.svc.cs at the Solution Explorer and start debugging.
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
