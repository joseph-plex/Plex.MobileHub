using System;
using System.Net;
using System.ServiceModel;
using System.Collections.Generic;

namespace MobileHubClient.Channels.Logs
{
    public class LogsChannel : IClientChannel
    {
        public const int DefaultPort = 55555;
        public const string DefaultIPv4 = "127.0.0.1";

        public String Name
        {
            get;
            set;
        }

        public int Port
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
        public LogsChannel() :
            this(DefaultIPv4)
        {
        }
        public LogsChannel(string IPv4)
            : this(IPv4, DefaultPort)
        { 
        }


        public LogsChannel(String ipAddress, int Port)
        {
            this.Name = "Logs";
            this.Port = Port;
            this.IP = IPAddress.Parse(ipAddress);
            this.CurrentState = IClientChannelState.Closed;
            StateBehaviours = new Dictionary<IClientChannelState, IClientChannelStateBehaviour>();

            StateBehaviours.Add(IClientChannelState.Closed, DefaultClosedStateBehavior.Create(this, typeof(LogsService)));
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
