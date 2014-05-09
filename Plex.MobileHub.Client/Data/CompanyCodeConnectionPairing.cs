using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileHubClient.Data
{
    public class CompanyCodeConnectionPairing
    {
        public String CompanyCode
        {
            get;
            set;
        }
        public String ConnectionString
        {
            get;
            set;
        }

        public CompanyCodeConnectionPairing()
        {
            CompanyCode = ConnectionString = String.Empty;
        }

        public CompanyCodeConnectionPairing(string companyCode, string connectionString) : this()
        {
            CompanyCode = companyCode;
            ConnectionString = connectionString;
        }
    }
}
