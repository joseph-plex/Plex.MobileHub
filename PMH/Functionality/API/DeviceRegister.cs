using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Plex.PMH.Data;
using Plex.PMH.Data.Tables;

namespace Plex.PMH.Functionality.API
{
    public static partial class Functions
    {
        public static int DeviceRegister()
        {
            DEVICES device = new DEVICES(){ DEVICE_ID = Utilities.SequenceNextValue(Sequences.DEVICE_ID)};
            device.Insert();
            return device.DEVICE_ID;
        }
    }
}