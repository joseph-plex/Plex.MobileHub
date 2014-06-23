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
        [OperationContract]
        void ConnectionConnect();
        [OperationContract]
        void ConnectionRelease();
        [OperationContract]
        void ConnectionStatus();
        [OperationContract]
        void DeviceRequestId();
        [OperationContract]
        void DeviceSynchronization();
        [OperationContract]
        void IUD();
        [OperationContract]
        void QryExecute();
        [OperationContract]
        void QueryDatabase();
    }
}
