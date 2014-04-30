using System;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Net;
using MobileHubClient.Logs;
using System.Text.RegularExpressions;
using MobileHubClient.Properties;
using MobileHubClient.Misc;
namespace MobileHubClient.Channels.General
{
    [ServiceContract(Namespace = "PMHC")]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class GeneralService : PlexServiceBase
    {

        [OperationContract]
        public void SetClientKey(string ClientKey)
        {
            ClientSettings.Default.ClientKey = ClientKey;
        }

        [OperationContract]
        public void SetClientId(int ClientId)
        {
            ClientSettings.Default.ClientId = ClientId;
        }

        [OperationContract]
        public void SetAddress(string Address)
        {
            Regex regex = new Regex(@ClientSettings.Default.AddressRegex);
            if (!regex.IsMatch(Address)) 
                throw new ArgumentException("Invalid IPv4 Address");
            ClientSettings.Default.Address = Address;
        }

        [OperationContract]
        public void SetPort(int Port)
        {
            ClientSettings.Default.Port = Port;
        }

        [OperationContract]
        public void SetAutoLogOn(bool AutoLogOn)
        {
            ClientSettings.Default.AutoLogOn = AutoLogOn;
        }
        [OperationContract]
        public string GetClientKey()
        {
            return ClientSettings.Default.ClientKey;
        }
        [OperationContract]
        public int GetClientId()
        {
            return ClientSettings.Default.ClientId;
        }
        [OperationContract]
        public string GetAddress()
        {
            return ClientSettings.Default.Address;
        }

        [OperationContract]
        public int GetPort()
        {
            return ClientSettings.Default.Port;
        }

        [OperationContract]
        public bool GetAutoLogOn()
        {
            return Convert.ToBoolean(ClientSettings.Default.AutoLogOn);
        }

        [OperationContract]
        public int GetOutMessageLimt()
        {
            return ClientSettings.Default.OutMessageLimit;
        }

        [OperationContract]
        public void RestoreDefaultSettings()
        {
            ClientSettings.Default.Reset();
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

    }
}
