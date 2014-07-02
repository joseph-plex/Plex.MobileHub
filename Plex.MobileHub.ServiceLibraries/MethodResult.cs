using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Plex.MobileHub.ServiceLibraries
{
    public class MethodResult : IMethodResult<MethodResult>
    {
        private const int SuccessfulCode = 0;
        private const int UnhandledErrorCode = -9999;
        private const string SuccessfulMessage = "The Operation Has Succeeded";
        private const string UnhandledErrorMessage = "An Unhandled Error Has Occured";

        public string Msg { get; set; }
        public int Response { get; set; }

        public virtual MethodResult Success()
        {
            Response = SuccessfulCode;
            Msg = SuccessfulMessage;
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
            Response = SuccessfulCode;
            Msg = SuccessMessage;
            return this;
        }

        public virtual MethodResult Success(int SuccessCode, string SuccessMessage)
        {
            Response = SuccessCode;
            Msg = SuccessMessage;
            return this;
        }

        public virtual MethodResult Failure()
        {
            Response = UnhandledErrorCode;
            Msg = UnhandledErrorMessage;
            return this;
        }

        public virtual MethodResult Failure(Exception e)
        {
            Response = UnhandledErrorCode;
            Msg = e.ToString();
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
            Response = UnhandledErrorCode;
            Msg = ErrorMessage;
            return this;
        }

        public virtual MethodResult Failure(int ErrorCode, Exception e)
        {
            Response = ErrorCode;
            Failure(e);
            return this;
        }

        public virtual MethodResult Failure(int ErrorCode, string ErrorMessage)
        {
            Response = ErrorCode;
            Msg = ErrorMessage;
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
