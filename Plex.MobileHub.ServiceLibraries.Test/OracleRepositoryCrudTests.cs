using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Plex.MobileHub.ServiceLibraries.Repositories;
using Plex.MobileHub.Data;
using Plex.MobileHub.Data.Types;

namespace Plex.MobileHub.ServiceLibraries.Test
{
    [TestClass]
    public class OracleRepositoryCrudTests
    {
        [TestMethod]
        public void APP_QUERIES_OracleRepoCrudTest()
        {
            APP_QUERIES variable = new APP_QUERIES();
            OracleRepository<APP_QUERIES> repo = new OracleRepository<APP_QUERIES>();
        }
    }
}
