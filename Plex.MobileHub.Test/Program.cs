using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Plex.MobileHub.Test.Service;
namespace Plex.MobileHub.Test
{
    public class Program
    {
        static void Main(string[] args)
        {
            foreach (var v in Directory.EnumerateFiles(Environment.CurrentDirectory))
                Console.WriteLine(v);
            Console.ReadLine();
        }

        static APISoapClient GetService()
        {
            return new APISoapClient("APISoap");
        }
    }
}
