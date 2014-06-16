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
    public class TestCLIENT_DB_COMPANIES
    {
        [TestMethod]
        public void Ctor()
        {
            CLIENT_DB_COMPANIES variable = new CLIENT_DB_COMPANIES();
        }
    }
}
