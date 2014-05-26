using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Plex.MobileHub.Manager.ManagementWebservice;

namespace Plex.MobileHub.Manager.Data
{
    public class DataFactory
    {
        public Result Query(string commandText, params object[] arguments)
        {
            return Result.FromQueryResult(GetService().Query(commandText, arguments));
        }

        public Int32 NonQuery(string commandText, params object [] arguments)
        {
            return GetService().NonQuery(commandText, arguments);
        }

        public IEnumerable<Log> GetLogs()
        {
            return GetService().GetLogRepository();
        }
       
        ManagerSDKSoapClient GetService()
        {
            return new ManagerSDKSoapClient("ManagerSDKSoap12");
        }
        
    }
}
