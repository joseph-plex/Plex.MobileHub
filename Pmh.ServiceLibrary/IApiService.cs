﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace Pmh.ServiceLibrary
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
        RegisteredQueryResult QryExecute(int connectionId, string QueryName, DateTime? Time = null);

        [OperationContract]
        QryResult QueryDatabase(int connectionId, string Query, params object[] arguments);
        
        [OperationContract]
        MethodResult DeviceRequestId(int connectionId);
        
        [OperationContract]
        DeviceSynchronizeMethodResult DeviceSynchronize(int connectionId, int versionId);
        
        [OperationContract]
        MethodResult IUD(int connection, object IUDData);

    }
}
