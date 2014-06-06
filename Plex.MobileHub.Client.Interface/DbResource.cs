using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plex.MobileHub.Client.Interface.GeneralService;

static class DbResource
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
public const string UserDatabaseAppPermission = @"select a.app_id,a.title,  e.company_code, b.name, d.db_company_id, (select count(*) from client_db_company_user_apps c where a.app_id = c.app_id and c.db_company_user_id = d.db_company_user_id and rownum<=1) as permission from apps a, client_users b, client_db_company_users d, client_db_companies e where  d.user_id = b.user_id  and    e.db_company_id = d.db_company_id and b.user_id = :a";
/// <summary>
/// Does get the permissions from a user on a particular client
/// </summary>
public const string UserClientCompanyPermission = @"
select (select count(*) from client_db_company_users p where p.user_id = c.user_id and p.db_company_id = b.db_company_id) as permission, b.company_code
from client_db_companies b, client_users c
where
  b.client_id = c.client_id
  and c.user_id = :a";
public const string UserClientCompany = @"select name from client_users t where client_id = :a";
public const string UserCompanyDbAppPermission =
    @"
select distinct
  (select count(*) 
    from client_db_company_user_apps a 
    where a.db_company_user_id = c.db_company_user_id
    and a.app_id = d.app_id) permission,
  d.title,
  d.description
  from client_db_company_user_apps b, 
    client_db_company_users c,
    apps d,
    client_apps e,
    client_db_companies f
  where c.db_company_user_id = b.db_company_user_id
  and f.db_company_id = c.db_company_id
  and d.app_id = e.app_id
  and f.company_code = :a
  and c.user_id = :b
";

    public static object GetValue(this Result result, int columnIndex, int rowIndex)
    {
        return result.Rows[rowIndex].Values[columnIndex];
    }

    public static object GetValue(this Result result, string columnName, int rowIndex)
    {
        return result.Rows[rowIndex].Values[result.GetColumnIndex(columnName)];
    }
    static public int GetColumnIndex(this Result result, String columnName)
    {
        return result.Columns.FindIndex((p) => p.ColumnName.Equals(columnName, StringComparison.OrdinalIgnoreCase));
    }
}
