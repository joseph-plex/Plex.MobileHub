using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Plex.MobileHub.Repositories;
using Plex.MobileHub.Objects;
using Plex.MobileHub.Objects.ResultTypes;

namespace Plex.MobileHub.Functionality.Clients
{
    public static partial class Functions
    {
        public static List<Command> ClientCommandRetrieveAll(int ConnectionId)
        {

            if (!Connections.Instance.ConnectionExists(ConnectionId))
                throw new Exception("Invalid Connection Id");

            var Conn = Connections.Instance.Retrieve(ConnectionId);
            var Col = Commands.Instance.CommandRepo.Values.Where(p => p.ClientId == Conn.ClientId);

            return Col.ToList();
        }
    }
}