using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
namespace Plex.MobileHub.Functionality
{
    public class FunctionStrategyBase<T> : IFunctionStrategy<T>
    {
        public virtual T Strategy(params object[] parameters)
        {
            return (T)GetType().InvokeMember(MethodBase.GetCurrentMethod().Name, BindingFlags.InvokeMethod | BindingFlags.Public, Type.DefaultBinder, this, parameters);
        }
    }
}