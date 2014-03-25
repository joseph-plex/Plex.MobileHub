using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MobileHub.Repositories;
namespace MobileHub.Functionality.API
{
    public static partial class Functions
    {
        public static void ConnectionRelease(int ConnectionId)
        {
            //todo create a proper exception for this.
            if (!Consumers.Instance.Exists(ConnectionId)) throw new Exception();
            Consumers.Instance.Remove(ConnectionId);
        }
    }
}