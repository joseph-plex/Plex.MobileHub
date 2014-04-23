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
        public static void ClientRespond(Response Resp)
    {
            Logs.Instance.Add(new Log("Command #" + Resp.Id +" Completed"));
            Responses.Instance.Add(Resp.Id, Resp);
        }
    }
}