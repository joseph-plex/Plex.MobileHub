using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plex.MobileHub.Repositories;

namespace Plex.MobileHub.Objects.Clients
{
    public interface IClientStateBehaviour
    {
        Int32 ClientId { get; set; }
        String Key { get; set; }
        String IPv4 { get; set; }
        Int32 Port { get; set; }

        Client Context
        {
            get;
        }

        T ExecuteRequest<T>(string commandName, params object[] arguments);
        void AdjustState();
        void RequestPull();
        void Connect(string address, int port);
        void CheckIn();
        void Disconnect();
    }
}
