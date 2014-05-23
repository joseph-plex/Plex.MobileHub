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
    public class TestAPP_QUERIES
    {
        [TestMethod]
        public void CtorAPP_QUERIES()
        {
            APP_QUERIES variable = new APP_QUERIES();
        }

        public void InsertAPP_QUERIES()
        {
            using(PlexxisDataRandomizer rng = new PlexxisDataRandomizer())
            using(var connection = Utilities.GetConnection(true))
            using (var transaction = connection.BeginTransaction())
            {
                APP_QUERIES variable = new APP_QUERIES();
                rng.Fill(variable);

                variable.IS_DELTA = 0;
                variable.Insert(connection);
            }
        }
    }
}
