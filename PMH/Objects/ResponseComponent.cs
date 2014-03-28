using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MobileHub.Objects
{
    public class ResponseComponent : Response
    {
        public virtual int ComponentId
        {
            get;
            set;
        }
        public virtual bool IsLastComponent
        {
            get;
            set;
        }

        public ResponseComponent()
        {
        }
    }
}