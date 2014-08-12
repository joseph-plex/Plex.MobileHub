using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plex.MobileHub.Service;
using System.ServiceModel;
using Plex.MobileHub.ServiceLibrary.AccessService;
namespace Plex.MobileHub.Testing.Resources
{
    [ServiceBehavior(InstanceContextMode=InstanceContextMode.Single,IncludeExceptionDetailInFaults=true)]
    public sealed class AccessTestService : AccessService, IAccessService
    {
        public AccessTestService (): base()
        {
            Repositories = new RepoTestFactory().GetRepositories();
        }
    }
}
