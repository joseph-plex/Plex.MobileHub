﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Plex.MobileHub.Client.Interface.GeneralService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="PMHC", ConfigurationName="GeneralService.GeneralService")]
    public interface GeneralService {
        
        [System.ServiceModel.OperationContractAttribute(Action="PMHC/GeneralService/SetClientKey", ReplyAction="PMHC/GeneralService/SetClientKeyResponse")]
        void SetClientKey(string ClientKey);
        
        [System.ServiceModel.OperationContractAttribute(Action="PMHC/GeneralService/SetClientKey", ReplyAction="PMHC/GeneralService/SetClientKeyResponse")]
        System.Threading.Tasks.Task SetClientKeyAsync(string ClientKey);
        
        [System.ServiceModel.OperationContractAttribute(Action="PMHC/GeneralService/SetClientId", ReplyAction="PMHC/GeneralService/SetClientIdResponse")]
        void SetClientId(int ClientId);
        
        [System.ServiceModel.OperationContractAttribute(Action="PMHC/GeneralService/SetClientId", ReplyAction="PMHC/GeneralService/SetClientIdResponse")]
        System.Threading.Tasks.Task SetClientIdAsync(int ClientId);
        
        [System.ServiceModel.OperationContractAttribute(Action="PMHC/GeneralService/SetAddress", ReplyAction="PMHC/GeneralService/SetAddressResponse")]
        void SetAddress(string Address);
        
        [System.ServiceModel.OperationContractAttribute(Action="PMHC/GeneralService/SetAddress", ReplyAction="PMHC/GeneralService/SetAddressResponse")]
        System.Threading.Tasks.Task SetAddressAsync(string Address);
        
        [System.ServiceModel.OperationContractAttribute(Action="PMHC/GeneralService/SetPort", ReplyAction="PMHC/GeneralService/SetPortResponse")]
        void SetPort(int Port);
        
        [System.ServiceModel.OperationContractAttribute(Action="PMHC/GeneralService/SetPort", ReplyAction="PMHC/GeneralService/SetPortResponse")]
        System.Threading.Tasks.Task SetPortAsync(int Port);
        
        [System.ServiceModel.OperationContractAttribute(Action="PMHC/GeneralService/SetAutoLogOn", ReplyAction="PMHC/GeneralService/SetAutoLogOnResponse")]
        void SetAutoLogOn(bool AutoLogOn);
        
        [System.ServiceModel.OperationContractAttribute(Action="PMHC/GeneralService/SetAutoLogOn", ReplyAction="PMHC/GeneralService/SetAutoLogOnResponse")]
        System.Threading.Tasks.Task SetAutoLogOnAsync(bool AutoLogOn);
        
        [System.ServiceModel.OperationContractAttribute(Action="PMHC/GeneralService/GetClientKey", ReplyAction="PMHC/GeneralService/GetClientKeyResponse")]
        string GetClientKey();
        
        [System.ServiceModel.OperationContractAttribute(Action="PMHC/GeneralService/GetClientKey", ReplyAction="PMHC/GeneralService/GetClientKeyResponse")]
        System.Threading.Tasks.Task<string> GetClientKeyAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="PMHC/GeneralService/GetClientId", ReplyAction="PMHC/GeneralService/GetClientIdResponse")]
        int GetClientId();
        
        [System.ServiceModel.OperationContractAttribute(Action="PMHC/GeneralService/GetClientId", ReplyAction="PMHC/GeneralService/GetClientIdResponse")]
        System.Threading.Tasks.Task<int> GetClientIdAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="PMHC/GeneralService/GetAddress", ReplyAction="PMHC/GeneralService/GetAddressResponse")]
        string GetAddress();
        
        [System.ServiceModel.OperationContractAttribute(Action="PMHC/GeneralService/GetAddress", ReplyAction="PMHC/GeneralService/GetAddressResponse")]
        System.Threading.Tasks.Task<string> GetAddressAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="PMHC/GeneralService/GetPort", ReplyAction="PMHC/GeneralService/GetPortResponse")]
        int GetPort();
        
        [System.ServiceModel.OperationContractAttribute(Action="PMHC/GeneralService/GetPort", ReplyAction="PMHC/GeneralService/GetPortResponse")]
        System.Threading.Tasks.Task<int> GetPortAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="PMHC/GeneralService/GetAutoLogOn", ReplyAction="PMHC/GeneralService/GetAutoLogOnResponse")]
        bool GetAutoLogOn();
        
        [System.ServiceModel.OperationContractAttribute(Action="PMHC/GeneralService/GetAutoLogOn", ReplyAction="PMHC/GeneralService/GetAutoLogOnResponse")]
        System.Threading.Tasks.Task<bool> GetAutoLogOnAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="PMHC/GeneralService/GetOutMessageLimt", ReplyAction="PMHC/GeneralService/GetOutMessageLimtResponse")]
        int GetOutMessageLimt();
        
        [System.ServiceModel.OperationContractAttribute(Action="PMHC/GeneralService/GetOutMessageLimt", ReplyAction="PMHC/GeneralService/GetOutMessageLimtResponse")]
        System.Threading.Tasks.Task<int> GetOutMessageLimtAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="PMHC/GeneralService/RestoreDefaultSettings", ReplyAction="PMHC/GeneralService/RestoreDefaultSettingsResponse")]
        void RestoreDefaultSettings();
        
        [System.ServiceModel.OperationContractAttribute(Action="PMHC/GeneralService/RestoreDefaultSettings", ReplyAction="PMHC/GeneralService/RestoreDefaultSettingsResponse")]
        System.Threading.Tasks.Task RestoreDefaultSettingsAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="PMHC/GeneralService/IsLoggedIn", ReplyAction="PMHC/GeneralService/IsLoggedInResponse")]
        bool IsLoggedIn();
        
        [System.ServiceModel.OperationContractAttribute(Action="PMHC/GeneralService/IsLoggedIn", ReplyAction="PMHC/GeneralService/IsLoggedInResponse")]
        System.Threading.Tasks.Task<bool> IsLoggedInAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="PMHC/GeneralService/LogOn", ReplyAction="PMHC/GeneralService/LogOnResponse")]
        void LogOn();
        
        [System.ServiceModel.OperationContractAttribute(Action="PMHC/GeneralService/LogOn", ReplyAction="PMHC/GeneralService/LogOnResponse")]
        System.Threading.Tasks.Task LogOnAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="PMHC/GeneralService/LogOff", ReplyAction="PMHC/GeneralService/LogOffResponse")]
        void LogOff();
        
        [System.ServiceModel.OperationContractAttribute(Action="PMHC/GeneralService/LogOff", ReplyAction="PMHC/GeneralService/LogOffResponse")]
        System.Threading.Tasks.Task LogOffAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="PMHC/GeneralService/Query", ReplyAction="PMHC/GeneralService/QueryResponse")]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(System.Collections.Generic.List<object>))]
        void Query(string commandText, System.Collections.Generic.List<object> arguments);
        
        [System.ServiceModel.OperationContractAttribute(Action="PMHC/GeneralService/Query", ReplyAction="PMHC/GeneralService/QueryResponse")]
        System.Threading.Tasks.Task QueryAsync(string commandText, System.Collections.Generic.List<object> arguments);
        
        [System.ServiceModel.OperationContractAttribute(Action="PMHC/GeneralService/GetApplications", ReplyAction="PMHC/GeneralService/GetApplicationsResponse")]
        void GetApplications(int clientId);
        
        [System.ServiceModel.OperationContractAttribute(Action="PMHC/GeneralService/GetApplications", ReplyAction="PMHC/GeneralService/GetApplicationsResponse")]
        System.Threading.Tasks.Task GetApplicationsAsync(int clientId);
        
        [System.ServiceModel.OperationContractAttribute(Action="PMHC/GeneralService/GetApplicationTables", ReplyAction="PMHC/GeneralService/GetApplicationTablesResponse")]
        void GetApplicationTables();
        
        [System.ServiceModel.OperationContractAttribute(Action="PMHC/GeneralService/GetApplicationTables", ReplyAction="PMHC/GeneralService/GetApplicationTablesResponse")]
        System.Threading.Tasks.Task GetApplicationTablesAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="PMHC/GeneralService/GetApplicationsTableColumns", ReplyAction="PMHC/GeneralService/GetApplicationsTableColumnsResponse")]
        void GetApplicationsTableColumns();
        
        [System.ServiceModel.OperationContractAttribute(Action="PMHC/GeneralService/GetApplicationsTableColumns", ReplyAction="PMHC/GeneralService/GetApplicationsTableColumnsResponse")]
        System.Threading.Tasks.Task GetApplicationsTableColumnsAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface GeneralServiceChannel : Plex.MobileHub.Client.Interface.GeneralService.GeneralService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class GeneralServiceClient : System.ServiceModel.ClientBase<Plex.MobileHub.Client.Interface.GeneralService.GeneralService>, Plex.MobileHub.Client.Interface.GeneralService.GeneralService {
        
        public GeneralServiceClient() {
        }
        
        public GeneralServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public GeneralServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public GeneralServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public GeneralServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public void SetClientKey(string ClientKey) {
            base.Channel.SetClientKey(ClientKey);
        }
        
        public System.Threading.Tasks.Task SetClientKeyAsync(string ClientKey) {
            return base.Channel.SetClientKeyAsync(ClientKey);
        }
        
        public void SetClientId(int ClientId) {
            base.Channel.SetClientId(ClientId);
        }
        
        public System.Threading.Tasks.Task SetClientIdAsync(int ClientId) {
            return base.Channel.SetClientIdAsync(ClientId);
        }
        
        public void SetAddress(string Address) {
            base.Channel.SetAddress(Address);
        }
        
        public System.Threading.Tasks.Task SetAddressAsync(string Address) {
            return base.Channel.SetAddressAsync(Address);
        }
        
        public void SetPort(int Port) {
            base.Channel.SetPort(Port);
        }
        
        public System.Threading.Tasks.Task SetPortAsync(int Port) {
            return base.Channel.SetPortAsync(Port);
        }
        
        public void SetAutoLogOn(bool AutoLogOn) {
            base.Channel.SetAutoLogOn(AutoLogOn);
        }
        
        public System.Threading.Tasks.Task SetAutoLogOnAsync(bool AutoLogOn) {
            return base.Channel.SetAutoLogOnAsync(AutoLogOn);
        }
        
        public string GetClientKey() {
            return base.Channel.GetClientKey();
        }
        
        public System.Threading.Tasks.Task<string> GetClientKeyAsync() {
            return base.Channel.GetClientKeyAsync();
        }
        
        public int GetClientId() {
            return base.Channel.GetClientId();
        }
        
        public System.Threading.Tasks.Task<int> GetClientIdAsync() {
            return base.Channel.GetClientIdAsync();
        }
        
        public string GetAddress() {
            return base.Channel.GetAddress();
        }
        
        public System.Threading.Tasks.Task<string> GetAddressAsync() {
            return base.Channel.GetAddressAsync();
        }
        
        public int GetPort() {
            return base.Channel.GetPort();
        }
        
        public System.Threading.Tasks.Task<int> GetPortAsync() {
            return base.Channel.GetPortAsync();
        }
        
        public bool GetAutoLogOn() {
            return base.Channel.GetAutoLogOn();
        }
        
        public System.Threading.Tasks.Task<bool> GetAutoLogOnAsync() {
            return base.Channel.GetAutoLogOnAsync();
        }
        
        public int GetOutMessageLimt() {
            return base.Channel.GetOutMessageLimt();
        }
        
        public System.Threading.Tasks.Task<int> GetOutMessageLimtAsync() {
            return base.Channel.GetOutMessageLimtAsync();
        }
        
        public void RestoreDefaultSettings() {
            base.Channel.RestoreDefaultSettings();
        }
        
        public System.Threading.Tasks.Task RestoreDefaultSettingsAsync() {
            return base.Channel.RestoreDefaultSettingsAsync();
        }
        
        public bool IsLoggedIn() {
            return base.Channel.IsLoggedIn();
        }
        
        public System.Threading.Tasks.Task<bool> IsLoggedInAsync() {
            return base.Channel.IsLoggedInAsync();
        }
        
        public void LogOn() {
            base.Channel.LogOn();
        }
        
        public System.Threading.Tasks.Task LogOnAsync() {
            return base.Channel.LogOnAsync();
        }
        
        public void LogOff() {
            base.Channel.LogOff();
        }
        
        public System.Threading.Tasks.Task LogOffAsync() {
            return base.Channel.LogOffAsync();
        }
        
        public void Query(string commandText, System.Collections.Generic.List<object> arguments) {
            base.Channel.Query(commandText, arguments);
        }
        
        public System.Threading.Tasks.Task QueryAsync(string commandText, System.Collections.Generic.List<object> arguments) {
            return base.Channel.QueryAsync(commandText, arguments);
        }
        
        public void GetApplications(int clientId) {
            base.Channel.GetApplications(clientId);
        }
        
        public System.Threading.Tasks.Task GetApplicationsAsync(int clientId) {
            return base.Channel.GetApplicationsAsync(clientId);
        }
        
        public void GetApplicationTables() {
            base.Channel.GetApplicationTables();
        }
        
        public System.Threading.Tasks.Task GetApplicationTablesAsync() {
            return base.Channel.GetApplicationTablesAsync();
        }
        
        public void GetApplicationsTableColumns() {
            base.Channel.GetApplicationsTableColumns();
        }
        
        public System.Threading.Tasks.Task GetApplicationsTableColumnsAsync() {
            return base.Channel.GetApplicationsTableColumnsAsync();
        }
    }
}
