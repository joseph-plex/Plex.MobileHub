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

        public ClientDbConnectionFactory DatabaseInformationRetrieve()
        {
            using (var Service = GetDatabaseService())
                return Service.DatabaseInformationRetrieve();
        }

        public List<KeyValuePair<String, String>> StoredDatabaseInformationRetrieve()
        {
            using (var Service = GetDatabaseService())
                return Service.StoredDatabaseInformationRetrieve();
        }

        public void RegisterDbConnectionData (string companyCode, string connectionString)
        {
            using (var Service = GetDatabaseService())
                Service.RegisterDbConnectionData(new KeyValuePair<String,String>(companyCode, connectionString));
        }

        public DatabaseService.Result QuerySource(string companyCode, string commandText, params object[] arguments)
        {
            using (var Service = GetDatabaseService())
                return Service.QuerySource(companyCode, commandText, arguments.ToList());
        }
        public ClientDbConnectionFactory DataInformationRetrieve()
        {
            using (var service = GetDatabaseService())
                return service.DatabaseInformationRetrieve();
        }

        public GeneralService.Result Query(string query, params object [] arguments )
        {
            using (var service = GetGeneralService())
                return service.Query(query, arguments.ToList());
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

        public bool IsConnected()
        {
            using (var service = GetGeneralService())
                return service.IsLoggedIn();
        }

        public bool ValidateClientSettings()
        {
            using (var service = GetGeneralService())
                return service.ValidateClientCredentials();
        }
        Manager() { }

        GeneralServiceClient GetGeneralService()
        {
            return new GeneralServiceClient("NetNamedPipeBinding_GeneralService");
        }

        DatabaseServiceClient GetDatabaseService()
        {
            return new DatabaseServiceClient("NetNamedPipeBinding_DatabaseService");
        }

        LogsServiceClient GetLogService()
        {
            return new LogsServiceClient("WSHttpBindinNetNamedPipeBinding_LogsServiceg_LogsService");
        }
    }
}
