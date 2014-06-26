using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace Plex.MobileHub.ServiceLibraries
{
    [ServiceContract]
    public interface IMessageCallback
    {
        [OperationContract]
        void SendMessage(String msg);
    }
}
