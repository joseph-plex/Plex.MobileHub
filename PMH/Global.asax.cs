﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Plex.PMH.Repositories;

namespace Plex.PMH
{
    public class Global : HttpApplication
    {
        public void Application_Start(object sender, EventArgs e)
        {
            var con = Connections.Instance;
            var comm = Commands.Instance;
            var resp = Responses.Instance;
            var logs = Logs.GetInstance();
            var cons = Consumers.Instance;
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