using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Plex.MobileHub.ServiceLibraries.APIServiceLibrary;
using Plex.MobileHub.ServiceLibraries;
using Plex.MobileHub.Data;
using Plex.MobileHub.Data.Types;

namespace Plex.MobileHub.Tests
{
    public class ApiTestImplementation : IApiService
    {
        public MethodResult ConnectionConnect(int clientId, int appId, string database, string user, string password)
        {
            throw new NotImplementedException();
        }

        public MethodResult ConnectionRelease(int connectionId)
        {
            throw new NotImplementedException();
        }

        public MethodResult ConnectionStatus(int connectionId)
        {
            throw new NotImplementedException();
        }

        public RegisteredQueryResult QryExecute(int connectionId, string QueryName, DateTime? Time = null)
        {
            throw new NotImplementedException();
        }

        public QryResult QueryDatabase(int connectionId, string Query, params object[] arguments)
        {
            throw new NotImplementedException();
        }

        public MethodResult DeviceRequestId(int connectionId)
        {
            throw new NotImplementedException();
        }

        public DeviceSynchronizeMethodResult DeviceSynchronize(int connectionId, int versionId)
        {
            throw new NotImplementedException();
        }

        public MethodResult IUD(int connection, object IUDData)
        {
            throw new NotImplementedException();
        }
    }
}
