using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Plex.PMH.Objects;
using Plex.PMH.Repositories;
using Plex.PMH.Data.Tables;

namespace Plex.PMH.Functionality.API
{
    public static partial class Functions
    {
        public static QueryResult QueryDatabase(int nConnectionId, String Query)
        {
            try
            {
                if (!Consumers.Instance.Exists(nConnectionId))
                    throw new Exception("The connection you are specifying does not exist");
                var cust = Consumers.Instance.Retrieve(nConnectionId);
                var dbList = CLIENT_DB_COMPANIES.GetAll().ToList();
                var index = dbList.FindIndex((p) => p.DB_COMPANY_ID == cust.DatabaseId);
                return fQuery(cust.ClientId, dbList[index].COMPANY_CODE, Query);
            }
            catch (Exception e)
            {
                Logs.Instance.Add(e);
            }
            return null;
        }

        private static QueryResult fQuery(int ClientId, string Code, string Qry)
        {
            List<object> args = new List<object>();
            args.Add(Code);
            args.Add(Qry);
            int i = Commands.Instance.Add(ClientId, "Query", args);
            Logs.Instance.Add(i);
            return Responses.Instance.GetResponse<QueryResult>(i);
        }
    }
}