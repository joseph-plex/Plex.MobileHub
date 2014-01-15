using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Threading;
using System.Xml.Serialization;
using System.Web.Script.Serialization;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using Plex.PMH.Objects;
namespace Plex.PMH.Repositories
{
    public class Responses
    {
        private static readonly Type[] BuiltInTypes = {
                                   typeof(bool),
                                   typeof(byte),
                                   typeof(sbyte),
                                   typeof(char),
                                   typeof(decimal),
                                   typeof(double),
                                   typeof(float),
                                   typeof(int),
                                   typeof(uint),
                                   typeof(long),
                                   typeof(ulong),
                                   typeof(object),
                                   typeof(short),
                                   typeof(ushort),
                                   typeof(string), 
                                   typeof(bool?),
                                   typeof(byte?),
                                   typeof(sbyte?),
                                   typeof(char?),
                                   typeof(decimal?),
                                   typeof(double?),
                                   typeof(float?),
                                   typeof(int?),
                                   typeof(uint?),
                                   typeof(long?),
                                   typeof(ulong?),
                                   typeof(short?),
                                   typeof(ushort?),
                               };
        private static Responses responses = new Responses();
        public static Responses Instance
        {
            get
            {
                return responses;
            }
        }

        Dictionary<int, Response> Repo = new Dictionary<int, Response>();

        public void Add(int ResponseId, Response response)
        {
            if (response is ResponseComponent) 
                AddComponent(response.Id, (ResponseComponent)response);
            else
                Repo.Add(ResponseId, response);
        }
        public void AddComponent(int ResponseId, ResponseComponent RespComponent)
        {
            if (Repo.ContainsKey(ResponseId))
                ((ResponseCompound)Repo[ResponseId]).Add(RespComponent);
            else{
                var rc = new ResponseCompound();
                rc.Add(RespComponent);
                Repo.Add(ResponseId, rc);
            }
        }

        public void Modify(int ResponseId, Response response)
        {
            Repo[ResponseId] = response;
        }

        public Response Retrieve(int ResponseId)
        {
            return Repo[ResponseId];
        }

        public void Remove(int ResponseId)
        {
            Repo.Remove(ResponseId);
        }

        public T GetResponse<T>(int Id)
        {
            return ToObj<T>(GetResponse(Id));
        }

        public XmlDocument GetResponse(int Id)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                while (!Repo.ContainsKey(Id))
                    Thread.Yield();
                if (Repo[Id] is ResponseCompound)
                    while (!((ResponseCompound)Repo[Id]).IsComplete) Thread.Yield();
                doc.LoadXml(Repo[Id].Resp);
                doc = RemoveXmlns(doc);
                Repo.Remove(Id);
                return doc;
            }
            catch (Exception e)
            {
                Logs.GetInstance().Add(e.ToString());
                throw;
            }
        }

        public Dictionary<int, Response> GetRepo()
        {
            return Repo;
        }

        public static T ToObj<T>(XmlDocument x)
        {
            using (var reader = new XmlNodeReader(x))
                return (T)new XmlSerializer(typeof(T),BuiltInTypes).Deserialize(reader);
        }

        static T ToObj<T>(string xml)
        {
            var x = new XmlDocument();
            x.LoadXml(xml);
            return ToObj<T>(x);
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

            var xmlDocument = new XmlDocument();
            using (var xmlReader = d.CreateReader())
                xmlDocument.Load(xmlReader);
            return xmlDocument;
        }
    }
}