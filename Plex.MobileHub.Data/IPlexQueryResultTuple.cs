using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plex.MobileHub.Data
{
    public interface IPlexQueryResultTuple
    {
        Object this[int i] { get; set; }
        IList<Object> Values { get; set; }

    }
}
