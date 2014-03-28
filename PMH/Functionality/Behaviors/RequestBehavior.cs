using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileHub.Functionality.Behaviors
{
    public interface RequestBehavior<T>
    {
        T HandleRequest();
    }
}
