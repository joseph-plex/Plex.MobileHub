using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Plex.PMH.Repositories;
using Plex.PMH.Objects;

namespace Plex.PMH.Functionality.API
{
    public static partial class Functions
    {
        public static ConnectionStatus ConnectionGetStatus(int ConnectionId)
        {
            if (Consumers.Instance.Exists(ConnectionId)) return ConnectionStatus.Connected;
            else return ConnectionStatus.Disconnected;
            
        }
    }
}