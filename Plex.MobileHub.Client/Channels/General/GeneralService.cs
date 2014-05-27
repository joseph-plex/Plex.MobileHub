using System;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Net;
using Plex.Logs;
using MobileHubClient.Data;
using System.Text.RegularExpressions;
using MobileHubClient.Properties;
using MobileHubClient.Misc;
using MobileHubClient.Core;
using MobileHubClient.PMH;
namespace MobileHubClient.Channels.General
{
    [ServiceContract(Namespace = "PMHC")]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class GeneralService : PlexServiceBase
    {
        [OperationContract]
        public void SetClientKey(string ClientKey)
        {
            ClientSettings.Instance.ClientKey = ClientKey;
        }

        [OperationContract]
        public void SetClientId(int ClientId)
        {
            ClientSettings.Instance.ClientId = ClientId;
        }

        [OperationContract]
        public void SetAddress(string Address)
        {
            Regex regex = new Regex(@ServiceSettings.Default.AddressRegex);
            if (!regex.IsMatch(Address)) throw new ArgumentException("Invalid IPv4 Address");
            ClientSettings.Instance.Address = Address;
        }

        [OperationContract]
        public void SetPort(int Port)
        {
            ClientSettings.Instance.Port = Port;
        }

        [OperationContract]
        public void SetAutoLogOn(bool AutoLogOn)
        {
            ClientSettings.Instance.AutoLogOn = AutoLogOn;
        }
        [OperationContract]
        public string GetClientKey()
        {
            return ClientSettings.Instance.ClientKey;
        }
        [OperationContract]
        public int GetClientId()
        {
            return ClientSettings.Instance.ClientId;
        }
        [OperationContract]
        public string GetAddress()
        {
            return ClientSettings.Instance.Address;
        }

        [OperationContract]
        public int GetPort()
        {
            return ClientSettings.Instance.Port;
        }

        [OperationContract]
        public bool GetAutoLogOn()
        {
            return Convert.ToBoolean(ClientSettings.Instance.AutoLogOn);
        }

        [OperationContract]
        public int GetOutMessageLimt()
        {
            return ServiceSettings.Default.OutMessageLimit;
        }

        [OperationContract]
        public void RestoreDefaultSettings()
        {
            ClientSettings.Reset();
        }

        [OperationContract]
        public bool IsLoggedIn()
        {
            switch(Context.CurrentState)
            {
                case ClientServiceState.Connected:
                    return true;
                default:
                    return false;
            }
        }

        [OperationContract]
        public void LogOn()
        {
            Context.LogOn();
        }

        [OperationContract]
        public void LogOff()
        {
            Context.LogOff();
        }

        [OperationContract]
        public void Query(string commandText, params object[] arguments)
        {

        }


        [OperationContract]
        public void GetApplications(int clientId)
        {
            //todo implement applications
        }

        [OperationContract]
        public void GetApplicationTables()
        {
            //todo implement application table
        }

        [OperationContract]
        public void GetApplicationsTableColumns()
        {
            //todo Implement Application Table Column
        }
    }
}
