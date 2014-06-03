using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Plex.MobileHub.Data.Tables;
using Plex.MobileHub.Data;

namespace Plex.MobileHub.Functionality.Clients
{
    public class RemoveClientUser : FunctionStrategyBase<Int32>
    {
        public int Strategy(int clientUserId)
        {
            CLIENT_USERS.GetAll().First(p => p.USER_ID == clientUserId).Delete();
            return 0;
        }
    }
}