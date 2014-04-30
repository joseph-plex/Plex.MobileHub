using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Plex.MobileHub.Data.Tables;
using System.ServiceModel;
using Plex.MobileHub.Repositories;
using Plex.MobileHub.Exceptions;
namespace Plex.MobileHub.Objects.Clients
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
            int commandId = Commands.Instance.Add(ClientId, commandName, arguments.ToList());
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
                Context.Disconnect();
                throw;
                //throw new Exception("Client cannot be contacted at the specified address. Please contact Client Administrator");
            }
            catch(TimeoutException)
            {
                Context.Disconnect();
                throw;
                //throw new Exception("The Client is taking too long to respond to the the request. Please contact Client Administrator");
            }    
        }


        public void Connect(string address, int port)
        {
            CLIENTS ClientTuple = CLIENTS.GetAll().First(p => p.CLIENT_ID == Context.clientId);

            
            if (ClientTuple.CLIENT_IP_ADDRESS != address)
                throw new MultipleClientLoginException();
            if (ClientTuple.CLIENT_PORT != port)         
                throw new MultipleClientLoginException();

            Logs.Instance.Add("Client " + ClientTuple.CLIENT_ID + " has checked in");
            CheckIn();
        }

        public void CheckIn()
        {
            Context.connectionTimeout.Stop();
            Context.connectionTimeout.Start();
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
            Context.connectionTimeout.Enabled = false;
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