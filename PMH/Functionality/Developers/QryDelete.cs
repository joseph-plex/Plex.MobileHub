using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Plex.MobileHub.Repositories;
using Plex.MobileHub.Data.Tables;
using Plex.MobileHub.Exceptions;
namespace Plex.MobileHub.Functionality.Developers
{
    public static partial class Functions
    {
        public static bool QryDelete(int ApplicationId, string ApplicationAuthenticationKey, string QueryName)
        {
            var Applications = new List<APPS>(APPS.GetAll());
            var ApplicationIndex = Applications.FindIndex((p) => p.APP_ID == ApplicationId);

            if (ApplicationIndex == -1)
                throw new InvalidApplicationException();

            if (Applications[ApplicationIndex].AUTH_KEY != ApplicationAuthenticationKey)
                throw new InvalidApplicationKeyException();

            var Queries = new List<APP_QUERIES>(APP_QUERIES.GetAll());
            var QueryIndex = Queries.FindIndex((p) => Applications[ApplicationIndex].APP_ID == p.APP_ID && p.NAME == QueryName);
            if (QueryIndex == -1)
                throw new InvalidQueryException();
            
            Queries[QueryIndex].Delete();
            return true;
        }
    }
}