using System;

namespace MobileHubClient.Channels
{
    public interface IClientChannelStateBehavior<T> : IClientChannelStateBehaviour
    {
        T Parent
        {
            get;
            set;
        }
    }

    public interface IClientChannelStateBehaviour
    {
        bool Open();
        bool Close();
    }
}
