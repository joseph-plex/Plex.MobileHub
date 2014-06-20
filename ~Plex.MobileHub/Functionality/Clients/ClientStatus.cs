using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Plex.MobileHub.Data.Tables;
using Plex.MobileHub.Data;
using Plex.MobileHub.Repositories;
using Plex.MobileHub.Objects.Clients;


namespace Plex.MobileHub.Functionality.Clients
{
    public class ClientStatus : FunctionStrategyBase<Int32>
    {
        public int Strategy(int clientId)
        {
            if (!Connections.Instance.ConnectionExists(clientId))
                return -1;
            return Connections.Instance.Retrieve(clientId).IsConnected ? 0 : -1;
        }
    }
}