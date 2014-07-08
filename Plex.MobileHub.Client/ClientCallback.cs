using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.Data.Common;
using System.Data;
using Oracle.DataAccess.Client;
using Plex.MobileHub.Data;
using Plex.MobileHub.ServiceConsumer;
using Plex.MobileHub.ServiceLibraries;
using Plex.MobileHub.ServiceLibraries.ClientServiceLibrary;

namespace Plex.MobileHub.Client
{
    public class ClientCallback : IClientCallback
    {
        public void IUD()
        {
            throw new NotImplementedException();
        }

        public QryResult Query(string connectionString, string query, params object[] arguments)
        {
            try 
            {
                using(IDbConnection connection = new OracleConnection(connectionString).OpenConnection())
                {
                    QryResult result = new QryResult { StartTimeStamp = DateTime.Now };
                    var plexResult = connection.Query(query, arguments);
                    result.CompetelionTimeStamp = DateTime.Now;
                    result.Columns = plexResult.Columns;
                    result.Tuples = plexResult.Tuples;
                    result.Success();
                    return result;
                }
            }
            catch(DbException e)
            {
                QryResult result = new QryResult {  DBErrorMessage = e.ToString(), DBErrorCode = e.ErrorCode};
                result.Failure(e);
                return result;
            }
            catch(Exception e)
            {
                QryResult result = new QryResult();
                result.Failure(e);
                return result;
            }
        }

        public RegisteredQueryResult ExecuteRegisteredQuery(string connetionString, string queryName, DateTime? time = null)
        {
            throw new NotImplementedException();
        }

        public void Synchronize()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
