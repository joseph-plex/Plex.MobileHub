using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ServiceModel;
using System.Web.Services.Discovery;

using MobileHub.Repositories;

namespace MobileHub.Functionality.Behaviors.Miscellaneous
{
    public class RequestClientCommandFetchBehavior : RequestBehavior<Boolean>
    {
        public const string BindingTypeIdentifier = "webs";
        public const string ServiceName = "pmhc";
        public string IPv4
        {
            get;
            set;
        }

        public int Port
        {
            get;
            set;
        }

        public Boolean HandleRequest()
        {

            //todo complete this function by using the svcutility to generate the ChannelFactory Class and invoke the correct method.
            var Uri = @"http://" + IPv4 + @":" + Port + @"/" + ServiceName + @"/" + BindingTypeIdentifier;
            var BindingSecurity = new WSHttpSecurity();
            var Binding = new WSHttpBinding();
            var EP = new EndpointAddress(Uri);


            BindingSecurity.Mode = SecurityMode.None;
            Binding.Security = BindingSecurity;

            var CFactory = new ChannelFactory<Object>(Binding, EP);

            try
            {
                var Channel = CFactory.CreateChannel();
            }
            catch (Exception e)
            {
                Logs.Instance.Add(e);
            }

            return false;
        }
    }
}