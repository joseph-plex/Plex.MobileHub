using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Plex.MobileHub.Data.Tables;
using Plex.MobileHub.Data;

namespace Plex.MobileHub.Functionality.Clients
{
    public class AddClientUser : FunctionStrategyBase<Int32>
    {
        int Strategy(int clientId, string name, string password)
        {
            CLIENT_USERS user = new CLIENT_USERS();
            using (var connection = Utilities.GetConnection(true))
                user.USER_ID = Convert.ToInt32(connection.Query("select id_gen.nextval from dual")[0, 0]);
            user.NAME = name;
            user.PASSWORD = password;
            user.CLIENT_ID = clientId;
            user.Insert();
            return user.USER_ID;
        }
    }
}