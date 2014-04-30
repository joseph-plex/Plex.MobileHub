using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using MobileHubClient.Logs;

namespace MobileHubClient.Channels.Logs
{
    [ServiceContract(Namespace = "PMHC")]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class LogsService : PlexServiceBase
    {
        [OperationContract]
        public List<Log> GetLogs()
        {
            return LogManager.Instance.Logs;
        }

        [OperationContract]
        public void Add(Log log)
        {
            LogManager.Instance.Add(log);
        }
    }
}
