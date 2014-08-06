using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plex.Data;
using Plex.MobileHub.ServiceLibrary.Types;

namespace Plex.MobileHub.ServiceLibrary.ApiService
{
    public class ConnectionConnect : MethodStrategyBase<MethodResult>
    {
        public MethodResult Strategy(int clientId, int appId, string database, string user, string password)
        {
            try { 
                //Ensure data inputted is correct.
                var client = GetRepository<CLIENTS>().Retrieve(p => p.CLIENT_ID == clientId);
                var app = GetRepository<APPS>().Retrieve(p => p.APP_ID == appId);
                var dbs = GetRepository<CLIENT_DB_COMPANIES>().RetrieveAll().Where(p => p.CLIENT_ID == clientId && p.COMPANY_CODE == database);
                var person = GetRepository<CLIENT_USERS>().Retrieve(p => p.CLIENT_ID == clientId && p.NAME == user && p.PASSWORD == password);


                if (AnyNull(client, app, dbs, person) || dbs.Count() == 0)
                    throw new Exception("Invalid Authentication, one or more of the values is incorrect");

                if (GetRepository<CLIENT_APPS>().Retrieve(p => p.APP_ID == appId && p.CLIENT_ID == clientId) == null)
                    throw new Exception("Client is not authorized to use application");

                //This can happen by mistake but is generally an indication of someone abusing the system
                var perm = GetRepository<CLIENT_DB_COMPANY_USERS>().RetrieveAll().Where(p => dbs.Any(db => db.DB_COMPANY_ID == p.DB_COMPANY_ID && person.USER_ID == p.USER_ID));
                if (perm.Count() == 0)
                    throw new Exception("User Is not authorized to view this database");

                //This can technically return more than one; however, only one needs to return in order to prove this connection information is valid. 
                if (GetRepository<CLIENT_DB_COMPANY_USER_APPS>().Retrieve(p => perm.Any(a => p.APP_ID == appId && a.DB_COMPANY_USER_ID == p.DB_COMPANY_USER_ID)) == null)
                    throw new Exception("User Database pairing is not authorized to use the application");

                ConsumerInformation consumer = new ConsumerInformation()
                {
                    ConsumerId = GetConsumerId(),
                    AppId = appId,
                    ClientId = clientId,
                    DatabaseCode = database,
                    UserId = person.USER_ID
                };

                GetRepository<ConsumerInformation>().Insert(consumer);
                return new MethodResult().Success(consumer.ConsumerId);
            }
            catch(Exception e)
            {
                return new MethodResult().Failure(e);
            }
        }

        Int32 GetConsumerId()
        {
            int num;
            using(PlexRandomizer pr = new PlexRandomizer())
                do num = pr.GetInt32();
                while (GetRepository<ConsumerInformation>().Exists(p => p.ConsumerId == num));
            return Math.Abs(num);
        }
    }
}
