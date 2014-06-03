using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Plex.MobileHub.Data.Tables;
using Plex.MobileHub.Data;
namespace Plex.MobileHub.Functionality.Clients
{
    public class AddLog : FunctionStrategyBase<int>
    {
        public int Strategy(DateTime logDate, string description, int id = 0)
        {
            LOGS logs;
            using(var connection = Utilities.GetConnection(true))
                (logs = new LOGS()
                {
                    DESCRIPTION = description,
                    LOG_DATE = logDate,
                    LOG_ID = Convert.ToInt32(connection.Query("select id_gen.nextval * dual")[0,0]),
                    CLIENT_ID = id
                }).Insert(connection);
            return logs.LOG_ID;
        }
    }
}