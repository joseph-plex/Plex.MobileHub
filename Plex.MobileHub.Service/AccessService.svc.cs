using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Plex.MobileHub.ServiceLibrary.AccessService;
using Plex.MobileHub.ServiceLibrary;
using Plex.Data;
using Plex.MobileHub.ServiceLibrary.Types;
namespace Plex.MobileHub.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class AccessService : HubServiceBase, IAccessService
    {
        public AccessService() 
            : base()
        {
            Repositories = new RepositoryFactory().GetRepositories();
        }
        public IList<String> GetPrimayKeys(string typeName)
        {
            return new GetPrimaryKeys { Repositories = Repositories }.Strategy(typeName);
        }

        public void Insert(string typeName, __TypeBase entry)
        {
            new Insert { Repositories = Repositories }.Strategy(typeName, entry);
        }

        public void Update(string typeName, __TypeBase entry)
        {
            new Update { Repositories = Repositories }.Strategy(typeName, entry);
        }

        public void Delete(string typeName, __TypeBase[] entry)
        {
            new Delete { Repositories = Repositories }.Strategy(typeName, entry);
        }

        public __TypeBase[] RetrieveAll(string TypeName)
        {
            return new RetrieveAll { Repositories = Repositories }.Strategy(TypeName);
        }
    }
}
