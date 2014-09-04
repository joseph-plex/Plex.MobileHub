using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
namespace Plex.MobileHub.Client.Oracle.U
{
    class Program
    {
        static readonly string[] FileNames = 
        {
            "oci.dll",
            "ocijdbc12.dll",
            "ociw32.dll",
            "Oracle.DataAccess.dll",
            "orannzsbb12.dll",
            "oraocci12.dll",
            "oraocci12d.dll",
            "oraociei12.dll",
            "oraons.dll",
            "OraOps12.dll"
        };

        static void Main(string[] args)
        {
            var path = args.Last().Trim();
            foreach (var v in FileNames)
                if(File.Exists(@path + v))
                    File.Delete(@path + v);
        }
    }
}
