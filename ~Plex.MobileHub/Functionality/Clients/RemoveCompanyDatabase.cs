using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Plex.MobileHub.Data.Tables;
using Plex.MobileHub.Data;

namespace Plex.MobileHub.Functionality.Clients
{
    public class RemoveCompanyDatabase : FunctionStrategyBase<Int32>
    {
        public int Strategy(int clientDbCompanyId)
        {
            CLIENT_DB_COMPANY_USERS.GetAll().First(p => p.USER_ID == clientDbCompanyId).Delete();
            return 0;
        }

    }
}