using System;
using System.Net;
using System.ServiceModel;
using System.Collections.Generic;
using System.ServiceModel.Description;

namespace MobileHubClient.Channels
{
    public class DefaultOpenedStateBehavior : IClientChannelStateBehavior<IClientChannel>
    {
        public IClientChannel Parent
        {
            get;
            set;
        }
        
        public bool Open()
        {
            throw new InvalidOperationException("The Channel is already open");
        }

        public bool Close()
        {
            if (Parent.Service.State == CommunicationState.Faulted) Parent.Service.Abort();
            else Parent.Service.BeginClose((IAsyncResult ar) => { ((ServiceHost)ar.AsyncState).EndClose(ar); }, Parent.Service);
            Parent.CurrentState = IClientChannelState.Closed;
            return true;
        }

        private DefaultOpenedStateBehavior() { }

        public static DefaultOpenedStateBehavior Create(IClientChannel channel)
        {
            return new DefaultOpenedStateBehavior() { Parent = channel };
        }
    }
}
