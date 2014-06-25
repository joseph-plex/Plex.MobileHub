using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plex.MobileHub.Data
{
    public interface IPlexQueryResult<C,T>
        where T: IPlexQueryResultTuple
        where C: IPlexQueryResultColumn 
    {
        Object this[Int32 ColumnIndex, Int32 RowIndex]{ get; }
        Object this[String ColumnName, Int32 RowIndex]{ get; }
        IEnumerable<Object> this[Int32 ColumnIndex] { get; }
        IEnumerable<Object> this[String ColumnName] { get; }
        List<C> Columns { get; set; }
        List<T> Tuples { get; set; }
        
    }
}
