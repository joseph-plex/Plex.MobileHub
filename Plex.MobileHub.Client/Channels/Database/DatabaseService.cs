using System;
using System.ServiceModel;
using System.ServiceModel.Web;


using MobileHubClient.Data;

namespace MobileHubClient.Channels.Database
{
    [ServiceContract(Namespace = "PMHC")]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    class DatabaseService : PlexServiceBase
    {
        [OperationContract]
        public bool Reset()
        { 
            ClientDbConnectionFactory.Instance.Discover();
            return true;
        }

        [OperationContract]
        public String GetDataMap()
        {
            return ClientDbConnectionFactory.Instance.ToString();
        }
    }
}
