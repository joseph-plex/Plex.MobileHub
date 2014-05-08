using System;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Collections.Generic;

using MobileHubClient.Data;
using MobileHubClient.Core;

namespace MobileHubClient.Channels.Database
{
    [ServiceContract(Namespace = "PMHC")]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    class DatabaseService : PlexServiceBase
    {
        [OperationContract]
        public void RegisterDbConnectionData(DbConnectionData dbc)
        {
            ClientSettings.Instance.DbConnections.Add(dbc);
        }

        [OperationContract]
        public ClientDbConnectionFactory Discover()
        {
            ClientDbConnectionFactory DbFactory = new ClientDbConnectionFactory();
            DbFactory.Discover();
            return DbFactory;
        }

        [OperationContract]
        public Result QuerySource(String companyCode, String commandText, params object [] arguments)
        {
            using (var Connection = ClientSettings.Instance.GetOpenConnectionByCode(companyCode))
                return Connection.Query(commandText, arguments);
            
        }

        [OperationContract]
        public List<DbConnectionData> GetDbConnectionData()
        {
            return ClientSettings.Instance.DbConnections;
        }
    }
}
