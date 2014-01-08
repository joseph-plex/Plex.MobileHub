﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Threading;
using System.Threading.Tasks;
using Plex.PMH.Repositories;
using Plex.PMH.Exceptions;

using Plex.PMH.Data.Tables;

namespace Plex.PMH.Functionality.API
{
    public static partial class Functions
    {
        public static int ConnectionConnect(int ClientId, int AppId, String Database, String User, String Password)
        {
            Consumer Cons = new Consumer()
            {
                ClientId = ClientGet(ClientId).CLIENT_ID,
                AppId = ApplicationGet(AppId).APP_ID,
                DatabaseId = CompanyGet(ClientId, Database).DB_COMPANY_ID,
                UserId = UserGet(ClientId, User, Password).USER_ID
            };

            if (!IsValidClientApp(Cons.ClientId, Cons.AppId))
                throw new UnauthorizedClientException();

            if(!IsValidUserCompanyPermission(ClientDBCompanyUsers(Cons.DatabaseId, Cons.AppId).DB_COMPANY_USER_ID, AppId))
                throw new Exception("User does not have access to the database");

            return Consumers.Instance.Add(Cons);
        }

        static APPS ApplicationGet(int ApplicationId)
        {
            var Apps = new List<APPS>(APPS.GetAll());
            var AppIndex = Apps.FindIndex((p) => p.APP_ID == ApplicationId);
            if (AppIndex == -1) throw new InvalidApplicationException();
            return Apps[AppIndex];
        }

        static CLIENTS ClientGet(int ClientId)
        {
            var Clients = new List<CLIENTS>(CLIENTS.GetAll());
            var ClientIndex = Clients.FindIndex((p)=> p.CLIENT_ID == ClientId);
            if (ClientIndex == -1) throw new InvalidClientException();
            return Clients[ClientIndex];
        }

        static CLIENT_USERS UserGet(int ClientId, string user, string pass)
        {
            var Users = new List<CLIENT_USERS>(CLIENT_USERS.GetAll());
            var UserIndex = Users.FindIndex((p) => p.CLIENT_ID == ClientId && p.NAME == user);
            //todo make valid error for this.
            if (UserIndex == -1) throw new Exception("User does not exist");
            if (Users[UserIndex].PASSWORD != pass) throw new InvalidUserPasswordException();
            return Users[UserIndex];
        }

        static bool IsValidClientApp(int ClientId, int ApplicationId)
        {
            return new List<CLIENT_APPS>(CLIENT_APPS.GetAll()).Exists((p) => p.APP_ID == ApplicationId && p.CLIENT_ID == ClientId);
        }

        static CLIENT_DB_COMPANIES CompanyGet(int ClientId, string CompanyCode)
        {
            var Companies = new List<CLIENT_DB_COMPANIES>(CLIENT_DB_COMPANIES.GetAll());
            var CompanyIndex = Companies.FindIndex((p) => p.CLIENT_ID == ClientId && p.COMPANY_CODE == CompanyCode);
            if (CompanyIndex == -1) throw new Exception("Incorrect Database Name");
            return Companies[CompanyIndex];
        }

        static CLIENT_DB_COMPANY_USERS ClientDBCompanyUsers(int DbCompanyId, int UserId)
        {
            var collection = new List<CLIENT_DB_COMPANY_USERS>(CLIENT_DB_COMPANY_USERS.GetAll());
            var DBIndex = collection.FindIndex((p) => p.DB_COMPANY_ID == DbCompanyId && UserId == p.USER_ID);
            if (DBIndex == -1) throw new Exception("User does not have access to the database");
            return  collection[DBIndex];
        }
        
        static bool IsValidUserCompanyPermission(int DbCompanyUserId, int AppId)
        {
            var AppAccess = new List<CLIENT_DB_COMPANY_USER_APPS>(CLIENT_DB_COMPANY_USER_APPS.GetAll());
            return AppAccess.Exists((p) => p.DB_COMPANY_USER_ID == DbCompanyUserId && p.APP_ID == AppId);
        }
    }
}