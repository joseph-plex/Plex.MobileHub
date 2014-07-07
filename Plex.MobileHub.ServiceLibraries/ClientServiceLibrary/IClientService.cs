﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Plex.MobileHub.ServiceLibraries.ClientServiceLibrary
{
    [ServiceContract(CallbackContract=(typeof(IClientCallback)))]
    public interface IClientService : IDisposable
    {
        IClientCallback ClientCallback { get; }

        [OperationContract(IsOneWay = true)]
        void LogIn(Int32 ClientId, String ClientKey);

        [OperationContract(IsOneWay = true)]
        void LogOut();

    }
}
