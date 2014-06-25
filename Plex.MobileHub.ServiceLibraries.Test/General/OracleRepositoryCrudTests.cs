using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Plex.MobileHub.ServiceLibraries.Repositories;
using Plex.MobileHub.Data;
using Plex.MobileHub.Data.Types;

namespace Plex.MobileHub.ServiceLibraries.Test.General
{
    [TestClass]
    public class OracleRepositoryCrudTests
    {
        [TestMethod]
        public void CLIENTS_OracleRepoCrudTest()
        {
            CLIENTS value, variable = new CLIENTS();
            using (var repo  = new OracleRepository<CLIENTS>())
            using(var rng = new PlexRandomizer())
            {
                repo.StartTransaction();
                rng.FillProperties(variable);
                
                //This Id should never be taken
                variable.CLIENT_ID = -1;
                //This is the maxmium valid value
                variable.CLIENT_PORT %= 65536;
                
                repo.Insert(variable);
                value = repo.Retrieve( p => p.CLIENT_ID == variable.CLIENT_ID);

                //This tests ensures the insert was correct.
                foreach (var property in typeof(CLIENTS).GetProperties())
                    Assert.AreEqual(property.GetValue(variable), property.GetValue(value));

                rng.FillProperties(variable);
                
                variable.CLIENT_ID = value.CLIENT_ID;
                variable.CLIENT_PORT %= 65536;

                repo.Update(variable);
                value = repo.Retrieve(p => p.CLIENT_ID == variable.CLIENT_ID);

                //This tests ensures the update was correct.
                foreach (var property in typeof(CLIENTS).GetProperties())
                    Assert.AreEqual(property.GetValue(variable), property.GetValue(value));

                repo.Delete(p => p.CLIENT_ID == variable.CLIENT_ID);
                value = repo.Retrieve(p => p.CLIENT_ID == variable.CLIENT_ID);

                //This Tests to ensure that the delete was correct
                Assert.AreEqual(null, value);
            }
        }
    }
}
