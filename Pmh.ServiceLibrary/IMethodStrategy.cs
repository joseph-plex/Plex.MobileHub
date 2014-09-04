using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pmh.ServiceLibrary
{
    public interface IMethodStrategy<T>
    {
        T Strategy(params object[] argument);
    }
}
