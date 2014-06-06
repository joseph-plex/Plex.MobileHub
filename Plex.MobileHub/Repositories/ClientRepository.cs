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
        
        /// <summary>
        /// External Use
        /// </summary>
        public static void RestartSingleton(){
            Repo = new Connections();
        }

        static Connections GetNewInstance()
        {
            return new Connections();
        }

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
            foreach (var client in CLIENTS.GetAll())
            {
                Client data = new Client();
                data.ClientId = client.CLIENT_ID;
                data.Key = client.CLIENT_KEY;
                data.AdjustState();
                Add(data);
            }
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
            var client = CLIENTS.GetAll().ToList().FirstOrDefault((p) => p.CLIENT_ID == Data.ClientId && p.CLIENT_KEY == Data.Key);

            if(client == null)
                throw new Exception("Invalid Client Credentials");

            if(!ConnectionExists(Data.ClientId))
            {
                Client data = new Client() { ClientId = client.CLIENT_ID, key = client.CLIENT_KEY};
                data.AdjustState();
                Add(data);
            }

            ConnectionRepo[Data.ClientId] = Data;
        }

        public void Remove(int i)
        {
            var client = CLIENTS.GetAll().ToList().FirstOrDefault((p) => p.CLIENT_ID == i);

            if(client == null)
                throw new Exception("Invalid Client Id");

            if(!ConnectionExists(i))
            {
                Client data = new Client() { ClientId = client.CLIENT_ID, key = client.CLIENT_KEY };
                data.AdjustState();
                Add(data);
            }
            ConnectionRepo.Remove(i);
        }

        public Client Retrieve(int i)
        {
            var client = CLIENTS.GetAll().ToList().FirstOrDefault((p) => p.CLIENT_ID == i);

            if (client == null)
                throw new Exception("Invalid Client Id");
                
            if(!ConnectionExists(i))
            {
                Client data = new Client() { ClientId = client.CLIENT_ID, key = client.CLIENT_KEY };
                data.AdjustState();
                Add(data);
            }
            return ConnectionRepo[i];
        }

        public bool ConnectionExists(int Id) 
        {
            return ConnectionRepo.ContainsKey(Id);
        }

    }
}

