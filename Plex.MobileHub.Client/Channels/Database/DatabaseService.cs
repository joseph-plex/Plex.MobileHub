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
        public void RegisterDbConnectionData(DbConnectionData dbc)
        {
            ClientService.Logs.Add("Adding DBConnectionData" + dbc.Company);
            var settings = ClientSettings.Instance;
            var existing = settings.DbConnections.FirstOrDefault(p => p.Company.Equals(dbc.Company,StringComparison.OrdinalIgnoreCase));

            if (existing == null)
                settings.DbConnections.Add(dbc);
            else
                foreach(var connectionString in  dbc.ConnectionStrings)
                    if (existing.ConnectionStrings.Exists(p=> p == connectionString))
                        existing.ConnectionStrings.Add(connectionString);
        }

        [OperationContract]
        public List<CompanyCodeConnectionPairing> Discover()
        {
            try
            {
                var collection = new List<CompanyCodeConnectionPairing>(new ClientDbConnectionFactory().Discover().GetCompanySourcePairing());
                return collection;
            }
            catch(Exception e)
            {
                ClientService.Logs.Add(e.ToString());
                return null;
            }
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
