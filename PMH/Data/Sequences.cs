using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plex.PMH.Data
{
    public enum Sequences
    {
        DEVICE_ID,
        ID_GEN
    }

    //public static int SequenceNextValue(Sequences Seq)
    //{
    //    string sql = @"select ^seq^.nextval as num from dual";
    //    switch (Seq)
    //    {
    //        case Sequences.DEVICE_ID:
    //            sql = sql.Replace("^seq^", "DEVICE_ID");
    //            break;
    //        case Sequences.ID_GEN:
    //            sql = sql.Replace("^seq^", "ID_GEN");
    //            break;
    //        default:
    //            throw new NotSupportedException();
    //    }

    //    using (var Conn = GetConnection(true))
    //    using (var Comm = new OracleCommand(sql, Conn))
    //    using (var reader = Comm.ExecuteReader(CommandBehavior.CloseConnection))
    //        if (reader.Read()) return Convert.ToInt32(reader["num"]);
    //    throw new NotSupportedException();
    //}


    //public static QueryResult Query(string Query)
    //{
    //    QueryResult r = new QueryResult();
    //    using (var Conn = GetConnection(true))
    //    {
    //        using (var Comm = new OracleCommand(Query, Conn))
    //        {
    //            using (var reader = Comm.ExecuteReader(CommandBehavior.KeyInfo | CommandBehavior.CloseConnection))
    //            {
    //                r.Columns = new List<Column>(GetColumnData(reader.GetSchemaTable()));
    //                while (reader.Read())
    //                {
    //                    var CurrentRow = new Row();
    //                    foreach (var Col in r.Columns)
    //                        CurrentRow.Values.Add((reader[Col.ColumnName] != DBNull.Value) ? reader[Col.ColumnName] : null);
    //                    r.Rows.Add(CurrentRow);
    //                }
    //            }
    //        }
    //    }
    //    return r;
    //}

    //static IEnumerable<Column> GetColumnData(DataTable dataTable)
    //{
    //    foreach (var Row in dataTable.Rows)
    //        yield return GetColumnData((DataRow)Row);
    //}
    //static Column GetColumnData(DataRow Collection)
    //{
    //    return new Column()
    //    {
    //        ColumnName = (Collection[0] != DBNull.Value) ? Convert.ToString(Collection[0]) : string.Empty,
    //        IsUnique = (Collection[5] != DBNull.Value) ? Convert.ToBoolean(Collection[5]) : (bool?)null,
    //        IsKey = (Collection[6] != DBNull.Value) ? Convert.ToBoolean(Collection[6]) : (bool?)null,

    //        DataType = (Collection[11] != DBNull.Value) ? Collection[11].ToString() : string.Empty,
    //        IsReadOnly = (Collection[18] != DBNull.Value) ? Convert.ToBoolean(Collection[18]) : (bool?)null,
    //        IsLong = (Collection[19] != DBNull.Value) ? Convert.ToBoolean(Collection[19]) : (bool?)null,
    //    };
    //}
    //static OracleDbType GetOracleDbType(Object obj)
    //{
    //    if (obj is OracleDbType)
    //        return (OracleDbType)obj;
    //    else
    //        return OracleDbType.Varchar2;
    //}

}