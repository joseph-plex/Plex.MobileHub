using System;
using Plex.MobileHub.Data.Types;
using Plex.MobileHub.ServiceLibraries.Repositories;
using Plex.MobileHub.ServiceLibraries.APIServiceLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Plex.MobileHub.ServiceLibraries.Test.ApiService
{
    [TestClass]
    public class ConnectionConnectTests
    {
        const int ValidClientId = 1;
        const int ValidAppId = 1;
        const string ValidDb = "PDRYWALL";
        const string ValidUser = "Joseph";
        const string ValidPassword = "Morain";


        
        [TestMethod]
        [Description("Just a simple test that should always end in success")]
        public void SimpleTest()
        {
            RepoFactory factory = new RepoFactory();
            ConnectionConnect cc = new ConnectionConnect();

            cc.appsRepository = factory.APPS();
            cc.clientAppsRepository = factory.CLIENT_APPS();
            cc.clientDbCompaniesRepository = factory.CLIENT_DB_COMPANIES();
            cc.clientDbCompanyUserAppsRepository = factory.CLIENT_DB_COMPANY_USER_APPS();
            cc.clientDbCompanyUsersRepository = factory.CLIENT_DB_COMPANY_USERS();
            cc.clientRepository = factory.CLIENTS();
            cc.clientUsersRepository = factory.CLIENT_USERS();

            Assert.AreEqual(true, cc.Strategy(ValidClientId, ValidAppId, ValidDb, ValidUser, ValidPassword));
        }
    }
}
