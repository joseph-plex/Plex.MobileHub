using System;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Collections.Generic;
using System.Linq;
using MobileHubClient.Data;
using MobileHubClient.Core;
using Plex.Logs;


namespace MobileHubClient.Channels.Database
{
    [ServiceContract(Namespace = "PMHC")]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    class DatabaseService : PlexServiceBase
    {
        [OperationContract]
        public void RegisterDbConnectionData(KeyValuePair<String,String> dbc)
        {
            ClientService.Logs.Add("Adding DBConnectionData" + dbc.Key);
            var settings = ClientSettings.Instance;
            if(!settings.DbConnections.Exists(p => p.Key == dbc.Key && p.Value == dbc.Value))
                settings.DbConnections.Add(dbc);
        }

        [OperationContract]
        public ClientDbConnectionFactory Discover()
        {
            try
            {
                return Context.DbFactory.Discover();
            }
            catch(Exception e)
            {
                ClientService.Logs.Add(e);
                return null;
            }
        }

        [OperationContract]
        public ClientDbConnectionFactory CurrentDatabaseInformation()
        {
            try
            {
                return Context.DbFactory;
            }
            catch (Exception e)
            {
                ClientService.Logs.Add(e);
                return null;
            }
        }

        [OperationContract]
        public Result QuerySource(String companyCode, String commandText, params object [] arguments)
        {
            using (var Connection = ClientSettings.Instance.GetOpenConnectionByCode(companyCode))
                return Connection.Query(commandText, arguments);
        }
    }
}
