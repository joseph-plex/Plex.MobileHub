using MobileHub.Data.Tables;
using MobileHub.Exceptions;
using MobileHub.Objects;
using MobileHub.Repositories;
using System;
using System.Collections.Generic;


namespace MobileHub.Functionality.API
{
    public class ConnectionConnect : FunctionStrategyBase<MethodResult>
    {
        /// <summary>
        /// Distributes connection Id's based on the validation of different inputs. If all input is valid it will return a connection Id. Otherwise it will throw an exception.
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="appId"></param>
        /// <param name="database"></param>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public MethodResult Strategy(int clientId, int appId, string database, string user, string password)
        {
            var mr = new MethodResult();
            try
            {
                Consumer Cons = new Consumer()
                {
                    ClientId = ClientGet(clientId).CLIENT_ID,
                    AppId = ApplicationGet(appId).APP_ID,
                    DatabaseId = CompanyGet(clientId, database).DB_COMPANY_ID,
                    UserId = UserGet(clientId, user, password).USER_ID
                };

                if (!IsValidClientApp(Cons.ClientId, Cons.AppId))
                    throw new UnauthorizedClientException();

                if (!IsValidUserCompanyPermission(ClientDBCompanyUsers(Cons.DatabaseId, Cons.UserId).DB_COMPANY_USER_ID, appId))
                    throw new Exception("User does not have access to the database");

                mr.Success(Consumers.Instance.Add(Cons));
            }
            catch(Exception e)
            {
                mr.Failure(e);
            }
            return mr;
        }

        /// <summary>
        /// Returns an APPS tuple based no the ApplicationId
        /// </summary>
        /// <param name="ApplicationId"></param>
        /// <returns></returns>
        static APPS ApplicationGet(int ApplicationId)
        {
            var Apps = new List<APPS>(APPS.GetAll());
            var AppIndex = Apps.FindIndex((p) => p.APP_ID == ApplicationId);
            if (AppIndex == -1) throw new InvalidApplicationException();
            return Apps[AppIndex];
        }

        /// <summary>
        /// Retrieves a CLIENTS tuple based on the ClientId.
        /// </summary>
        /// <param name="ClientId"></param>
        /// <returns></returns>
        static CLIENTS ClientGet(int ClientId)
        {
            var Clients = new List<CLIENTS>(CLIENTS.GetAll());
            var ClientIndex = Clients.FindIndex((p) => p.CLIENT_ID == ClientId);
            if (ClientIndex == -1) throw new InvalidClientException();
            return Clients[ClientIndex];
        }

        /// <summary>
        /// Determines if a specific user and password are associated with a particular ClientId.
        /// If yes, returns the specific CLIENT_USER tuple.
        /// </summary>
        /// <param name="ClientId"></param>
        /// <param name="user"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        static CLIENT_USERS UserGet(int ClientId, string user, string pass)
        {
            var Users = new List<CLIENT_USERS>(CLIENT_USERS.GetAll());
            var UserIndex = Users.FindIndex((p) => p.CLIENT_ID == ClientId && p.NAME == user);
            if (UserIndex == -1) throw new Exception("User does not exist");
            if (Users[UserIndex].PASSWORD != pass) throw new InvalidUserPasswordException();
            return Users[UserIndex];
        }
        /// <summary>
        /// Determines if a Client has access to a specific application.
        /// </summary>
        /// <param name="ClientId"></param>
        /// <param name="ApplicationId"></param>
        /// <returns></returns>
        static bool IsValidClientApp(int ClientId, int ApplicationId)
        {
            return new List<CLIENT_APPS>(CLIENT_APPS.GetAll()).Exists((p) => p.APP_ID == ApplicationId && p.CLIENT_ID == ClientId);
        }
        /// <summary>
        /// Returns a CLIENT_DB_COMPANIES tuple given the ClientId and the Company Code.
        /// </summary>
        /// <param name="ClientId"></param>
        /// <param name="CompanyCode"></param>
        /// <returns></returns>
        static CLIENT_DB_COMPANIES CompanyGet(int ClientId, string CompanyCode)
        {
            var Companies = new List<CLIENT_DB_COMPANIES>(CLIENT_DB_COMPANIES.GetAll());
            var CompanyIndex = Companies.FindIndex((p) => p.CLIENT_ID == ClientId && p.COMPANY_CODE == CompanyCode);
            if (CompanyIndex == -1) throw new Exception("Incorrect Database Name");
            return Companies[CompanyIndex];
        }
        /// <summary>
        /// Determines whether or a particular user is registered to a database
        /// </summary>
        /// <param name="DbCompanyId"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        static CLIENT_DB_COMPANY_USERS ClientDBCompanyUsers(int DbCompanyId, int UserId)
        {
            var collection = new List<CLIENT_DB_COMPANY_USERS>(CLIENT_DB_COMPANY_USERS.GetAll());
            var DBIndex = collection.FindIndex((p) => p.DB_COMPANY_ID == DbCompanyId && UserId == p.USER_ID);
            if (DBIndex == -1) throw new Exception("User does not have access to the database");
            return collection[DBIndex];
        }

        /// <summary>
        /// Determined whether or not there existed a combination of DbCompanyUser and application Id.
        /// </summary>
        /// <param name="DbCompanyUserId"></param>
        /// <param name="AppId"></param>
        /// <returns></returns>
        static bool IsValidUserCompanyPermission(int DbCompanyUserId, int AppId)
        {
            var AppAccess = new List<CLIENT_DB_COMPANY_USER_APPS>(CLIENT_DB_COMPANY_USER_APPS.GetAll());
            return AppAccess.Exists((p) => p.DB_COMPANY_USER_ID == DbCompanyUserId && p.APP_ID == AppId);
        }
    }
}