using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using Plex.MobileHub.Data;
namespace Plex.MobileHub.ServiceLibraries.APIServiceLibrary
{
    [ServiceContract]
    public interface IApiService
    {
        [OperationContract]
        MethodResult ConnectionConnect(int clientId, int appId, string database, string user, string password);

        [OperationContract]
        MethodResult ConnectionRelease(int connectionId);
        
        [OperationContract]
        MethodResult ConnectionStatus(int connectionId);

        [OperationContract]
        void QryExecute(int connectionId, string QueryName);

        [OperationContract]
        PlexQueryResult QueryDatabase(int connectionId, string Query);
        
        [OperationContract]
        MethodResult DeviceRequestId(int connectionId);
        
        [OperationContract]
        void DeviceSynchronize(int connectionId, int versionId);
        
        [OperationContract]
        MethodResult IUD(int connection, object IUDData);
        

    }
}
