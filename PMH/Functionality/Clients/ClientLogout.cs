using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Plex.MobileHub.Repositories;
using Plex.MobileHub.Objects;
using Plex.MobileHub.Objects.Synchronization;
using Plex.MobileHub.Functionality.Clients;
using Plex.MobileHub.Data.Tables;

namespace Plex.MobileHub.Functionality.Clients
{
    public static partial class Functions
    {
        public static void  ClientLogout(int ConnectionId)
        {
            Connections.Instance.Retrieve(ConnectionId).Disconnect();
        }
    }
}