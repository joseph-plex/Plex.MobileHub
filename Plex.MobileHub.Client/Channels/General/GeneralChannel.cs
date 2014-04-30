using System;
using System.Net;
using System.ServiceModel;
using System.Collections.Generic;

namespace MobileHubClient.Channels.General
{
    public class GeneralChannel : IClientChannel
    {
        //todo Add events here for state changing
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

       
        public GeneralChannel()
            : this(DefaultIPAddress)
        {
        }

        public GeneralChannel(string address)
            : this(address, DefaultPort)
        {
        }

        public GeneralChannel(string address, int port)
        {
            this.Name = "General";
            this.Port = port;
            this.IP = IPAddress.Parse(address);
            this.CurrentState = IClientChannelState.Closed;
            StateBehaviours = new Dictionary<IClientChannelState, IClientChannelStateBehaviour>();

            StateBehaviours.Add(IClientChannelState.Closed, DefaultClosedStateBehavior.Create(this, typeof(GeneralService)));
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
