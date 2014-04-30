using System;
using System.Net;
using System.ServiceModel;
using System.Collections.Generic;

namespace MobileHubClient.Channels.Database
{
    public class DatabaseChannel : IClientChannel
    {

        public const int DefaultPort = 55555;
        public const string DefaultIPAddress = "127.0.0.1";
        public String Name
        {
            get;
            set;
        }
        public Int32 Port
        {
            get;
            set;
        }
        public IPAddress IP
        {
            get;
            set;
        }
        public ClientService Context
        {
            get;
            set;
        }

        public ServiceHost Service
        {
            get;
            set;
        }

        public IClientChannelState CurrentState
        {
            get;
            set;
        }
        public Dictionary<IClientChannelState, IClientChannelStateBehaviour> StateBehaviours
        {
            get;
            set;
        }

        IClientChannelStateBehaviour CurrentBehavior
        {
            get
            {
                return StateBehaviours[CurrentState];
            }
        }

        public DatabaseChannel()
            : this(DefaultIPAddress)
        {
        }

        public DatabaseChannel(string address)
            : this(address, DefaultPort)
        {

        }

        public DatabaseChannel(string address, int port)
        {
            this.Port = port;
            this.Name = "Database";
            this.IP = IPAddress.Parse(address);
            this.CurrentState = IClientChannelState.Closed;
            StateBehaviours = new Dictionary<IClientChannelState, IClientChannelStateBehaviour>();

            StateBehaviours.Add(IClientChannelState.Closed, DefaultClosedStateBehavior.Create(this, typeof(DatabaseService)));
            StateBehaviours.Add(IClientChannelState.Opened, DefaultOpenedStateBehavior.Create(this));
        }
        public bool Open()
        {
            return CurrentBehavior.Open();
        }

        public bool Close()
        {
            return CurrentBehavior.Close();
        }
    }
}
