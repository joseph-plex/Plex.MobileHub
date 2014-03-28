using System;
using System.Xml;
using System.Xml.Serialization;

namespace MobileHub.Objects
{
    public class MethodResult
    {
        private const int UnhandledErrorCode = -9999;
        private const string UnhandledErrorMessage = "An Unhandled Error Has Occured";
        private const int SuccessfulCode = 0;
        private const string SuccessfulMessage = "The Operation Has Succeeded";

        public string Msg;
        public int Response;

        public virtual MethodResult Success()
        {
            Msg = SuccessfulMessage;
            Response = SuccessfulCode;
            return this;
        }

        public virtual MethodResult Success(int SuccessCode)
        {
            Msg = SuccessfulMessage;
            Response = SuccessCode;
            return this;
        }

        public virtual MethodResult Success(string SuccessMessage)
        {
            Msg = SuccessMessage;
            Response = SuccessfulCode;
            return this;
        }

        public virtual MethodResult Success(int SuccessCode, string SuccessMessage)
        {
            Msg = SuccessMessage;
            Response = SuccessCode;
            return this;
        }

        public virtual MethodResult Failure()
        {
            Msg = UnhandledErrorMessage;
            Response = UnhandledErrorCode;
            return this;
        }

        public virtual MethodResult Failure(Exception e)
        {
            Msg = e.Message;
            Response = UnhandledErrorCode;
            return this;
        }

        public virtual MethodResult Failure(int ErrorCode)
        {
            Msg = UnhandledErrorMessage;
            Response = ErrorCode;
            return this;
        }

        public virtual MethodResult Failure(string ErrorMessage)
        {
            Msg = ErrorMessage;
            Response = UnhandledErrorCode;
            return this;
        }

        public virtual MethodResult Failure(int ErrorCode, Exception e)
        {
            Msg = e.Message;
            Response = ErrorCode;
            return this;
        }

        public virtual MethodResult Failure(int ErrorCode, string ErrorMessage)
        {
            Msg = ErrorMessage;
            Response = ErrorCode;
            return this;
        }

        public static implicit operator XmlDocument(MethodResult m)
        {
            XmlDocument d = new XmlDocument();
            using (var sw = new System.IO.StringWriter())
            {
                new XmlSerializer(m.GetType()).Serialize(sw, m);
                d.LoadXml(sw.ToString());
                return d;
            }
        }
    }
}