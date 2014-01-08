using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plex.PMH.Repositories
{
    public class Response
    {
        public virtual int Id
        {
            get;
            set;

        }
        public virtual string Resp
        {
            get;
            set;
        }
        public virtual bool IsError
        {
            get;
            set;
        }
    }
}