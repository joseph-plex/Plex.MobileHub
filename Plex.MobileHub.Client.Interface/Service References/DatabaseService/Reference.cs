﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Plex.MobileHub.Client.Interface.DatabaseService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="PMHC", ConfigurationName="DatabaseService.DatabaseService")]
    public interface DatabaseService {
        
        [System.ServiceModel.OperationContractAttribute(Action="PMHC/DatabaseService/Reset", ReplyAction="PMHC/DatabaseService/ResetResponse")]
        bool Reset();
        
        [System.ServiceModel.OperationContractAttribute(Action="PMHC/DatabaseService/GetDataMap", ReplyAction="PMHC/DatabaseService/GetDataMapResponse")]
        string GetDataMap();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface DatabaseServiceChannel : Plex.MobileHub.Client.Interface.DatabaseService.DatabaseService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class DatabaseServiceClient : System.ServiceModel.ClientBase<Plex.MobileHub.Client.Interface.DatabaseService.DatabaseService>, Plex.MobileHub.Client.Interface.DatabaseService.DatabaseService {
        
        public DatabaseServiceClient() {
        }
        
        public DatabaseServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public DatabaseServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public DatabaseServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public DatabaseServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public bool Reset() {
            return base.Channel.Reset();
        }
        
        public string GetDataMap() {
            return base.Channel.GetDataMap();
        }
    }
}
