using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileHubClient.Misc
{
    public interface IClientSettingState
    {
        bool LogOnDuringServiceStart
        {
            get;
            set;
        }

        int ClientId
        {
            get;
            set;
        }

        string ClientKey
        {
            get;
            set;
        }

        string IP
        {
            get;
            set;
        }

        int Port
        {
            get;
            set;
        }

        void Save();
        void Load();
        int LogOn();
        int LogOff();
    }
}
