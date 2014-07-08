using System;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Collections.Generic;
using System.Linq;
using MobileHubClient.Data;
using MobileHubClient.Core;
using MobileHubClient.Misc;
using Plex.Logs;


namespace MobileHubClient.Channels.Database
{
    [ServiceContract(Namespace = "PMHC")]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    class DatabaseService : PlexServiceBase
    {
        [OperationContract]
        public void RegisterDbConnectionData(KeyValuePair<String, String> dbc)
        {
            ClientService.Logs.Add("Adding Database Connection Data " + dbc.Key);
            var settings = ClientSettings.Instance;
            
            if (!Context.DbConnections.Any(p => p.COMPANY_CODE == dbc.Key && p.DATABASE_CSTRING == dbc.Value))
                WebService.ClientDbCompanyAdd(ClientSettings.Instance.ClientId, dbc.Key, dbc.Value);                
        }

        [OperationContract]
        public ClientDbConnectionFactory DatabaseInformationSearch()
        {
            try {
                return Context.DbFactory.Discover();
            }
            catch(Exception e) {
                ClientService.Logs.Add(e); 
                throw;
            } 
        }

        [OperationContract]
        public ClientDbConnectionFactory DatabaseInformationRetrieve()
        {
            return Context.DbFactory;
        }

        [OperationContract]
        public List<KeyValuePair<String, String>> StoredDatabaseInformationRetrieve()
        {
            List<KeyValuePair<String, String>> values = new List<KeyValuePair<String, String>>();
            foreach(var connection in Context.DbConnections)
                values.Add(new KeyValuePair<string,string>(connection.COMPANY_CODE, connection.DATABASE_CSTRING));

            return values;
        }

        [OperationContract]
        public Result QuerySource(String companyCode, String commandText, params object [] arguments)
        {
            using (var Connection = Context.GetOpenConnection(companyCode))
                return Connection.Query(commandText, arguments);
        }
    }
}
