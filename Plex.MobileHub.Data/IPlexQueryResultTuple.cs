using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml;

namespace Plex.MobileHub.Data
{
    public interface IPlexQueryResultTuple  
    {
        Object this[int i] { get; set; }

        List<Object> Values { get; set; }

    }
}
