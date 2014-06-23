using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace Plex.MobileHub.ServiceLibraries.APIServiceLibrary
{
    [ServiceContract]
    public interface IApiService
    {
        void ConnectionConnect();
        void ConnectionRelease();
        void ConnectionStatus();
        void DeviceRequestId();
        void DeviceSynchronization();
        void IUD();
        void QryExecute();
        void QueryDatabase();
    }
}
