using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MobileHub.Data.Tables;
using System.ServiceModel;
using MobileHub.Repositories;
namespace MobileHub.Objects.Clients
{
    sealed class ClientConnected : IClientStateBehaviour
    {
        public int ClientId
        {
            get
            {
                return Context.clientId;
            }
            set
            {
                throw new InvalidOperationException("Cannot Change this value while client is connected");
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
                throw new InvalidOperationException("Cannot Change this value while client is connected");
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
                throw new InvalidOperationException("Cannot Change this value while the client is connected");
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
                throw new InvalidOperationException("Cannot change this value while the client is connected");
            }
        }
        public Client Context
        {
            get;
            set;
        }

        public void AdjustState()
        {
            CLIENTS ClientTuple = CLIENTS.GetAll().First(p => p.CLIENT_ID == Context.clientId);
            if (ClientTuple.CLIENT_INSTANCE_ID == null)
                Context.Disconnect();
            else
                Context.Connect(ClientTuple.CLIENT_IP_ADDRESS, ClientTuple.CLIENT_PORT ?? -1);
        }


        public T ExecuteRequest<T>(string commandName, params object[] arguments)
        {
            Command command = new Command()  {
                ClientId = Context.ClientId,
                Name = commandName,
                Params = arguments.ToList()
            };

            int commandId = Commands.Instance.Add(command);
            RequestPull();
            return Responses.Instance.GetResponse<T>(commandId);
        }

        public void RequestPull()
        {
            try
            {
                CreateChannel().RetrieveCommands();
            }
            catch (EndpointNotFoundException)
            {
                throw new Exception("Client cannot be contacted at the specified address");
            }
        }


        public void Connect(string address, int port)
        {
            throw new InvalidOperationException("Client is already connected");
        }

        public void Disconnect()
        {
            Context.address = null;
            Context.port = 0;

            CLIENTS ClientTuple = CLIENTS.GetAll().First(p => p.CLIENT_ID == ClientId);
            ClientTuple.CLIENT_INSTANCE_ID = null;
            ClientTuple.CLIENT_IP_ADDRESS = null;
            ClientTuple.CLIENT_PORT = null;

            ClientTuple.Update();
        }

        EndpointAddress CreateEndpointAddress()
        {
            string EndpointString = @"http://" + @Context.IPv4 + ":" + @Context.Port + @"/External/webs";
            return new EndpointAddress(EndpointString);
        }

        WSHttpBinding CreateWSHttpBinding()
        {
            return new WSHttpBinding()
            {
                Security = new WSHttpSecurity()
                {
                    Mode = SecurityMode.None
                }
            };
        }
        ExternalService CreateChannel()
        {

            var endpoint = CreateEndpointAddress();
            var binding = CreateWSHttpBinding();
            var myChannelFactory = new ChannelFactory<ExternalService>(binding, endpoint);
            return myChannelFactory.CreateChannel();
        }
    }
}