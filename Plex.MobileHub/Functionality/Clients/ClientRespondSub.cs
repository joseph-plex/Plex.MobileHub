using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Plex.MobileHub.Repositories;
using Plex.MobileHub.Objects;
using Plex.MobileHub.Objects.Synchronization;
using Plex.MobileHub.Functionality.Clients;

namespace Plex.MobileHub.Functionality.Clients
{
    public static partial class Functions
    {
        public static MethodResult ClientRespondSub(ResponseComponent Component)
        {
            MethodResult mr = new MethodResult();
            try 
            {
                Responses.Instance.Add(Component.Id, Component);
                mr.Success();
            }
            catch (Exception e) { mr.Failure(e); }
            return mr;
        }
    }
}