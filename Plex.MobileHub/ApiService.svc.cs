using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Plex.MobileHub.ServiceLibraries.APIServiceLibrary;
using Plex.MobileHub.ServiceLibraries;
using Plex.MobileHub.Data;
using Plex.MobileHub.Data.Types;

namespace Plex.MobileHub
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Api : IApiService
    {
        public MethodResult ConnectionConnect(int clientId, int appId, string database, string user, string password)
        {
            ConnectionConnect cc, connectionConnectionBehavior = cc = new ConnectionConnect();
            cc.ConsumerRepository = Singleton<InMemoryRepository<Consumer>>.Instance;

            cc.clientRepository = new OracleRepository<CLIENTS>();
            cc.clientAppsRepository = new OracleRepository<CLIENT_APPS>();
            cc.clientUsersRepository = new OracleRepository<CLIENT_USERS>();
            cc.clientDbCompaniesRepository = new OracleRepository<CLIENT_DB_COMPANIES>();
            cc.clientDbCompanyUsersRepository = new OracleRepository<CLIENT_DB_COMPANY_USERS>();
            cc.clientDbCompanyUserAppsRepository = new OracleRepository<CLIENT_DB_COMPANY_USER_APPS>();

            return cc.Strategy(clientId, appId, database, user, password);
        }
        public MethodResult ConnectionRelease(int connectionId)
        {
            throw new NotImplementedException();
        }
        public MethodResult ConnectionStatus(int connectionId)
        {
            throw new NotImplementedException();
        }
        public void QryExecute(int connectionId, string QueryName)
        {
            throw new NotImplementedException();
        }
        public PlexQueryResult QueryDatabase(int connectionId, string Query)
        {
            throw new NotImplementedException();
        }
        public MethodResult DeviceRequestId(int connectionId)
        {
            throw new NotImplementedException();
        }
        public void DeviceSynchronize(int connectionId, int versionId)
        {
            throw new NotImplementedException();
        }
        public MethodResult IUD(int connection, object IUDData)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
