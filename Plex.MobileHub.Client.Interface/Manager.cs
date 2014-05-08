using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Plex.MobileHub.Client.Interface.GeneralService;
namespace Plex.MobileHub.Client.Interface
{
    public class Manager
    {
        public static Manager Instance { get { return instance; } }
        static Manager instance = new Manager();

        public String ClientKey
        {
            get
            {
                using (var Service = GetGeneralService())
                    return Service.GetClientKey();
            }
            set
            {
                using (var Service = GetGeneralService())
                    Service.SetClientKey(value);
            }
        }
        public Int32 ClientId
        {
            get
            {
                using (var Service = GetGeneralService())
                    return Service.GetClientId();
            }
            set
            {
                using (var Service = GetGeneralService())
                    Service.SetClientId(value);
            }
        }
        public String IPAddress
        {
            get
            {
                using (var Service = GetGeneralService())
                    return Service.GetAddress();
            }
            set
            {
                using (var Service = GetGeneralService())
                    Service.SetAddress(value);
            }
        }
        public Int32 Port
        {
            get
            {
                using (var Service = GetGeneralService())
                    return Service.GetPort();
            }
            set
            {
                using (var Service = GetGeneralService())
                    Service.SetPort(value);
            }
        }
        public Boolean AutoLogin
        {
            get{
                using (var Service = GetGeneralService())
                    return Service.GetAutoLogOn();
            }
            set{
                using (var Service = GetGeneralService())
                    Service.SetAutoLogOn(value);
            }
        }
 
        private Manager() { }

        GeneralServiceClient GetGeneralService()
        {
            return new GeneralServiceClient("WSHttpBinding_GeneralService");
        }
    }
}
