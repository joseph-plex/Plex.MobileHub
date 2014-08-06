using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
namespace Plex.MobileHub.ServiceLibrary.ManagementService
{
    [ServiceContract]
    public interface IManagementService : IDisposable
    {
        [OperationContract]
        void DoWork();
    }
}
