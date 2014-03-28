using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MobileHub.Data.Tables;

namespace MobileHub.Objects
{
    public class Consumer
    {
        public int ConsumerId;
        public int ClientId;
        public int DatabaseId;
        public int AppId;
        public int UserId;

        public CLIENT_DB_COMPANIES ClientDbCompaniesGet()
        {
            var Db = CLIENT_DB_COMPANIES.GetAll().First(p => p.DB_COMPANY_ID == DatabaseId);
            if (Db == null) throw new Exception("Cannot find specified database");
            return Db;
        }

        public IEnumerable<APP_QUERIES> GetAccessibleQueries()
        {
            return APP_QUERIES.GetAll().Where(p => p.APP_ID == AppId);
        }

    }
}