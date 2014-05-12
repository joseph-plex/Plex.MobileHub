using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ServiceModel;
using System.ServiceModel.Web;

using System.IO;
using System.Reflection;
using System.Xml.Serialization;
using System.Globalization;
using MobileHubClient.PMH;
using MobileHubClient.Misc;
using MobileHubClient.ComCallbacks;
using MobileHubClient.Core;
using Plex.Logs;
namespace MobileHubClient.Channels.External
{
    [ServiceContract(Namespace = "PMHC")]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    class ExternalService : PlexServiceBase
    {
        public const int MaxExternalChannelOutgoingMessageSize = 16777215;

        [OperationContract]
        public  void RetrieveCommands()
        {
            ClientService.Logs.Add("Requests to pull retrieved from server");
            try
            {
                var result = WebService.GetCommands(Context.clientInstanceId);
                Parallel.ForEach<Command>(result, Command =>
                {
                    ClientService.Logs.Add(new Log("Executing Command : " + Command.Name, LogPriority.Low));
                    var ret = typeof(Functions).InvokeMember(Command.Name, BindingFlags.InvokeMethod | BindingFlags.Static | BindingFlags.Public, Type.DefaultBinder, null, Command.Params );
                    SendResponse(Command, ret);
                });
            }
            catch (AggregateException ae)
            {
                foreach (var x in ae.InnerExceptions)
                    ClientService.Logs.Add(new Log(x.ToString()));
            }
            catch(Exception e)
            {
                ClientService.Logs.Add(new Log(e.ToString()));
                throw;
            }
        }

        static void SendResponse(Command command, object ret)
        {
            Response r = new Response();
            try
            {
                if (ret != null)
                    using (var ms = new StringWriter(CultureInfo.CurrentCulture))
                    {
                        new XmlSerializer(ret.GetType()).Serialize(new NoNameSpaceXmlWriter(ms), ret);
                        r.Resp = ms.ToString();
                    }
                else
                    r.Resp = string.Empty;
            }
            catch (Exception e)
            {
                r.Resp = e.ToString();
                r.IsError = true;
            }
            r.Id = command.RequestId;
            if (r.Resp.Length < MaxExternalChannelOutgoingMessageSize)
                WebService.Respond(r);
            else
                PartialResponse(r);
        }

        static void PartialResponse(Response r)
        {
            var components = r.Resp.SplitByLength(MaxExternalChannelOutgoingMessageSize).ToList();
            var totalparts = components.Count();

            for (int i = 0; i < totalparts; i++)
                WebService.ResponsePartial(new ResponseComponent() { Id = r.Id, Resp = components[i], ComponentId = i, IsLastComponent = (totalparts - 1 == i) });
        }
    }
}
