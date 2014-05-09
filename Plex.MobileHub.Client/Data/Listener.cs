using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileHubClient.Data
{
    public class Listener
    {
        public static IEnumerable<string> ExtractServiceNames(IEnumerable<string> listenerOutput)
        {
            string Delimiter = "Services Summary...";
            var Collection = listenerOutput.ToList();

            int Index = Collection.FindIndex((p) => p.StartsWith(Delimiter));

            for (int i = ++Index; i < Collection.Count; i++)
                if (Collection[i].TrimStart().StartsWith("Service"))
                    yield return Collection[i].Split('"')[1];
        }
        public static IEnumerable<string> ExtractEndPointSummary(IEnumerable<string> listenerOutput)
        {
            string StartDelimiter = @"Listening Endpoints Summary...";
            string EndDelimiter = @"Services Summary...";
            var Collection = listenerOutput.ToList();

            int Start = Collection.FindIndex((p) => p.StartsWith(StartDelimiter)) + 1;
            int End = Collection.FindIndex((p) => p.StartsWith(EndDelimiter));

            for (int i = Start; i < End; i++)
                yield return Collection[i];
        }
        public String Executable
        {
            get;
            set;
        }
        public List<String> Companies
        {
            get;
            set;
        }
        public List<DataSourceMap> Maps
        {
            get;
            set;
        }
        public List<String> Sources
        {
            get;
            set;
        }

        public Listener()
        {
            Executable = string.Empty;
            Sources = new List<string>();
            Companies = new List<string>();
            Maps = new List<DataSourceMap>();
        }

   

        public IEnumerable<string> CompanyGetSources(string Company)
        {
            return CompanyGetSources(Companies.FindIndex((a) => a == Company));
        }

        public IEnumerable<string> CompanyGetSources(int Index)
        {
            if (Index < 0) new Exception("Cannot access a negative index");
            foreach (var m in Maps.FindAll((p) => p.CompanyIndex == Index))
                yield return Sources[m.SourceIndex];
        }

        public IEnumerable<String> CompaniesGet()
        {
            return (IEnumerable<String>)Companies;
        }
    }
    public class DataSourceMap
    {
        public int CompanyIndex;
        public int SourceIndex;
    }

    
}
