using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileHubClient.PMH;
using MobileHubClient.Data;
using System.Data;
using MobileHubClient.Core;
namespace MobileHubClient.ComCallbacks
{
    public static partial class Functions
    {
        public static MethodResult IUD(String Code, IUDData [] Data)
        {
            String sql = String.Empty;
            MethodResult mr = new MethodResult();

            using(var Conn = Context.GetOpenConnection(Code))
            using(var transaction = Conn.BeginTransaction())
            {
                foreach(var iud in Data)
                {
                    bool SinglePK = ValidForOperation(Conn, iud.TableName);
                    foreach( var row in iud.Rows)
                    {
                        switch(row.DBAction)
                        {
                            case 1:
                                sql = CreateInsertCommandText(iud.TableName, iud.ColumnNames);
                                Conn.NonQuery(sql, row.Values);
                                ClientService.Logs.Add(sql);
                                break;
                            case 2:
                                if (!SinglePK)
                                    throw new InvalidOperationException("Multiple PK, operation cannot be completed");
                                sql = CreateUpdateCommandText(iud.TableName, iud.ColumnNames);
                                Conn.NonQuery(sql, row.Values);
                                ClientService.Logs.Add(sql);
                                break;
                            case 3:
                                if (!SinglePK)
                                    throw new InvalidOperationException("Multiple PK, operation cannot be completed");
                                sql = CreateDeleteCommandText(iud.TableName, iud.ColumnNames);
                                Conn.NonQuery(sql, row.Values);
                                ClientService.Logs.Add(sql);
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            return mr;
        }

        static string CreateInsertCommandText(String tableName, IEnumerable<String> cols)
        {
            var columnNames = cols.ToArray();
            String sql = "INSERT INTO {0}({1}) VALUES({2})";

            String phVar = ":var", @values = phVar + new int();
            String @columns = columnNames.First();

            //0 Base array's first value was already processed so start @ 1
            for(int i = 1; i < columnNames.Length; i++ )
            {
                @values += ", " + phVar + i;
                @columns += ", " + columnNames[i];
            }

            //For debugging so we can see what the value is before returning.
            sql = String.Format(sql, tableName, @columns, @values);
            return sql;
        }


        //This will fail if there is a composite primary key.
        static string CreateUpdateCommandText(string tableName, IEnumerable<String> cols)
        {
            var columnNames = cols.ToArray();
            String sql = "UPDATE {0} SET {1} WHERE {2}";
            //First value is PK and won't be changed, start at second value.
            String phVar = ":var", @values = columnNames[1] + "=" + phVar + 1;

            //Second value already processed, start @ 3rd.
            for (int i = 2; i < columnNames.Length; i++)
                @values += ", " + columnNames[i] + "=" + phVar + i;

            sql = String.Format(sql, tableName, @values, columnNames.First() + " = " + phVar + 0);
            return sql;
        }

        //This will do the wrong thing if there is a composite primary key.
        static string CreateDeleteCommandText(String tableName, IEnumerable<String> cols)
        {
            String sql = "DELETE FROM {0} WHERE {1}";
            sql = String.Format(sql, tableName, cols.First() + " = :var0");
            return sql;

        }

        static bool ValidForOperation(IDbConnection connection, string tableName)
        {
            String sql = String.Empty;
            sql += "select count(*) as PKCOUNT";
            sql += "  from user_constraints uc";
            sql += "  inner join user_cons_columns ucc";
            sql += "    on ucc.CONSTRAINT_NAME = uc.CONSTRAINT_NAME";
            sql += "  where uc.CONSTRAINT_TYPE = 'P'";
            sql += "    and uc.TABLE_NAME = :a";

            if (Convert.ToInt32(connection.Query(sql, tableName)[0, 0]) == 1)
                return true;
            return false;
        }
    }
}
