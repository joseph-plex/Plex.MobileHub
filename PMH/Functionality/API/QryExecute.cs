using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

using Plex.PMH.Repositories;
using Plex.PMH.Exceptions;
using Plex.PMH.Data.Tables;
namespace Plex.PMH.Functionality.API
{
    public static partial class Functions
    {
        public static XmlDocument QryExecute(int ConnectionId, string QueryName, DateTime? Time = null)
        {
            try
            {
                if (!Consumers.Instance.Exists(ConnectionId)) throw new InvalidConsumerException();
                var cust = Consumers.Instance.Retrieve(ConnectionId);


                var dbList = new List<CLIENT_DB_COMPANIES>(CLIENT_DB_COMPANIES.GetAll());
                //var dbList = db.CLIENT_DB_COMPANIES.ToList();// Access.ClientDBCompanies.GetAll();
                
                var index = dbList.FindIndex((p) => p.DB_COMPANY_ID == cust.DatabaseId);
                //todo cannot find appropriate error for this
                if (index == -1) throw new Exception("Cannot find specified database");

                var Queries = new List<APP_QUERIES>(APP_QUERIES.GetAll());
                var QueryIndex = Queries.FindIndex((p) => p.NAME == QueryName && p.APP_ID == Consumers.Instance.Retrieve(ConnectionId).AppId);
                //todo create an appropriate error for this.
                if (QueryIndex == -1) throw new InvalidQueryException();

                Logs.GetInstance().Add(Queries[QueryIndex].QUERY_ID);
                Logs.GetInstance().Add(Queries[QueryIndex].NAME);

                return fQuery(cust.ClientId, dbList[index].COMPANY_CODE, Queries[QueryIndex].QUERY_ID, Time);
            }
            catch (Exception e)
            {
                Logs.GetInstance().Add(e);
            }
            return null;
        }

        static XmlDocument fQuery(int ClientId, string Code, int Qry, DateTime? Time = null)
        {
            List<object> args = new List<object>();
            args.Add(Code);
            args.Add(Qry);
            int i = new int();
            if (Time != null)
            {
                args.Add(Time);
                i = Commands.Instance.Add(ClientId, "ExecuteRegisteredQueryWithDate", args);
            }
            else
                i = Commands.Instance.Add(ClientId, "ExecuteRegisteredQueryWithoutDate", args);

            Logs.GetInstance().Add(i);
            return Responses.Instance.GetResponse(i);
        }
    }
}