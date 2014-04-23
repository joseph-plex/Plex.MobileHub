using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.IO;

using Plex.PMH.Tester.pmh;

namespace Plex.PMH.Tester
{
    class Program
    {
        public const string  EnvironmentVariablePath = "path";
        static void Main(string[] args)
        {
            using (var service = GetService())
            {
                try
                {
                    var mr = service.ConnectionConnect(9999, 1001, "PDRYWALL", "supercool", "David");
                    var connectionId = mr.Response;

                    var DeviceId = service.DeviceRequestId(connectionId).Response;
                    var result = service.DeviceSynchronize(connectionId, 155, 167);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            Console.Read();
        }

        static APISoapClient GetService()
        {
            return new APISoapClient("APISoap");
        }

    }
}
