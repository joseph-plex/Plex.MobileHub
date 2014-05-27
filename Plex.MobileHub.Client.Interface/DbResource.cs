using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class DbResource
{
    /// <summary>
    /// Determines what database a client has that a particular client user has access to.
    /// requires user Id
    /// </summary>
    public const string UserAccessForDatabase = @"select ( select count(*) from client_db_company_users a where b.db_company_id = a.db_company_id and c.user_id = a.user_id) as has_Access,  b.company_code, b.company_id, c.name, c.user_id from client_db_companies b, client_users c where c.client_id = b.client_id and c.user_id = :a";

    /// <summary>
    /// Does a client_db_company_user have access to client_db_company_user_app?
    /// requires user Id
    /// </summary>
    public const string UserDatabaseAppPermission = @"select a.app_id,a.title,  e.company_code, b.name, d.db_company_id, (select count(*) from client_db_company_user_apps c where a.app_id = c.app_id and c.db_company_user_id = d.db_company_user_id and rownum<=1) as permission from apps a, client_users b, client_db_company_users d, client_db_companies e where  d.user_id = b.user_id  and e.db_company_id = d.db_company_id and b.user_id = :a";
}
