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
        public static MethodResult ClientRespond(Response Resp)
        {
            MethodResult mr = new MethodResult();
            try  {

                var Comm =  Commands.Instance.Retrieve(Resp.Id);
                Logs.Instance.Add(new Log("Command #" + Resp.Id + ": " + Comm + " Completed"));
                Responses.Instance.Add(Resp.Id, Resp);
                Commands.Instance.Remove(Resp.Id);
            }
            catch (Exception e) { mr.Failure(e); }
            return mr;
        }
    }
}