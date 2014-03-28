using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
namespace MobileHub.Functionality
{
    public class FunctionStrategyBase<T> : IFunctionStrategy<T>
    {
        public virtual T Strategy(params object[] parameters)
        {
            MethodBase current = MethodBase.GetCurrentMethod();
            return (T)GetType().InvokeMember(current.Name, BindingFlags.InvokeMethod | BindingFlags.Public, Type.DefaultBinder, this, parameters);
        }

    }
}