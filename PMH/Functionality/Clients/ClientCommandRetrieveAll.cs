using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using MobileHub.Repositories;
using MobileHub.Objects;
using MobileHub.Objects.ResultTypes;

namespace MobileHub.Functionality.Clients
{
    public static partial class Functions
    {
        public static GetCommandsMethodResult ClientCommandRetrieveAll(int ConnectionId)
        {
            var mr = new GetCommandsMethodResult();
            try 
            { 
                if (!Connections.Instance.ConnectionExists(ConnectionId))
                    throw new Exception("Invalid Connection Id");

                var Conn = Connections.Instance.Retrieve(ConnectionId);
                var Col = Commands.Instance.CommandRepo.Values.Where(p => p.ClientId == Conn.ClientId);

                mr.Commands = Col.ToList();
                mr.Success();
            }
            catch(Exception e)
            { mr.Failure(e); }
            return mr;

        }
    }
}