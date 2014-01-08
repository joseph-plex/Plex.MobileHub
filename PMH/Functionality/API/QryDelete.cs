using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Plex.PMH.Repositories;
using Plex.PMH.Data.Tables;
namespace Plex.PMH.Functionality.API
{
    public static partial class Functions
    {
        public static bool QryDelete(int ApplicationId, string ApplicationAuthenticationKey, string QueryName)
        {
            var Applications = new List<APPS>(APPS.GetAll());
            var ApplicationIndex = Applications.FindIndex((p) => p.APP_ID == ApplicationId);

            //todo create an error for this.
            if (ApplicationIndex == -1)
                throw new Exception("This application does not exist");

            //todo create an error for this
            if (Applications[ApplicationIndex].AUTH_KEY != ApplicationAuthenticationKey)
                throw new Exception("Invalid Application Key");

            var Queries = new List<APP_QUERIES>(APP_QUERIES.GetAll());
            var QueryIndex = Queries.FindIndex((p) => Applications[ApplicationIndex].APP_ID == p.APP_ID && p.NAME == QueryName);
            //todo create an error for this
            if (QueryIndex == -1) throw new Exception("No query of this name exists for this application");

            Queries[QueryIndex].Delete();
            return true;
        }
    }
}