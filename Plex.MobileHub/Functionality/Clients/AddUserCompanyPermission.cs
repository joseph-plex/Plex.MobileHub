using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Plex.MobileHub.Data.Tables;
using Plex.MobileHub.Data;

namespace Plex.MobileHub.Functionality.Clients
{
    public class AddUserCompanyPermission : FunctionStrategyBase<Int32>
    {
        public int Strategy(int appId, int dbCompanyUserId, int appUserTypeId = 0)
        {
            using(var connection = Utilities.GetConnection(true))
                new CLIENT_DB_COMPANY_USER_APPS()
                {
                    APP_ID = appId,
                    DB_COMPANY_USER_ID = dbCompanyUserId,
                    APP_USER_TYPE_ID = ((appUserTypeId == 0)? (int?)null : appUserTypeId),
                    DB_COMPANY_USER_APP_ID = Convert.ToInt32(connection.Query("select id_gen.nextval from dual")[0, 0])
                }.Insert(connection);
            return 0;
        }
    }
}