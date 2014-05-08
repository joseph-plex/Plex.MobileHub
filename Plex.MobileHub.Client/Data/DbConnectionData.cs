using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Oracle.DataAccess.Client;
namespace MobileHubClient.Data
{
    public class DbConnectionData
    {
        public String Company
        {
            get;
            set;
        }

        public List<String> ConnectionStrings
        {
            get;
            set;
        }

        public DbConnectionData()
        {
            Company = String.Empty;
            ConnectionStrings = new List<String>();
        }

        public IDbConnection GetOpenConnection()
        {
            if (ConnectionStrings.Count == 0)
                throw new InvalidOperationException("There are no Connection Strings available for this company");

            foreach (var text in ConnectionStrings)
            {
                try
                {
                    OracleConnection Connection = new OracleConnection(text);
                    Connection.Open();
                    Connection.Query("select * from dual");
                    return Connection;
                }
                catch
                {
                    continue;
                }
            }
            throw new Exception("None of the connection strings are valid");
        }
    }
}
