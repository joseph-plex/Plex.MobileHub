using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Plex.MobileHub.Data;

namespace Plex.MobileHub.ServiceLibraries
{
    public class QryResult : PlexQueryResult,  IMethodResult<MethodResult>, IMethodResult
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
        public virtual DateTime CompetelionTimeStamp { get; set; }

        public virtual Int32 DBErrorCode { get; set; }
        public virtual String DBErrorMessage { get; set; }

        protected MethodResult methodResult;
        public QryResult() 
            : base ()
        {
            methodResult = new MethodResult();
        }

        public QryResult(IDataReader r)
            : base (r)
        {
  
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
