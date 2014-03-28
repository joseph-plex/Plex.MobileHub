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
        public static MethodResult ClientLogout(int ConnectionId)
        {
            MethodResult mr = new MethodResult();
            try 
            {
                var connection = Connections.Instance.Retrieve(ConnectionId);
                connection.Disconnect();
                mr.Success();
            }
            catch (Exception e) { mr.Failure(e); }
            return mr;
        }
    }
}