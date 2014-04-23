using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using MobileHub.Repositories;
using MobileHub.Objects;
using MobileHub.Objects.Synchronization;
using MobileHub.Functionality.Clients;
using MobileHub.Data.Tables;

namespace MobileHub.Functionality.Clients
{
    public static partial class Functions
    {
        public static void  ClientLogout(int ConnectionId)
        {
            Connections.Instance.Retrieve(ConnectionId).Disconnect();
        }
    }
}