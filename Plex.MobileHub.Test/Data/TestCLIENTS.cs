using System;
using System.Data;
using System.Linq;
using Plex.MobileHub.Data;
using Plex.MobileHub.Data.Types;
using Plex.MobileHub.Data.Tables;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Plex.MobileHub.Test.Data
{
    [TestClass]
    public class TestCLIENTS
    {
        [TestMethod]
        public void CtorCLIENTS()
        {
            CLIENTS variable = new CLIENTS();
        }

        [TestMethod]
        [Timeout(100)]
        public void InsertCLIENTS()
        {
            using (PlexxisDataRandomizer rng = new PlexxisDataRandomizer())
            using (var connection = Utilities.GetConnection(true))
            using (var transaction = connection.BeginTransaction())
            {
                CLIENTS variable = new CLIENTS();

                rng.Fill(variable);

                variable.CLIENT_ID = -1;
                variable.CLIENT_PORT = 65535;

                variable.Insert(connection);

                var dbvar = CLIENTS.GetAll(connection).First(p => p.CLIENT_ID == variable.CLIENT_ID);

                Assert.AreEqual<int>(variable.CLIENT_ID, dbvar.CLIENT_ID);
            }
        }
    }
}
