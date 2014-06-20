using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MobileHub.Repositories;

namespace MobileHub.Functionality.Behaviors.API
{
    public class ConnectionStatusBehavior : RequestBehavior<ConnectionStatus>
    {
        public int ConnectionId
        {
            get;
            set;
        }

        public ConnectionStatus HandleRequest()
        {
            if (Consumers.Instance.Exists(ConnectionId)) return ConnectionStatus.Connected;
            else return ConnectionStatus.Disconnected;
        }
    }
}