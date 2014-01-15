using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
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
                    var mr = service.ConnectionConnect(9999, 1001, "KERR", "supercool", "David");
                    if (mr.Response < 0) throw new Exception(mr.Msg);
                    var ConnectionId = mr.Response;

                    var resp =  service.QryExecute(ConnectionId, "QRY_JOBS", null);
                    return;
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

        static IEnumerable<String> DirSearch(String sDir, String FileName)
        {
            string[] directories = null;
            string[] files = null;

            //Check to make sure that Directory is okay
            try { directories = Directory.GetDirectories(sDir); }
            catch { yield break; }

            foreach (string d in Directory.GetDirectories(sDir))
            {
                try { files = Directory.GetFiles(d, FileName); }
                catch { continue; }

                foreach(string f in files)
                    yield return f;
                foreach (var r in DirSearch(d, FileName))
                    yield return r;
            }
        }

        public static IEnumerable<String> SearchPath(String FileName)
        {
            List<String> collection = new List<string>();
            var Directories = Environment.GetEnvironmentVariable(EnvironmentVariablePath).Split(new char[] { ';' },StringSplitOptions.RemoveEmptyEntries);
            Parallel.ForEach(Directories, (drive) =>{ foreach (var Directory in DirSearch(drive + @"\", FileName))collection.Add(Directory); });
            return collection;
        }
    }
}
