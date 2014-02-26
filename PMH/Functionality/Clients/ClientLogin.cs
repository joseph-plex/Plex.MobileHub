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
        public static MethodResult ClientLogin(int ClientId, string Key, string IPv4, int Port)
        {
            MethodResult mr = new MethodResult();
            try{ mr.Success(Connections.Instance.Add(new ConnectionData() { ClientId = ClientId, Key = Key, IPv4 = IPv4, Port = Port })); }
            catch (Exception e) { mr.Failure(e); }
            return mr;
        }
    }
}