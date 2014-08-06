using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Threading.Tasks;
namespace Plex.MobileHub.ServiceLibrary.DevelService
{
    [ServiceContract]
    public interface IDevelService : IDisposable
    {
        [OperationContract]
        void DoWork();
    }
}
