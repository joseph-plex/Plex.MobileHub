using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Plex.MobileHub.ServiceLibraries.DeveloperServiceLibrary
{
    [ServiceContract] 
    public interface IDeveloperService : IDisposable
    {
        [OperationContract]
        void DoWork();
    }
}
