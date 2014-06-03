using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Plex.MobileHub.Data.Tables;
using Plex.MobileHub.Data;

namespace Plex.MobileHub.Functionality.Clients
{
    public class RemoveUserCompanyPermission : FunctionStrategyBase<Int32>
    {
        public int Strategy(int clientDbCompanyUserAppId)
        {
            CLIENT_DB_COMPANY_USER_APPS.GetAll().First(p => p.DB_COMPANY_USER_APP_ID == clientDbCompanyUserAppId).Delete();
            return 0;
        }
    }
}