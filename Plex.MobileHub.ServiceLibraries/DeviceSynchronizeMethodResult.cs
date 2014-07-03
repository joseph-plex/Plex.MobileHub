using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plex.MobileHub.ServiceLibraries
{
    public class DeviceSynchronizeMethodResult : IMethodResult<MethodResult>, IMethodResult
    {
        public virtual String Msg
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
        public virtual Int32 Response
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
        public virtual DateTime StartTimeStamp { get; set; }
        public virtual DateTime CompletionTimeStamp { get; set; }
        public virtual List<RegisteredQueryResult> Results { get; set; }

        protected virtual MethodResult methodResult { get; set; }

        public DeviceSynchronizeMethodResult()
        {
            Results = new List<RegisteredQueryResult>();
        }

        #region MethodResult Implementations
        public virtual MethodResult Success()
        {
            return methodResult.Success();
        }
        public virtual MethodResult Success(int SuccessCode)
        {
            return methodResult.Success(SuccessCode);
        }
        public virtual MethodResult Success(String SuccessMsg)
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
