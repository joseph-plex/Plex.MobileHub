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
        void ConnectionConnect(int clientId, int appId, string database, string user, string password);

        [OperationContract]
        void ConnectionRelease(int connectionId);
        
        [OperationContract]
        void ConnectionStatus(int connectionId);

        [OperationContract]
        void QryExecute(int connectionId, string QueryName);

        [OperationContract]
        void QueryDatabase(int connectionId, string Query);
        
        [OperationContract]
        void DeviceRequestId(int connectionId);
        
        [OperationContract]
        void DeviceSynchronize(int connectionId, int versionId);
        
        [OperationContract]
        void IUD(int connection, object IUDData);
        

    }
}
