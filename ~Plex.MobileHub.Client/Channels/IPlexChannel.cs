using System;
using System.Net;
using System.ServiceModel;
using System.Collections.Generic;
using MobileHubClient.Core;

namespace MobileHubClient.Channels
{
    public interface IClientChannel
    {
        IPAddress IP
        {
            get;
            set;
        }
        Int32 Port
        {
            get;
            set;
        }
        String Name
        {
            get;
            set;
        }

        ClientService Context
        {
            get;
            set;
        }

        ServiceHost Service
        {
            get;
            set;
        }
        IClientChannelState CurrentState
        {
            get;
            set;
        }
        Dictionary<IClientChannelState, IClientChannelStateBehaviour> StateBehaviours
        {
            get;
            set;
        }
        Boolean Open();
        Boolean Close();
    }
}
