using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Compression;
using System.IO;
namespace Plex.MobileHub.Client.Oracle
{
    class Program
    {
        static void Main(string[] args)
        {
            //Todo Implement a way to specify a path to extract data too.
            using (Stream stream = new MemoryStream(Oracle.Resources.Files))
            using (ZipArchive archive = new ZipArchive(stream))
                foreach (ZipArchiveEntry entry in archive.Entries)
                    entry.ExtractToFile(Environment.CurrentDirectory + @"\" + entry.Name);
        }
    }
}
