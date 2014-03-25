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
using System.Diagnostics;

using MobileHub.Objects;

namespace MobileHub.Repositories
{
    public class Responses
    {
        private static readonly Type[] BuiltInTypes;
        private static Responses responses = new Responses();

        public static Responses Instance
        {
            get
            {
                return responses;
            }
        }
        static Responses()
        {
            BuiltInTypes = new Type[]  
             {
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
                var xmlDocument = new XmlDocument();
                while (!Repo.ContainsKey(Id)) 
                    Thread.Yield();
                if (Repo[Id] is ResponseCompound)
                    while (!((ResponseCompound)Repo[Id]).IsComplete) 
                        Thread.Yield();

                xmlDocument.LoadXml(Repo[Id].Resp);
                Repo.Remove(Id);

                return xmlDocument;
            }
            catch (Exception e)
            {
                Logs.Instance.Add(e.ToString());
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
    }
}