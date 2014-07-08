using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Linq;
using Plex.Logs;
using MobileHubClient.Core;

namespace MobileHubClient.Channels.Logs
{
    [ServiceContract(Namespace = "PMHC")]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class LogsService : PlexServiceBase
    {
        [OperationContract]
        public List<Log> GetLogs()
        {
            return ClientService.Logs.GetLogs().ToList();
        }

        [OperationContract]
        public void Add(Log log)
        {
            ClientService.Logs.Add(log);
        }
    }
}
