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
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            Console.WriteLine(path);
            Console.ReadLine();

            //using (var Service = GetService()) { \
            //    var Id = Service.ConnectionConnect(9999, 1001, "PDRYWALL", "supercool", "David").Response;
            //    var ret = Service.DeviceSynchronize(Id, 0);
            //    Console.WriteLine("Complete");
            //    Console.ReadLine();
            //}
        }

        static APISoapClient GetService()
        {
            return new APISoapClient("APISoap");
        }
    }
}
