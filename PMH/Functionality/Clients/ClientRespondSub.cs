using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Plex.PMH.Repositories;
using Plex.PMH.Objects;
using Plex.PMH.Objects.Synchronization;
using Plex.PMH.Functionality.Clients;

namespace Plex.PMH.Functionality.Clients
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