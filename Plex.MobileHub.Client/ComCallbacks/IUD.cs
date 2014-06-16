using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileHubClient.PMH;
using MobileHubClient.Data;
namespace MobileHubClient.ComCallbacks
{
    public static partial class Functions
    {
        public static MethodResult IUD(String Code, IUDData [] Data)
        {
            MethodResult mr = new MethodResult();
            using(var Conn = Context.GetOpenConnection(Code))
            using(var transaction = Conn.BeginTransaction())
            {
                foreach(var iud in Data)
                {
                }
            }
            return mr;
        }
    }
}
