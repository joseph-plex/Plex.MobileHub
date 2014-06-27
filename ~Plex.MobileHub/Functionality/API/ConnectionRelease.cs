using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Plex.MobileHub.Objects;
using Plex.MobileHub.Repositories;
namespace Plex.MobileHub.Functionality.API
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
                return new mr.Failure(e);
            }
        }
    }
}