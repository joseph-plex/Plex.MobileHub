using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Plex.MobileHub.Exceptions;
using Plex.MobileHub.Data.Tables;

using System.Net;
using System.Net.Sockets;

using System.Diagnostics;
using System.Threading;
using Plex.MobileHub.Objects;
using Plex.MobileHub.Objects.Clients;

namespace Plex.MobileHub.Repositories
{
    public class Connections
    {
        private static Connections Repo = new Connections();
        public static Connections Instance
        {
            get{
                return Repo;
            }
        }

        private Dictionary<int, Client> ConnectionRepo;

        private Connections()
        {
            ConnectionRepo = new Dictionary<int, Client>();
        }

        public Dictionary<int, Client> GetAll() 
        {
            return new Dictionary<int, Client>(ConnectionRepo);
        }

        public int Add(Client Data)
        {
            var clients = CLIENTS.GetAll().ToList();
            var index = clients.FindIndex((p) => p.CLIENT_ID == Data.ClientId);

            //Ensure data is all valid
            if (index == -1) throw new InvalidClientException();
            if (clients[index].CLIENT_KEY != Data.Key) throw new Exception("Invalid Client Key");
            if (ConnectionRepo.Values.ToList().Exists((p) => p.ClientId == Data.ClientId)) throw new MultipleClientLoginException();

            ConnectionRepo.Add(Data.ClientId, Data);

            return Data.ClientId;
        }

        public void Modify(Client Data)
        {
            if (!CLIENTS.GetAll().ToList().Exists((p) => p.CLIENT_ID == Data.ClientId && p.CLIENT_KEY == Data.Key))
                throw new Exception("Invalid Client Credentials");

            ConnectionRepo[Data.ClientId] = Data;
        }

        public void Remove(int i)
        {
            if(ConnectionRepo.ContainsKey(i))
                ConnectionRepo.Remove(i);
        }

        public Client Retrieve(int i)
        {
            if (!ConnectionExists(i))
                throw new Exception("No Client With Specified Id does not Exists");
            return ConnectionRepo[i];
        }

        public bool ConnectionExists(int Id) 
        {
            return ConnectionRepo.ContainsKey(Id);
        }

    }
}

