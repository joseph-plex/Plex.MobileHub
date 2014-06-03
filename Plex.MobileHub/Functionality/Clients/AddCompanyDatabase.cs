using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Plex.MobileHub.Data.Tables;
using Plex.MobileHub.Data;


namespace Plex.MobileHub.Functionality.Clients
{
    public class AddCompanyDatabase : FunctionStrategyBase<int>
    {
        public int Strategy(int clientId, string companyCode, string  connectionString)
        {
            using(var connection = Utilities.GetConnection(true))
                new CLIENT_DB_COMPANIES()
                {
                    CLIENT_ID = clientId,
                    COMPANY_CODE = companyCode,
                    DATABASE_SID = connectionString,
                    DB_COMPANY_ID = Convert.ToInt32(connection.Query("select id_gen.nextval from dual")[0, 0])
                }.Insert(connection);
            return 0;
        }
    }
}