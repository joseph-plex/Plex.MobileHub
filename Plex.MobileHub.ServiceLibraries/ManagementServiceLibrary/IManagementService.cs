using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using Plex.MobileHub.Data.Types;
namespace Plex.MobileHub.ServiceLibraries.ManagementServiceLibrary
{
    [ServiceContract]
    public interface IManagementService : IDisposable
    {
        [OperationContract]
        void DoWork();

        [OperationContract]
        void GetApp(Predicate<APPS> predicate);
    }
}
