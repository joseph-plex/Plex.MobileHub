﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Plex.PMH.Exceptions;
using Plex.PMH.Data.Tables;

using System.Net;
using System.Net.Sockets;

using System.Diagnostics;
using System.Threading;

namespace Plex.PMH.Repositories
{
    public class Connections
    {
        //change this value
        private const int OfflineTimer = 60000;

        private static Connections Repo = new Connections();
        public static Connections Instance
        {
            get{
                return Repo;
            }
        }

        private Dictionary<int, ConnectionData> ConnectionRepo;

        private Connections()
        {
            ConnectionRepo = new Dictionary<int, ConnectionData>();
        }

        public Dictionary<int, ConnectionData> GetAll() 
        {
            return new Dictionary<int, ConnectionData>(ConnectionRepo);
        }

        public void Add(ConnectionData Data)
        {
            //todo make a custom exception for this (or find one)

            var clients = CLIENTS.GetAll().ToList();
            var index = clients.FindIndex((p) => p.CLIENT_ID == Data.ClientId);

            //Ensure data is all valid
            if (index == -1) throw new InvalidClientException();
            if (clients[index].CLIENT_KEY != Data.Key) throw new Exception("Invalid Client Key");
            if (ConnectionRepo.Values.ToList().Exists((p) => p.ClientId == Data.ClientId)) throw new MultipleClientLoginException();

            Data.InitTime = DateTime.Now;
            Data.LastCheck = Stopwatch.StartNew();

            ConnectionRepo.Add(Data.ClientId, Data);
        }

        public void Modify(ConnectionData Data)
        {
            if (!CLIENTS.GetAll().ToList().Exists((p) => p.CLIENT_ID == Data.ClientId && p.CLIENT_KEY == Data.Key))
                throw new Exception("Invalid Client Credentials");

            Data.InitTime = DateTime.Now;
            Data.LastCheck = Stopwatch.StartNew();

            ConnectionRepo[ConnectionId] = Data;
        }

        public void Remove(int i)
        {
            if(ConnectionRepo.ContainsKey(i))
                ConnectionRepo.Remove(i);
        }

        public ConnectionData Retrieve(int i)
        {
            return ConnectionRepo[i];
        }

        public bool ConnectionExists(int Id) 
        {
            try
            {
                Retrieve(Id);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

