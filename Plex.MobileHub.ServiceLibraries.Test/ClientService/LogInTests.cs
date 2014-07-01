using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Plex.MobileHub.ServiceLibraries.ClientServiceLibrary;

namespace Plex.MobileHub.ServiceLibraries.Test.ClientService
{
    [TestClass]
    public class LogInTests
    {
        [TestMethod]
        public void SimpleTest1()
        {
            RepoFactory factory = new RepoFactory();
            LogIn login = new LogIn();
            login.ClientsRepository = factory.CLIENTS();

            Assert.IsTrue(login.Strategy(1, "Client1"));
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        [Description("Test with inalid Client Key and invalid Client Id")]
        public void SimpleTestFail1()
        {

            RepoFactory factory = new RepoFactory();
            LogIn login = new LogIn();
            login.ClientsRepository = factory.CLIENTS();

            Assert.IsTrue(login.Strategy(-1, String.Empty));
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        [Description("Test with Valid Client Key but invalid Client Id")]
        public void SimpleTestFail2()
        {

            RepoFactory factory = new RepoFactory();
            LogIn login = new LogIn();
            login.ClientsRepository = factory.CLIENTS();

            Assert.IsTrue(login.Strategy(-1, "Client1"));
        }
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        [Description("Test with inValid Client Key but valid Client Id")]
        public void SimpleTestFail3()
        {

            RepoFactory factory = new RepoFactory();
            LogIn login = new LogIn();
            login.ClientsRepository = factory.CLIENTS();

            Assert.IsTrue(login.Strategy(1, String.Empty));
        }
    }
}
