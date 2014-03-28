using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using MobileHub.Repositories;
using MobileHub.Objects;
using MobileHub.Objects.Synchronization;
using MobileHub.Functionality.Clients;

namespace MobileHub.Functionality.Clients
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