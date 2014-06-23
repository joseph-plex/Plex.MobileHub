using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
namespace Plex.MobileHub.ServiceLibraries.ManagementServiceLibrary
{
    [ServiceContract]
    public interface IManagementService
    {
        [OperationContract]
        void DoWork();
    }
}
