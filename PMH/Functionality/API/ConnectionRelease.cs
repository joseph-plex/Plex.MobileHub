using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MobileHub.Objects;
using MobileHub.Repositories;
namespace MobileHub.Functionality.API
{
    public class ConnectionRelease : FunctionStrategyBase<object>
    {
        public MethodResult Strategy(int connectionId)
        {
            MethodResult mr = new MethodResult();
            try
            {
                if(Connections.Instance.ConnectionExists(connectionId))
                    Connections.Instance.Remove(connectionId);
                mr.Success();
            }
            catch(Exception e)
            {
                mr.Failure(e);
            }
            return mr;
        }
    }
}