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
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(File.ReadAllText("input.txt"));
                    RemoveXmlns(doc).Save(Console.Out);
                    var s = service.GetSerialData(doc.OuterXml);
                    return;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            Console.Read();
        }

        public static XmlDocument RemoveXmlns(XmlDocument doc)
        {
            XDocument d;
            using (var nodeReader = new XmlNodeReader(doc))
            {
                nodeReader.MoveToContent();
                d = XDocument.Load(nodeReader);
            }

            d.Root.Descendants().Attributes().Where(x => x.IsNamespaceDeclaration).Remove();
            foreach (var elem in d.Descendants())
                elem.Name = elem.Name.LocalName;

            //foreach (var attr in d.Descendants().Attributes())
            //{
            //    var elem = attr.Parent;
            //    attr.Remove();
            //    elem.Add(new XAttribute(attr.Name.LocalName, attr.Value));
            //}

            var xmlDocument = new XmlDocument();
            using(var xmlReader = d.CreateReader())
                xmlDocument.Load(xmlReader);
            return xmlDocument;
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
