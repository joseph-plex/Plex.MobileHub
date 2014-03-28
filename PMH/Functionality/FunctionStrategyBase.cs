using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;

namespace MobileHub.Functionality
{
    public interface IFunctionStrategy<T>
    {
        T Strategy(params object[] parameters);
    }
}