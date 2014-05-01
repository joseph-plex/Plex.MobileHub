using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Plex.MobileHub.Test.Service;
namespace Plex.MobileHub.Test
{
    public class Program
    {
        static void Main(string[] args)
        {
            using (var Service = GetService()) { 
                var Id = Service.ConnectionConnect(9999, 1001, "PDRYWALL", "supercool", "David").Response;
                var ret = Service.DeviceSynchronizeAlt(Id,null);
                Console.WriteLine("Complete");
                Console.ReadLine();
            }
        }

        static APISoapClient GetService()
        {
            return new APISoapClient("APISoap");
        }
    }
}
