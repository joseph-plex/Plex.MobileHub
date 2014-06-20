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
        IList<C> Columns { get; set; }
        IList<T> Tuples { get; set; }
        
    }
}
