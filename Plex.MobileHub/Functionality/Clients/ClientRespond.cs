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
        public static void ClientRespond(Response Resp)
    {
            Logs.Instance.Add(new Log("Command #" + Resp.Id +" Completed"));
            Responses.Instance.Add(Resp.Id, Resp);
        }
    }
}