using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.ServiceModel;
using System.ServiceModel.Description;
namespace MobileHubClient.Channels.External
{
    public class ExternalChannel : IClientChannel
    {
        
        public const int DefaultPort = 55556;
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

       
        public ExternalChannel()
            : this(DefaultIPAddress)
        {

        }

        public ExternalChannel(string address)
            : this(address, DefaultPort)
        {
            
        }

        public ExternalChannel(string address, int Port)
        {
            this.Port = Port;
            this.Name = "External";
            this.IP = IPAddress.Parse(address);
            this.CurrentState = IClientChannelState.Closed;
            StateBehaviours = new Dictionary<IClientChannelState, IClientChannelStateBehaviour>();

            StateBehaviours.Add(IClientChannelState.Closed, DefaultClosedStateBehavior.Create(this, typeof(ExternalService)));
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
