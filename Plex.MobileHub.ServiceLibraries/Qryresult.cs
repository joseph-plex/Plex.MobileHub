using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Plex.MobileHub.Data;

namespace Plex.MobileHub.ServiceLibraries
{
    public class QryResult : PlexQueryResult, IMethodResult<MethodResult>, IMethodResult
    {
        public String Msg
        {
            get
            {
                return methodResult.Msg;
            }
            set
            {
                methodResult.Msg = value;
            }
        }
        public Int32 Response
        {
            get
            {
                return methodResult.Response;
            }
            set
            {
                methodResult.Response = value;
            }
        }
        public DateTime StartTimeStamp { get; set; }
        public DateTime CompetelionTimeStamp { get; set; }

        public Int32 DBErrorCode { get; set; }
        public String DBErrorMessage { get; set; }

        MethodResult methodResult;
        public QryResult()
        {
            methodResult = new MethodResult();
        }

        #region MethodResult Implementations
        public MethodResult Success()
        {
            return methodResult.Success();
        }
        public MethodResult Success(int SuccessCode)
        {
            return methodResult.Success(SuccessCode);
        }
        public MethodResult Success(String SuccessMsg)
        {
            return methodResult.Success(SuccessMsg);
        }
        public MethodResult Success(int SuccessCode, string SuccessMessage)
        {
            return methodResult.Success(SuccessCode, SuccessMessage);
        }

        public MethodResult Failure()
        {
            return methodResult.Failure();
        }
        public MethodResult Failure(Exception e)
        {
            return methodResult.Failure(e);
        }
        public MethodResult Failure(int ErrorCode)
        {
            return methodResult.Failure(ErrorCode);
        }
        public MethodResult Failure(string ErrorMessage)
        {
            return methodResult.Failure(ErrorMessage);
        }
        public MethodResult Failure(int ErrorCode, Exception e)
        {
            return methodResult.Failure(ErrorCode, e);
        }
        public MethodResult Failure(int ErrorCode, string ErrorMessage)
        {
            return methodResult.Failure(ErrorCode, ErrorMessage);
        }
        #endregion
    }
}
