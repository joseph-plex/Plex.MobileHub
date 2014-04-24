using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Plex.MobileHub.Repositories;
using Plex.MobileHub.Objects;
using Plex.MobileHub.Objects.Synchronization;
using Plex.MobileHub.Functionality.Clients;
using Plex.MobileHub.Data.Tables;

namespace Plex.MobileHub.Functionality.Clients
{
    public static partial class Functions
    {
        public static int ClientLogin(int ClientId, string Key, string IPv4, int Port)
        {
        
            var client = Connections.Instance.Retrieve(ClientId);
            if (Key != client.Key) 
                throw new Exception("Invalid Authentication");
            client.Connect(IPv4, Port);

            //todo Implement a way to valid on connection
            return ClientId;

             
        }
    }
}