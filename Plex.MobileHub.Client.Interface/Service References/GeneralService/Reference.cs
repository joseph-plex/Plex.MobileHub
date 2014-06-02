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
        
        [System.ServiceModel.OperationContractAttribute(Action="PMHC/GeneralService/SetClientId", ReplyAction="PMHC/GeneralService/SetClientIdResponse")]
        void SetClientId(int ClientId);
        
        [System.ServiceModel.OperationContractAttribute(Action="PMHC/GeneralService/SetAddress", ReplyAction="PMHC/GeneralService/SetAddressResponse")]
        void SetAddress(string Address);
        
        [System.ServiceModel.OperationContractAttribute(Action="PMHC/GeneralService/SetPort", ReplyAction="PMHC/GeneralService/SetPortResponse")]
        void SetPort(int Port);
        
        [System.ServiceModel.OperationContractAttribute(Action="PMHC/GeneralService/SetAutoLogOn", ReplyAction="PMHC/GeneralService/SetAutoLogOnResponse")]
        void SetAutoLogOn(bool AutoLogOn);
        
        [System.ServiceModel.OperationContractAttribute(Action="PMHC/GeneralService/GetClientKey", ReplyAction="PMHC/GeneralService/GetClientKeyResponse")]
        string GetClientKey();
        
        [System.ServiceModel.OperationContractAttribute(Action="PMHC/GeneralService/GetClientId", ReplyAction="PMHC/GeneralService/GetClientIdResponse")]
        int GetClientId();
        
        [System.ServiceModel.OperationContractAttribute(Action="PMHC/GeneralService/GetAddress", ReplyAction="PMHC/GeneralService/GetAddressResponse")]
        string GetAddress();
        
        [System.ServiceModel.OperationContractAttribute(Action="PMHC/GeneralService/GetPort", ReplyAction="PMHC/GeneralService/GetPortResponse")]
        int GetPort();
        
        [System.ServiceModel.OperationContractAttribute(Action="PMHC/GeneralService/GetAutoLogOn", ReplyAction="PMHC/GeneralService/GetAutoLogOnResponse")]
        bool GetAutoLogOn();
        
        [System.ServiceModel.OperationContractAttribute(Action="PMHC/GeneralService/GetOutMessageLimt", ReplyAction="PMHC/GeneralService/GetOutMessageLimtResponse")]
        int GetOutMessageLimt();
        
        [System.ServiceModel.OperationContractAttribute(Action="PMHC/GeneralService/RestoreDefaultSettings", ReplyAction="PMHC/GeneralService/RestoreDefaultSettingsResponse")]
        void RestoreDefaultSettings();
        
        [System.ServiceModel.OperationContractAttribute(Action="PMHC/GeneralService/IsLoggedIn", ReplyAction="PMHC/GeneralService/IsLoggedInResponse")]
        bool IsLoggedIn();
        
        [System.ServiceModel.OperationContractAttribute(Action="PMHC/GeneralService/LogOn", ReplyAction="PMHC/GeneralService/LogOnResponse")]
        void LogOn();
        
        [System.ServiceModel.OperationContractAttribute(Action="PMHC/GeneralService/LogOff", ReplyAction="PMHC/GeneralService/LogOffResponse")]
        void LogOff();
        
        [System.ServiceModel.OperationContractAttribute(Action="PMHC/GeneralService/Query", ReplyAction="PMHC/GeneralService/QueryResponse")]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(System.Collections.Generic.List<object>))]
        void Query(string commandText, System.Collections.Generic.List<object> arguments);
        
        [System.ServiceModel.OperationContractAttribute(Action="PMHC/GeneralService/GetApplications", ReplyAction="PMHC/GeneralService/GetApplicationsResponse")]
        void GetApplications(int clientId);
        
        [System.ServiceModel.OperationContractAttribute(Action="PMHC/GeneralService/GetApplicationTables", ReplyAction="PMHC/GeneralService/GetApplicationTablesResponse")]
        void GetApplicationTables();
        
        [System.ServiceModel.OperationContractAttribute(Action="PMHC/GeneralService/GetApplicationsTableColumns", ReplyAction="PMHC/GeneralService/GetApplicationsTableColumnsResponse")]
        void GetApplicationsTableColumns();
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
        
        public void SetClientId(int ClientId) {
            base.Channel.SetClientId(ClientId);
        }
        
        public void SetAddress(string Address) {
            base.Channel.SetAddress(Address);
        }
        
        public void SetPort(int Port) {
            base.Channel.SetPort(Port);
        }
        
        public void SetAutoLogOn(bool AutoLogOn) {
            base.Channel.SetAutoLogOn(AutoLogOn);
        }
        
        public string GetClientKey() {
            return base.Channel.GetClientKey();
        }
        
        public int GetClientId() {
            return base.Channel.GetClientId();
        }
        
        public string GetAddress() {
            return base.Channel.GetAddress();
        }
        
        public int GetPort() {
            return base.Channel.GetPort();
        }
        
        public bool GetAutoLogOn() {
            return base.Channel.GetAutoLogOn();
        }
        
        public int GetOutMessageLimt() {
            return base.Channel.GetOutMessageLimt();
        }
        
        public void RestoreDefaultSettings() {
            base.Channel.RestoreDefaultSettings();
        }
        
        public bool IsLoggedIn() {
            return base.Channel.IsLoggedIn();
        }
        
        public void LogOn() {
            base.Channel.LogOn();
        }
        
        public void LogOff() {
            base.Channel.LogOff();
        }
        
        public void Query(string commandText, System.Collections.Generic.List<object> arguments) {
            base.Channel.Query(commandText, arguments);
        }
        
        public void GetApplications(int clientId) {
            base.Channel.GetApplications(clientId);
        }
        
        public void GetApplicationTables() {
            base.Channel.GetApplicationTables();
        }
        
        public void GetApplicationsTableColumns() {
            base.Channel.GetApplicationsTableColumns();
        }
    }
}
