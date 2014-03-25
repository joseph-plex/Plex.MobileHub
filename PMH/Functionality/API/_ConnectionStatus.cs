using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using MobileHub.Repositories;
using MobileHub.Objects;

namespace MobileHub.Functionality.API
{
    public static partial class Functions
    {
        public static int ConnectionGetStatus(int ConnectionId)
        {
            if (Consumers.Instance.Exists(ConnectionId)) return 0;
            else return 1;
        }
    }
}