﻿using System;
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
using System.Collections.Generic;
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
        public bool ValidateClientCredentials()
        {
            var clientinfo = ClientSettings.Instance;
            var result = Query("select count(*) from clients c where c.client_id = :a and c.client_key = :b", clientinfo.ClientId, clientinfo.ClientKey);
            return Convert.ToInt32(result[0, 0]) > 0;
        } 

        [OperationContract]
        public MobileHubClient.Data.Result Query(string commandText, params object[] arguments)
        {
            return WebService.Query(commandText, arguments);
        }

        [OperationContract]
        public CLIENTS CilentsRetrieve(int id)
        {
            return WebService.CilentsRetrieve(id);
        }

        [OperationContract]
        public CLIENT_APPS ClientsAppsRetrieve(int id)
        {
            return WebService.ClientsAppsRetrieve(id);
        }

        [OperationContract]
        public int ClientDbCompanyAdd(int clientId, string companyCode, string connectionString)
        {
            return WebService.ClientDbCompanyAdd(clientId, companyCode, connectionString);
        }

        [OperationContract]
        public void ClientDbCompanyRemove(int id)
        {
            WebService.ClientDbCompanyRemove(id);
        }

        [OperationContract]
        public int ClientDbCompanyUserAdd(int dbCompanyId, int userId, string connectAs)
        {
            return WebService.ClientDbCompanyUserAdd(dbCompanyId, userId, connectAs);
        }

        [OperationContract]
        public void ClientDbCompanyUserRemove(int id)
        {
            WebService.ClientDbCompanyUserRemove(id);
        }

        [OperationContract]
        public int ClientDbCompanyUserAppsAdd(int appId, int dbCompanyUserId, int? appUserTypeId = null) 
        {
            return WebService.ClientDbCompanyUserAppsAdd(appId, dbCompanyUserId, appUserTypeId);
        }

        [OperationContract]
        public void ClientUserAdd(int clientId, string name, string password)
        {
            WebService.ClientUserAdd(clientId, name, password);
        }

        [OperationContract]
        public void ClientUserRemove(int clientUserId)
        {
            WebService.ClientUserRemove(clientUserId);
        }

        [OperationContract]
        public List<CLIENT_USERS> ClientUserRetrieveAllForClients(int clientId)
        {
            return WebService.ClientUserRetrieveAllForClients(clientId);
        }

        [OperationContract]
        public CLIENT_USERS ClientUserRetrieve(int clientUserId)
        {
            return WebService.ClientUserRetrieve(clientUserId);
        }
    }
}
