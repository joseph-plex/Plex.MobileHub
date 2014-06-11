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
public const string UserClientCompanyPermission =
@"
select 
  (select count(*) 
    from client_db_company_users cu 
    where cu.db_company_id = db.db_company_id
    and cu.user_id = u.user_id) has_Access, 
  db.company_code CompanyCode,
  db.db_company_id CompanyId
  from client_users u, 
    clients c, 
    client_db_companies db
  where u.client_id = c.client_id
    and c.client_id = db.client_id
    and u.user_id = :a
";
public const string UserClientCompany = @"select name from client_users t where client_id = :a";
public const string UserCompanyDbAppPermission = @"
select
  (
    select count(*) 
      from client_db_company_user_apps v 
      where v. db_company_user_app_id = dbua.db_company_user_app_id
  ) as has_access,
  a.title,
  a.description
  from client_db_company_users dbu
  right join client_users cu
    on cu.user_id = dbu.user_id
  join client_db_companies db
    on db.client_id = cu.client_id
  right join client_apps ca 
    join apps a
      on a.app_id = ca.app_id
    on ca.client_id = cu.client_id
  left join client_db_company_user_apps dbua
    on dbua.db_company_user_id = dbu.db_company_user_id
    and a.app_id = dbua.app_id
  where db.db_company_id = :a
    and cu.user_id = :b

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
