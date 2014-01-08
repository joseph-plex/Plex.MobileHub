using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plex.PMH.Objects
{
    public class MethodResult
    {
        private const int UnhandledErrorCode = -9999;
        private const string UnhandledErrorMessage = "An Unhandled Error Has Occured";
        private const int SuccessfulCode = 0;
        private const string SuccessfulMessage = "The Operation Has Succeeded";

        public string Msg;
        public int Response;

        public MethodResult Success()
        {
            Msg = SuccessfulMessage;
            Response = SuccessfulCode;
            return this;
        }

        public MethodResult Success(int SuccessCode)
        {
            Msg = SuccessfulMessage;
            Response = SuccessCode;
            return this;
        }

        public MethodResult Success(string SuccessMessage)
        {
            Msg = SuccessMessage;
            Response = SuccessfulCode;
            return this;
        }

        public MethodResult Success(int SuccessCode, string SuccessMessage)
        {
            Msg = SuccessMessage;
            Response = SuccessCode;
            return this;
        }
        
        public MethodResult Failure()
        {
            Msg = UnhandledErrorMessage;
            Response = UnhandledErrorCode;
            return this;
        }

        public MethodResult Failure(Exception e)
        {
            Msg = e.Message;
            Response = UnhandledErrorCode;
            return this;
        }

        public MethodResult Failure(int ErrorCode)
        {
            Msg = UnhandledErrorMessage;
            Response = ErrorCode;
            return this;
        }

        public MethodResult Failure(string ErrorMessage)
        {
            Msg = ErrorMessage;
            Response = UnhandledErrorCode;
            return this;
        }

        public MethodResult Failure(int ErrorCode, Exception e)
        {
            Msg = e.Message;
            Response = ErrorCode;
            return this;
        }

        public MethodResult Failure(int ErrorCode, string ErrorMessage)
        {
            Msg = ErrorMessage;
            Response = ErrorCode;
            return this;
        }
    }
}