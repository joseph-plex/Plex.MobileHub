using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using MobileHub.Repositories;

using MobileHub.Data.Tables;
using MobileHub.Objects;
namespace MobileHub
{
    public delegate void Subscriber(object sender, EventArgs e);
    public class Global : HttpApplication
    {
        public void Application_Start(object sender, EventArgs e)
        {
            var con = Connections.Instance;
            var comm = Commands.Instance;
            var resp = Responses.Instance;
            var logs = Logs.Instance;
            var cons = Consumers.Instance;

            foreach (var client in CLIENTS.GetAll())
            {
                Client data = new Client();
                data.ClientId = client.CLIENT_ID;
                data.Key = client.CLIENT_KEY;
                data.AdjustState();
                con.Add(data);
            }
        }
      
        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
        }

        protected void Session_End(object sender, EventArgs e)
        {
        }
    }
}