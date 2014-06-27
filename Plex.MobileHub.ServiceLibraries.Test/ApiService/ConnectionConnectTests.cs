using System;
using Plex.MobileHub.Data.Types;
using Plex.MobileHub.ServiceLibraries;
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

            cc.ConsumerRepository = new InMemoryRepository<Consumer>(); 
            cc.appsRepository = factory.APPS();
            cc.clientAppsRepository = factory.CLIENT_APPS();
            cc.clientDbCompaniesRepository = factory.CLIENT_DB_COMPANIES();
            cc.clientDbCompanyUserAppsRepository = factory.CLIENT_DB_COMPANY_USER_APPS();
            cc.clientDbCompanyUsersRepository = factory.CLIENT_DB_COMPANY_USERS();
            cc.clientRepository = factory.CLIENTS();
            cc.clientUsersRepository = factory.CLIENT_USERS();

            Assert.IsTrue(cc.Strategy(1, 1, "PDRYWALL", "Joseph", "Morain").Response > 0);
        }

        [TestMethod]
        [Description("The Client Id specified does not exist")]
        //[ExpectedException(typeof(Exception))]
        public void SimpleTestClientFailure()
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

            Assert.IsTrue(cc.Strategy(0, ValidAppId, ValidDb, ValidUser, ValidPassword).Response< 0);

        }

        [TestMethod]
        [Description("he Applciation Id does not exist")]
        //[ExpectedException(typeof(Exception))]
        public void SimpleTestAppFailure()
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

            Assert.IsTrue(cc.Strategy(ValidClientId, 0, ValidDb, ValidUser, ValidPassword).Response < 0);

            //Assert.AreEqual(true, cc.Strategy(ValidClientId, 0, ValidDb, ValidUser, ValidPassword));
        }

        [TestMethod]
        [Description("The Database Code does not exist.")]
        //[ExpectedException(typeof(Exception))]
        public void SimpleTestDbFailure()
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

            Assert.IsTrue(cc.Strategy(ValidClientId, ValidAppId, String.Empty, ValidUser, ValidPassword).Response < 0);
        }

        [TestMethod]
        [Description("Username does not exist")]
        //[ExpectedException(typeof(Exception))]
        public void SimpleTestUserFailure()
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

            Assert.IsTrue(cc.Strategy(ValidClientId, ValidAppId, ValidDb, String.Empty, ValidPassword).Response < 0);
        }

        [TestMethod]
        [Description("Username does exist but password is incorrect")]
        //[ExpectedException(typeof(Exception))]
        public void SimpleTestPasswordFailure()
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

            Assert.AreEqual(true,cc.Strategy(ValidClientId, ValidAppId, ValidDb, ValidUser, String.Empty).Response < 0);
        }

        [TestMethod]
        [Description("The database is not associated with the client")]
        //[ExpectedException(typeof(Exception))]
        public void SimpleTestDbClientRelationshipFailure()
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

            Assert.AreEqual(true, cc.Strategy(2, ValidAppId, ValidDb, ValidUser, ValidPassword).Response < 0);
        }

        [TestMethod]
        [Description("The client is not associated with the app")]
        //[ExpectedException(typeof(Exception))]
        public void SimpleTestAppClientRelationshipFailure()
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

            Assert.AreEqual(true, cc.Strategy(ValidClientId, 2, ValidDb, ValidUser, ValidPassword).Response < 0);
        }

        [TestMethod]
        [Description("User is not associated with the client")]
        //[ExpectedException(typeof(Exception))]
        public void SimpleTestUserClientRelationshipFailure()
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

            Assert.AreEqual(true, cc.Strategy(ValidClientId, 1, ValidDb, "Marci" ,ValidPassword).Response < 0);
        }

        [TestMethod]
        [Description("User is not associated with a database associated with the client.")]
        //[ExpectedException(typeof(Exception))]
        public void SimpleTestUserDbRelationshipFailure()
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

            Assert.AreEqual(true, cc.Strategy(ValidClientId, 1, ValidDb, "Nash", "Bacon").Response < 0);
        }


        [TestMethod]
        [Description("User database permission pairing does not associated with the application")]
        //[ExpectedException(typeof(Exception))]
        public void SimpleTestUserDbPermssionAppRelationshipFailure()
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

            Assert.AreEqual(true, cc.Strategy(ValidClientId, 2, ValidDb, "Joseph" , "Morain").Response <0 );
        }
    }
}
