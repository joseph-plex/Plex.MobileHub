using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Plex.MobileHub.ServiceLibrary.ApiService;

namespace Plex.MobileHub.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ApiService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ApiService.svc or ApiService.svc.cs at the Solution Explorer and start debugging.
    public class ApiService : IApiService
    {

        public ServiceLibrary.MethodResult ConnectionConnect(int clientId, int appId, string database, string user, string password)
        {
            throw new NotImplementedException();
        }

        public ServiceLibrary.MethodResult ConnectionRelease(int connectionId)
        {
            throw new NotImplementedException();
        }

        public ServiceLibrary.MethodResult ConnectionStatus(int connectionId)
        {
            throw new NotImplementedException();
        }

        public ServiceLibrary.RegisteredQueryResult QryExecute(int connectionId, string QueryName, DateTime? Time = null)
        {
            throw new NotImplementedException();
        }

        public ServiceLibrary.QryResult QueryDatabase(int connectionId, string Query, params object[] arguments)
        {
            throw new NotImplementedException();
        }

        public ServiceLibrary.MethodResult DeviceRequestId(int connectionId)
        {
            throw new NotImplementedException();
        }

        public ServiceLibrary.DeviceSynchronizeMethodResult DeviceSynchronize(int connectionId, int versionId)
        {
            throw new NotImplementedException();
        }

        public ServiceLibrary.MethodResult IUD(int connection, object IUDData)
        {
            throw new NotImplementedException();
        }
    }
}
