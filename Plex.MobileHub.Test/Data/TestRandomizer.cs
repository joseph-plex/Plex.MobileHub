using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Plex.MobileHub.Data;
using Plex.MobileHub.Data.Tables;
namespace Plex.MobileHub.Test.Data
{
    [TestClass]
    public class TestRandomizer
    {
        [TestMethod]
        public void RNGCtor()
        {
            PlexxisDataRandomizer rng = new PlexxisDataRandomizer();
        }

        [TestMethod]
        public void RNGGetInt32_1()
        {
            PlexxisDataRandomizer rng = new PlexxisDataRandomizer();
            var value = rng.GetInt32();
        }

        [TestMethod]
        public void RNGGetString_1()
        {
            PlexxisDataRandomizer rng = new PlexxisDataRandomizer();
            var value = rng.GetString();
        }
        [TestMethod]
        public void RNGGetDateTime()
        {
            PlexxisDataRandomizer rng = new PlexxisDataRandomizer();
            var value = rng.GetDateTime();
        }

        [TestMethod]
        public void FillProperties()
        {
            PlexxisDataRandomizer rng = new PlexxisDataRandomizer();
            APPS app = new APPS();
            rng.Fill(app);

            app.IS_CLIENT_CUSTOM_APP = 0;
            app.APP_ID = -1;

            app.Insert();

        }
    }
}
