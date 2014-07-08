using MobileHubClient.Channels;
using MobileHubClient.Channels.Database;
using MobileHubClient.Channels.External;
using MobileHubClient.Channels.General;
using MobileHubClient.Channels.Logs;
using MobileHubClient.Data;
using MobileHubClient.Misc;
using System;
using System.Data;
using System.Collections.Generic;
using System.ServiceProcess;
using System.Linq;
using System.Timers;
using Plex.Logs;
using Oracle.DataAccess.Client;
using MobileHubClient.PMH;
using MobileHubClient.ComCallbacks;


namespace MobileHubClient.Core
{
    public class ClientService : ServiceBase, IClientStateBehaviour
    {
        static public LogManager Logs = new LogManager();
        
        public delegate void Subscriber(object sender, EventArgs e);
        public ClientDbConnectionFactory DbFactory = new ClientDbConnectionFactory(true);
        
        internal Dictionary<ClientServiceState, IClientStateBehaviour> StateBehaviours;
        internal List<IClientChannel> clientChannels;
        internal ClientServiceState CurrentState;
        internal int clientInstanceId = 0;
        internal Timer checkInTimer;

        public IReadOnlyCollection<CLIENT_DB_COMPANIES> DbConnections
        {
            get
            {
                return StateBehaviours[CurrentState].DbConnections;
            }
        }

        ClientSettings Settings;

        event Subscriber OnLogOff;
        event Subscriber OnLogOn;
        
        public ClientService()
        {
            InitializeComponent();
            Settings = ClientSettings.Instance;
            Functions.Context = this;
        }
        public void OnDebug()
        {
            OnStart(null);
        }

        protected override void OnStart(string[] args)
        {
            bool CommitLogs = false;
            using(var LogCache = Logs.CreateLogCache())
            {
                LogCache.Add("Client Service - Initialization");
                //Set Instance Variables
                checkInTimer = new Timer();
                checkInTimer.Elapsed += (s, e) => LogOn();
                checkInTimer.Interval = ClientSettings.Instance.CheckInTimer;

                PlexServiceBase.Context = this;
                clientChannels = new List<IClientChannel>();
                CurrentState = ClientServiceState.Disconnected;
                StateBehaviours = new Dictionary<ClientServiceState, IClientStateBehaviour>();
                StateBehaviours.Add(ClientServiceState.Connected, new ClientStateConnected() { Context = this });
                StateBehaviours.Add(ClientServiceState.Disconnected, new ClientStateDisconnected() { Context = this });

                //Subscribers to events
                LogCache.Add("Client Service - Opening Communication Channels");

                OnLogOn += (s, e) => CurrentState = ClientServiceState.Connected;
                OnLogOn += (s, e) => ExternalService.Context = this;
                OnLogOn += (S, e) => checkInTimer.Enabled = true;

                OnLogOff += (s, e) => CurrentState = ClientServiceState.Disconnected;
                OnLogOff += (s, e) => clientInstanceId = 0;

                clientChannels.Add(new LogsChannel() { Context = this });
                clientChannels.Add(new GeneralChannel() { Context = this });
                clientChannels.Add(new ExternalChannel() { Context = this });
                clientChannels.Add(new DatabaseChannel() { Context = this });

                clientChannels.ForEach(a => a.Open());
                LogCache.Add(ClientSettings.Instance.ToString("x"));

                try { 
                    if (ClientSettings.Instance.AutoLogOn) 
                        LogOn();
                }
                catch(Exception x)  {
                    LogCache.Add(x);
                    CommitLogs = true;
                }
                LogCache.Add("Client Service - Ready");
                if (CommitLogs)
                    LogCache.CommitCache();
            }
        }

        protected override void OnStop()
        {
            clientChannels.ForEach(a => a.Close());
            LogOff();
        }

        #region Behavior Implementations
        public void LogOn()
        {
            StateBehaviours[CurrentState].LogOn();
            OnLogOn(this, EventArgs.Empty);
        }

        public void LogOff()
        {
            StateBehaviours[CurrentState].LogOff();
            OnLogOff(this, EventArgs.Empty);
        }

        public IDbConnection GetOpenConnection(String Code)
        {
            return StateBehaviours[CurrentState].GetOpenConnection(Code);
        }
        #endregion

        #region Generated Code (Do Not Modify)
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
            this.ServiceName = "Plexxis Mobile Hub Client";
        }
        #endregion
    }
}
