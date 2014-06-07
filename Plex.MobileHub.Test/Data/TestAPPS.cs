using System;
using Plex.MobileHub.Data;
using Plex.MobileHub.Data.Types;
using Plex.MobileHub.Data.Tables;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Plex.MobileHub.Test.Data
{
    [TestClass]
    public class TestAPPS
    {
        [TestMethod]
        public void CtorAPPS()
        {
            APPS variables = new APPS();
        }

        [TestMethod]
        public void InsertAPPS()
        {
            using (PlexxisDataRandomizer rng = new PlexxisDataRandomizer())
            using (var connection = Utilities.GetConnection(true))
            using (var transaction = connection.BeginTransaction())
            {
                APPS variable = new APPS();
                rng.Fill(variable);
                variable.APP_ID = -1;
                variable.IS_CLIENT_CUSTOM_APP = 0;
                variable.Insert(connection);
            }
        }

        [TestMethod]
        public void DeleteAPPS()
        {
            using (PlexxisDataRandomizer rng = new PlexxisDataRandomizer())
            using (var connection = Utilities.GetConnection(true))
            using (var transaction = connection.BeginTransaction())
            {
                APPS variable = new APPS();
                rng.Fill(variable);
                variable.APP_ID = -1;
                variable.IS_CLIENT_CUSTOM_APP = 0;
                variable.Insert(connection);
                variable.Delete(connection);
            }
        }
    }
}
