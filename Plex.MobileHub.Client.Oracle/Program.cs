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
            using (var writer = new StreamWriter(new FileStream(@"C:\Users\joseph\Desktop\text.txt", FileMode.Create)))
            using (Stream stream = new MemoryStream(Oracle.Resources.Files))
            using (ZipArchive archive = new ZipArchive(stream))
            {
                try
                {
                    writer.WriteLine(Environment.CurrentDirectory);
                    foreach (var s in args)
                        writer.WriteLine(s);

                    foreach (ZipArchiveEntry entry in archive.Entries)
                        entry.ExtractToFile(args.Last() + @"\" + entry.Name);
                }
                catch (Exception e) { writer.WriteLine(e); }
            }
        }
    }
}
