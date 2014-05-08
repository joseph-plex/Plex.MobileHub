using MobileHubClient.Channels;
using MobileHubClient.Channels.Database;
using MobileHubClient.Channels.External;
using MobileHubClient.Channels.General;
using MobileHubClient.Channels.Logs;
using MobileHubClient.Data;
using MobileHubClient.Logs;
using MobileHubClient.Misc;
using System;
using System.Collections.Generic;
using System.ServiceProcess;
using System.Timers;
namespace MobileHubClient.Core
{
    public class ClientService : ServiceBase
    {
        public delegate void Subscriber(object sender, EventArgs e);
        
        internal Dictionary<ClientServiceState, IClientStateBehaviour> StateBehaviours;
        internal List<IClientChannel> clientChannels;
        internal ClientServiceState CurrentState;
        internal int clientInstanceId = 0;
        internal Timer checkInTimer;

        ClientSettings Settings;

        event Subscriber OnLogOff;
        event Subscriber OnLogOn;
        
        public ClientService()
        {
            InitializeComponent();
            Settings = ClientSettings.Instance;
        }

        protected override void OnStart(string[] args)
        {

            //Set Instance Variables
            checkInTimer = new Timer();
            checkInTimer.Elapsed += (s,e)=> LogOn();
            checkInTimer.Interval = ClientSettings.Instance.CheckInTimer;

            PlexServiceBase.Context = this;
            clientChannels = new List<IClientChannel>();
            CurrentState = ClientServiceState.Disconnected;
            StateBehaviours = new Dictionary<ClientServiceState, IClientStateBehaviour>();
            StateBehaviours.Add(ClientServiceState.Connected,  new ClientStateConnected() { Context = this});
            StateBehaviours.Add(ClientServiceState.Disconnected, new ClientStateDisconnected() { Context = this });
            
            //Subscribers to events
            OnLogOn += (s, e) => CurrentState = ClientServiceState.Connected;
            OnLogOn += (s, e) => ExternalService.Context = this;
            OnLogOn += (S,e)=> checkInTimer.Enabled = true;
            
            OnLogOff += (s, e) => CurrentState = ClientServiceState.Disconnected;
            OnLogOff += (s, e) => clientInstanceId = 0;

            clientChannels.Add(new LogsChannel() { Context = this });
            clientChannels.Add(new GeneralChannel() { Context = this });
            clientChannels.Add(new ExternalChannel() { Context = this });
            clientChannels.Add(new DatabaseChannel() { Context = this });

            clientChannels.ForEach(a => a.Open());

            if (ClientSettings.Instance.AutoLogOn)
                LogOn();
        }

        protected override void OnStop()
        {
            clientChannels.ForEach(a => a.Close());
            LogOff();
        }

        internal void LogOn()
        {
            StateBehaviours[CurrentState].LogOn();
            OnLogOn(this, EventArgs.Empty);
        }

        internal void LogOff()
        {
            StateBehaviours[CurrentState].LogOff();
            OnLogOff(this, EventArgs.Empty);
        }

        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            this.ServiceName = "Plexxis Mobile Hub Client";
        }
    }
}
