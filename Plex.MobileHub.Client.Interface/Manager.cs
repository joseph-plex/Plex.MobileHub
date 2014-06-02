using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Plex.MobileHub.Client.Interface.DatabaseService;
using Plex.MobileHub.Client.Interface.GeneralService;
using Plex.MobileHub.Client.Interface.Logs;

namespace Plex.MobileHub.Client.Interface
{
    public class Manager
    {
        public static Manager Instance { get { return instance; } }
        static Manager instance = new Manager();

        public String ClientKey
        {
            get
            {
                using (var Service = GetGeneralService())
                    return Service.GetClientKey();
            }
            set
            {
                using (var Service = GetGeneralService())
                    Service.SetClientKey(value);
            }
        }
        public Int32 ClientId
        {
            get
            {
                using (var Service = GetGeneralService())
                    return Service.GetClientId();
            }
            set
            {
                using (var Service = GetGeneralService())
                    Service.SetClientId(value);
            }
        }
        public String IPAddress
        {
            get
            {
                using (var Service = GetGeneralService())
                    return Service.GetAddress();
            }
            set
            {
                using (var Service = GetGeneralService())
                    Service.SetAddress(value);
            }
        }
        public Int32 Port
        {
            get
            {
                using (var Service = GetGeneralService())
                    return Service.GetPort();
            }
            set
            {
                using (var Service = GetGeneralService())
                    Service.SetPort(value);
            }
        }
        public Boolean AutoLogin
        {
            get{
                using (var Service = GetGeneralService())
                    return Service.GetAutoLogOn();
            }
            set{
                using (var Service = GetGeneralService())
                    Service.SetAutoLogOn(value);
            }
        }

        public ClientDbConnectionFactory DatabaseInformationSearch()
        {
            using (var Service = GetDatabaseService())
                return Service.DatabaseInformationSearch();
        }

        public ClientDbConnectionFactory CurrentDatabaseInformation()
        {
            using (var Service = GetDatabaseService())
                return Service.CurrentDatabaseInformation();
        }

        public void RegisterDbConnectionData (string companyCode, string connectionString)
        {
            using (var Service = GetDatabaseService())
                Service.RegisterDbConnectionData(new KeyValuePair<String,String>(companyCode, connectionString));
        }

        public Result QuerySource(string companyCode, string commandText, params object[] arguments)
        {
            using (var Service = GetDatabaseService())
                return Service.QuerySource(companyCode, commandText, arguments.ToList());
        }
        public ClientDbConnectionFactory DataInformationRetrieve()
        {
            using (var service = GetDatabaseService())
                return service.DatabaseInformationRetrieve();
        }

        public void LogOn()
        {
            using(var service = GetGeneralService())
                service.LogOn();
        }

        public void LogOff()
        {
            using (var service = GetGeneralService())
                service.LogOff();
        }
        Manager() { }

        GeneralServiceClient GetGeneralService()
        {
            return new GeneralServiceClient("WSHttpBinding_GeneralService");
        }

        DatabaseServiceClient GetDatabaseService()
        {
            return new DatabaseServiceClient("WSHttpBinding_DatabaseService");
        }

        LogsServiceClient GetLogService()
        {
            return new LogsServiceClient("WSHttpBinding_LogsService");
        }
    }
}
