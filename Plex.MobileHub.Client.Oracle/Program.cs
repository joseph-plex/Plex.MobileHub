using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Compression;
using System.IO;
using System.Security.Principal;
namespace Plex.MobileHub.Client.Oracle
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = args.Last().Trim();
            using (Stream stream = new MemoryStream(Oracle.Resources.Files))
            using (ZipArchive archive = new ZipArchive(stream))
                foreach (ZipArchiveEntry entry in archive.Entries)
                    if (!File.Exists(path + entry.Name))
                        entry.ExtractToFile(path + entry.Name);
        }
    }
}
