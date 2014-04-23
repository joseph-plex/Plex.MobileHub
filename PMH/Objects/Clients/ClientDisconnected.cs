using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MobileHub.Data.Tables;
using MobileHub.Repositories;
namespace MobileHub.Objects.Clients
{
    internal class ClientDisconnected : IClientStateBehaviour
    {
        public int ClientId
        {
            get
            {
                return Context.clientId;
            }
            set
            {
                Context.clientId = value;
            }
        }

        public string Key
        {
            get
            {
                return Context.key;
            }
            set
            {
                Context.key = value;
            }
        }

        public int Port
        {
            get
            {
                return Context.port;
            }
            set
            {
                Context.port = value;
            }
        }

        public string IPv4
        {
            get
            {
                return Context.address;
            }
            set
            {
                Context.address = value;
            }
        }
        public Client Context
        {
            get;
            set;
        }

      
        public T ExecuteRequest<T>(Command command)
        {
            throw new InvalidOperationException("Client Is Not Connected");
        }

        public T ExecuteRequest<T>(string commandName, params object[] arguments)
        {
            throw new InvalidOperationException("Client Is Not Connected");
        }

        public void RequestPull()
        {
            throw new InvalidOperationException("Client Is Not Connected");
        }
        public void AdjustState()
        {
            CLIENTS ClientTuple = CLIENTS.GetAll().First(p => p.CLIENT_ID == Context.clientId);
            if (ClientTuple.CLIENT_INSTANCE_ID == null)
                Context.Disconnect();
            else
                Context.Connect(ClientTuple.CLIENT_IP_ADDRESS, ClientTuple.CLIENT_PORT ?? -1);
        }
        public void Connect(string address, int port)
        {
            CLIENTS ClientTuple = CLIENTS.GetAll().First(p => p.CLIENT_ID == Context.clientId);

            //todo change client_instance_id so it is a random number
            ClientTuple.CLIENT_INSTANCE_ID = ClientTuple.CLIENT_ID;
            ClientTuple.CLIENT_IP_ADDRESS = Context.IPv4 = address;
            ClientTuple.CLIENT_PORT = Context.Port = port;


            ClientTuple.Update();

            Logs.Instance.Add("Client " + ClientTuple.CLIENT_ID + " has logged in");

            //Context.connectionTimeout.Enabled = true;

        }

        public void CheckIn()
        {
            Context.connectionTimeout.Stop();
            Context.connectionTimeout.Start();
        }
        public void Disconnect()
        {
            Context.connectionTimeout.Enabled = false;
        }
    }

}