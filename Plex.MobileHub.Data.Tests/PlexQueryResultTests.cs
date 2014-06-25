using System;
using System.Data;
using Oracle.DataAccess.Client;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Plex.MobileHub.Data.Tests
{
    [TestClass]
    public class PlexQueryResultTests
    {
        const string User = "C##PMH";
        const string Pass = "!!!plex!!!sa";
        const string Source = "(DESCRIPTION = (ADDRESS_LIST = (ADDRESS = (PROTOCOL = TCP)(HOST = 10.0.1.96)(PORT = 1521)))(CONNECT_DATA = (SERVICE_NAME = PLE1LIVE)))";
        const string ConnectionString = "User Id=" + User + ";" + "Password=" + Pass + ";" + "Data Source=" + Source + ";";

        [TestMethod]
        public void PlexQueryResultSerialize()
        {
            using (Stream stream = new MemoryStream())
            using (IDbConnection connection = new OracleConnection(ConnectionString).OpenConnection())
                new XmlSerializer(typeof(PlexQueryResult)).Serialize(stream, connection.Query("select * from dual"));
        }

        [TestMethod]
        public void PlexQueryResultCtor()
        {
            using (Stream stream = new MemoryStream())
            using (IDbConnection connection = new OracleConnection(ConnectionString).OpenConnection())
            {
                var variable = connection.Query("select * from dual");
                new XmlSerializer(typeof(PlexQueryResult)).Serialize(stream, variable);
                stream.Position = 0;
                var newvar = new XmlSerializer(typeof(PlexQueryResult)).Deserialize(stream) as PlexQueryResult;

            }
        }
    }
}
