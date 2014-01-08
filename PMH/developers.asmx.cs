using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

using Plex.PMH.Functionality.API;
using Plex.PMH.Repositories;
using Plex.PMH.Exceptions;
using Plex.PMH.Objects;

namespace Plex.PMH
{
    /// <summary>
    /// Summary description for Developers
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Developers : WebService
    {
        [WebMethod]
        public List<TableDefinition> AppGetTableDefinition(int nAppId, string sAppAuthKey)
        {
            return Functions.AppGetTableDefinition(nAppId, sAppAuthKey);
        }

        [WebMethod]
        public List<QueryDefinition> AppGetQueries(int AppId, string AppAuthKey)
        {
            return Functions.AppGetQueries(AppId, AppAuthKey);
        }

        [WebMethod]
        public void ClientGetDatabaseList(int nClientId, int nAppId)
        {
            throw new NotImplementedException();
        }
    }
}
