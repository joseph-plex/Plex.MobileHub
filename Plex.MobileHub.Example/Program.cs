using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plex.MobileHub.Example.Hub;
namespace Plex.MobileHub.Example
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var service = new APISoapClient("APISoap"))
            {
                var MData = new List<IUDData>();
                IUDData data = new IUDData();
                List<Row> Rows = new List<Row>();
                var mr = service.ConnectionConnect(9999, 1001, "PDRYWALL", "supercool", "David");
                
                data.TableName = "COMPANY";
                data.ColumnNames = (new string[] { "CODE", "CITY" });
                Rows.Add(new Row() { Values = new string[] { "PDRYWALL", "Toronto" }, DBAction = 2 });

                data.Rows = Rows.ToArray();
                MData.Add(data);

                mr = service.IUD(mr.Response, MData.ToArray());
            }
        }
    }
}
