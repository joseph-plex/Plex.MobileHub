using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plex.MobileHub.ServiceLibraries.ClientServiceLibrary;
using Plex.MobileHub.ServiceLibraries;
using Plex.MobileHub.Data;
namespace Plex.MobileHub.Tests
{
    public class TestClientCallback : IClientCallback
    {
        public void IUD()
        {
            throw new NotImplementedException();
        }

        public QryResult Query(string connectionString, string query, params object[] arguments)
        {
            QryResult result = new QryResult{StartTimeStamp = DateTime.Now};
            result.Success();
            result.DBErrorCode = 0;
            result.DBErrorMessage = null;

            result.Tuples = new List<PlexQueryResultTuple>(){
                
                new PlexQueryResultTuple
                {
                    Values = new List<object>() { "X"}
                }
            };
            result.Columns = new List<PlexQueryResultColumn>()
            {
                new PlexQueryResultColumn
                {
                    ColumnName = "DUMMY",
                    ColumnSequence = null,
                    DataType = "System.String",
                    DataLength = null,
                    DataPrecision = null,
                    DataScale = null,
                    AllowDbNull = null,
                    IsReadOnly = false,
                    IsLong = false,
                    IsKey = false,
                    IsUnique = false,
                    Description = null,
                    KeyType = null
                }
            };

            return result;
        }

        public ServiceLibraries.RegisteredQueryResult ExecuteRegisteredQuery(string connetionString, string queryName, DateTime? time = null)
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
