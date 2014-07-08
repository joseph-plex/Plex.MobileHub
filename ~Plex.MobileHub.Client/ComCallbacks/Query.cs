using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.Data;
using System.Data.Common;
using MobileHubClient.PMH;
using System.Threading;
using Oracle.DataAccess.Client;
using MobileHubClient.Data;
using MobileHubClient.Core;
namespace MobileHubClient.ComCallbacks
{
    public static partial class Functions
    {
        public static RQryResult Query(string dbCode, string qry)
        {
            RQryResult result = new RQryResult();
            try
            {
                using (var Conn = Context.GetOpenConnection(dbCode))
                {
                    var sTime = DateTime.Now;
                    result = Conn.Query(qry).ToQueryResult();
                    var eTime = DateTime.Now;

                    result.Response = result.Rows.Length;
                    result.CompletionTimeStamp = eTime;
                    result.StartTimeStamp = sTime;
                    result.Msg = "Success";
                }
            }
            catch (DbException e)
            {
                result.DBErrorMessage = e.ToString();
                result.DBErrorCode = e.ErrorCode;
                result.Response = e.HResult;
                result.Msg = e.Message;
            }
            catch (Exception e)
            {
                result.Response = e.HResult;
                result.Msg = e.Message;
            }
            return result;
        }
    }
}
