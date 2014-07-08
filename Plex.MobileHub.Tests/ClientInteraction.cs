using System;
using System.Threading;
using System.ServiceModel;
using Plex.MobileHub.ServiceConsumer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Plex.MobileHub.ServiceLibraries.APIServiceLibrary;
using Plex.MobileHub.ServiceLibraries.ClientServiceLibrary;
using Plex.MobileHub.ServiceLibraries;
using Plex.MobileHub.Data.Types;
using Plex.MobileHub.ServiceLibraries.Test;

using System.Diagnostics;
namespace Plex.MobileHub.Tests
{
  
    [TestClass]
    public class ClientInteraction
    {
        const string PipeService = "net.pipe://localhost";
        const string ApiPipeServiceName = "ApiPipe";
        const string ClientPipeServiceName = "ClientPipe";

        [TestMethod]
        [Timeout(2000)]
        public void ApiInitTest()
        {
            bool Success = false;
            Api api = new ApiTestService();
            ServiceHost host = new ServiceHost(api,new Uri(PipeService));
            host.AddServiceEndpoint(typeof(IApiService), new NetNamedPipeBinding(), ApiPipeServiceName);
            host.Opened += (s, e) => { 
                host.BeginClose(OnClose, host);
                Success =  true;
            };
            host.BeginOpen(OnOpen, host);

            while (!Success)
                Thread.Yield();

            Assert.IsTrue(Success);
        }

        [TestMethod]
        [Timeout(2000)]
        public void ClientInitTest()
        {
            bool Success = false;
            ClientService client = new ClientTestService();
            ServiceHost host = new ServiceHost(client, new Uri(PipeService));
            
            host.AddServiceEndpoint(typeof(IClientService), new NetNamedPipeBinding(), ClientPipeServiceName);
            host.Opened += (s,e) => { host.BeginClose(OnClose, host); Success = true;};

            host.BeginOpen(OnOpen, host);

            while (!Success)
                Thread.Yield();
            Assert.IsTrue(Success);
        }

        [TestMethod]
        [Timeout(2000)]
        public void ApiClientInitTest()
        {
            bool ApiSuccessSet, ClientSuccessSet = ApiSuccessSet = false;

            Api api = new ApiTestService();
            ClientService client = new ClientTestService();
            ServiceHost apiHost = new ServiceHost(api, new Uri(PipeService));
            ServiceHost clientHost = new ServiceHost(client, new Uri(PipeService));

            //To let us know the services were successfully opened
            apiHost.Opened += (s, e) => ApiSuccessSet = true;
            clientHost.Opened += (s, e) => ClientSuccessSet = true;

            // to clean up the test case later.
            apiHost.Opened += (s, e) => apiHost.BeginClose(OnClose, apiHost);
            clientHost.Opened += (s, e) => clientHost.BeginClose(OnClose, clientHost);

            clientHost.AddServiceEndpoint(typeof(IClientService), new NetNamedPipeBinding(), ClientPipeServiceName);
            apiHost.AddServiceEndpoint(typeof(IApiService), new NetNamedPipeBinding(), ApiPipeServiceName);
            clientHost.BeginOpen(OnOpen, clientHost);
            apiHost.BeginOpen(OnOpen, apiHost);

            //This allows both services to be open for communication.
            while (!ApiSuccessSet || !ClientSuccessSet)
                Thread.Yield();

            Assert.IsTrue(ApiSuccessSet);
            Assert.IsTrue(ClientSuccessSet);

        }

        [TestMethod]
        [Timeout(2000)]
        public void ApiClientTest()
        {
            bool ApiSuccessSet, ClientSuccessSet = ApiSuccessSet = false;
            Api apiService = new ApiTestService();
            var clientService = new ClientTestService();

            ServiceHost clientHost = new ServiceHost(clientService, new Uri(PipeService));
            ServiceHost apiHost = new ServiceHost(apiService, new Uri(PipeService));

            //To let us know the services were successfully opened
            clientHost.Opened += (s, e) => ClientSuccessSet = true;
            apiHost.Opened += (s, e) => ApiSuccessSet = true;
            clientHost.AddServiceEndpoint(typeof(IClientService), new NetNamedPipeBinding(), ClientPipeServiceName);
            apiHost.AddServiceEndpoint(typeof(IApiService), new NetNamedPipeBinding(), ApiPipeServiceName);
            clientHost.BeginOpen(OnOpen, clientHost);
            apiHost.BeginOpen(OnOpen, apiHost);

            //The reason this is here is to make the best out of the async calls.
            RepoFactory factory = new RepoFactory();
            var ClientInfoRepo = apiService.ClientInfo = clientService.ClientInfo = new InMemoryRepository<ClientInformation>();
            //Reuse repositories from a different unit test.
            apiService.ClientInfo = clientService.ClientInfo = new InMemoryRepository<ClientInformation>();
            apiService.Consumers = new InMemoryRepository<Consumer>();

            apiService.CLIENT_APPS = factory.CLIENT_APPS();
            apiService.CLIENT_USERS = factory.CLIENT_USERS();
            apiService.CLIENTS = clientService.CLIENTS = factory.CLIENTS();
            apiService.CLIENT_DB_COMPANIES = factory.CLIENT_DB_COMPANIES();
            apiService.CLIENT_DB_COMPANY_USERS = factory.CLIENT_DB_COMPANY_USERS();
            apiService.CLIENT_DB_COMPANY_USER_APPS = factory.CLIENT_DB_COMPANY_USER_APPS();
            apiService.APPS = factory.APPS();

            //This allows both services to be open for communication.
            while (!ApiSuccessSet || !ClientSuccessSet)
                Thread.Yield();


            EndpointAddress ApiEndpoint = new EndpointAddress(PipeService + @"/" + ApiPipeServiceName);
            EndpointAddress clientEndpoint = new EndpointAddress(PipeService + @"/" + ClientPipeServiceName);

            InstanceContext context = new InstanceContext((IClientCallback)new TestClientCallback());
            var ClientChannelFactory = new DuplexChannelFactory<IClientService>(context, new NetNamedPipeBinding(), clientEndpoint + "hi");
            var ApiChannelFactory = new ChannelFactory<IApiService>(new NetNamedPipeBinding(), ApiEndpoint);
            var ClientChannel = ClientChannelFactory.CreateChannel();
            var ApiChannel = ApiChannelFactory.CreateChannel();


            //This is a normal work flow
            ClientChannel.LogIn(1, "Client1");
            var methodResult = ApiChannel.ConnectionConnect(1, 1, "PDRYWALL", "Joseph", "Morain");

            //Just check to make sure things worked as intended
            Assert.IsTrue(methodResult.Response > 0);

            var result = ApiChannel.QueryDatabase(methodResult.Response, "select * from dual");
            Assert.AreEqual<String>("X",Convert.ToString(result[0, 0]));

            //clientHost.BeginClose(OnClose, clientHost);
            //apiHost.BeginClose(OnClose, apiHost);

            ClientChannelFactory.Close();
            ApiChannelFactory.Close();
            clientHost.Close();
            apiHost.Close();
        }

        void OnOpen(IAsyncResult ar)
        {
            ServiceHost service = (ServiceHost)ar.AsyncState;
            service.EndOpen(ar);
        }
        void OnClose(IAsyncResult ar)
        {
            ServiceHost service = (ServiceHost)ar.AsyncState;
            service.EndClose(ar);
        }
    }
}
