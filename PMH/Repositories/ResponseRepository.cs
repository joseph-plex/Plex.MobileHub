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

namespace Plex.PMH.Repositories
{
    public class Responses
    {
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

        public XmlDocument GetResponse(int Id)
        {
            XmlDocument x = clean(new XmlDocument());
            try
            {
                while (!Repo.ContainsKey(Id))
                    Thread.Yield();
                if (Repo[Id] is ResponseCompound)
                    while (!((ResponseCompound)Repo[Id]).IsComplete) Thread.Yield();
                x.LoadXml(Repo[Id].Resp);
                Repo.Remove(Id);
            }
            catch (Exception e)
            {
                Logs.GetInstance().Add(e.ToString());
                throw;
            }
            return x;
        }

        XmlDocument clean(XmlDocument Document)
        {
            foreach (XmlNode n in Document.ChildNodes)
                if (n.NodeType == XmlNodeType.XmlDeclaration)
                    Document.RemoveChild(n);
            return Document;
        }
        public T GetResponse<T>(int Id)
        {
            using (var stream = new MemoryStream())
            {
                StreamWriter sw = new StreamWriter(stream);
                sw.Write(GetResponse(Id).DocumentElement.OuterXml);
                stream.Position = 0;
                return (T)new XmlSerializer(typeof(T)).Deserialize(stream);
            }
        }

        public Dictionary<int, Response> GetRepo()
        {
            return Repo;
        }
    }
}