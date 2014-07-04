using System;
using System.Linq;
using System.Collections.Generic;
using Plex.MobileHub.Objects;
using Plex.MobileHub.Functionality.API;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Plex.MobileHub.Test.Api
{
    [TestClass]
    public class IUDTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            
            IUD Iud = new IUD();
            IUDData data = new IUDData();
            MethodResult mr = new ConnectionConnect().Strategy(9999, 1001, "PDRYWALL", "supercool", "David");
            
            if (mr.Response < 0)
                throw new Exception("Method Result did not return valid Id");

            data.TableName = "COMPANY";
            data.ColumnNames = (new string[] { "CODE" ,"CITY" }).ToList();
            data.Rows = new List<Row>();
            Row r1 = new Row();
            r1.Values.Add("COMPANY");
            r1.Values.Add("Toronto");
            data.Rows.Add(r1);
            r1.DBAction = 2;

            mr = Iud.Strategy(mr.Response, new IUDData[]{ data });
        }
    }
}
