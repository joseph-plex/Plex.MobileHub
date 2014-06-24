using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Plex.MobileHub.ServiceLibraries.ClientServiceLibrary
{
    [ServiceContract(SessionMode = SessionMode.Required)]
    public interface IClientService
    {
        [OperationContract]
        void DoWork();
    }
}
