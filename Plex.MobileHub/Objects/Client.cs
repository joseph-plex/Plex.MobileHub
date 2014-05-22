using System;
using System.Linq;
using System.Collections.Generic;
using System.ServiceModel;
using Plex.MobileHub.Repositories;
using System.Timers;
using Plex.MobileHub.Objects.Clients;
using Plex.MobileHub.Properties;
using System.Xml.Serialization;
using System.Collections;
namespace Plex.MobileHub.Objects
{
    public class Client 
    {
        public delegate void Subscriber(object sender, EventArgs e);

        public event Subscriber OnConnect;
        public event Subscriber OnDisconnect;

        [XmlIgnore]
        public ClientState currentState
        {
            get;
            private set;
        }

        public virtual Int32 ClientId
        {
            get
            {
                return stateBehaviour[currentState].ClientId;
            }
            set
            {
                stateBehaviour[currentState].ClientId = value;
            }
        }
        public virtual String Key
        {
            get
            {
                return stateBehaviour[currentState].Key;
            }
            set
            {
                stateBehaviour[currentState].Key = value;
            }
        }

        public virtual String IPv4
        {
            get
            {
                return stateBehaviour[currentState].IPv4;
            }
            set
            {
                stateBehaviour[currentState].IPv4 = value;
            }
        }
        public virtual Int32 Port
        {
            get
            {
                return stateBehaviour[currentState].Port;
            }
            set
            {
                stateBehaviour[currentState].Port = value;
            }
        }

        public virtual IReadOnlyCollection<Command> QueuedRequests
        {
            get
            {
                return queuedRequests;
            }
        }

        protected internal int port;
        protected internal string key;
        protected internal int clientId;
        protected internal string address;
        protected internal Timer connectionTimeout;
      
        protected internal Dictionary<ClientState, IClientStateBehaviour> stateBehaviour;

        protected internal List<Command> queuedRequests;

        public Client()
        {
            queuedRequests = new List<Command>();

            connectionTimeout = new Timer();
            connectionTimeout.Enabled = false;
            connectionTimeout.Interval = Settings.Default.ClientConnectionTimeout;

            Settings.Default.PropertyChanged += (s, e) => connectionTimeout.Interval = Settings.Default.ClientConnectionTimeout;  

            currentState = ClientState.Disconnected;

            OnConnect += (object sender, EventArgs e) => currentState = ClientState.Connected;
            OnDisconnect += (object sender, EventArgs e) => currentState = ClientState.Disconnected;

            stateBehaviour = new Dictionary<ClientState, IClientStateBehaviour>();
            stateBehaviour.Add(ClientState.Connected, new ClientConnected() { Context = this });
            stateBehaviour.Add(ClientState.Disconnected, new ClientDisconnected() { Context = this });
        }

        public virtual T ExecuteRequest<T>(string commandName, params object [] arguments)
        {
            return stateBehaviour[currentState].ExecuteRequest<T>(commandName, arguments);
        }

        public virtual void RequestPull()
        {
            stateBehaviour[currentState].RequestPull();
        }
        public virtual void AdjustState()
        {
            stateBehaviour[currentState].AdjustState();
        }
        public virtual void Connect(string address, int port)
        {
            stateBehaviour[currentState].Connect(address,port);
            OnConnect(this,EventArgs.Empty);
        }
        public virtual void Disconnect()
        {
            stateBehaviour[currentState].Disconnect();
            OnDisconnect(this, EventArgs.Empty);
        }
    }
}