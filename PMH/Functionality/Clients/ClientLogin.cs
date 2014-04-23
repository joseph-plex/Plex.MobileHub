using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using MobileHub.Repositories;
using MobileHub.Objects;
using MobileHub.Objects.Synchronization;
using MobileHub.Functionality.Clients;
using MobileHub.Data.Tables;

namespace MobileHub.Functionality.Clients
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