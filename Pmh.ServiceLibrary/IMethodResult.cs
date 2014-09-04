using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Pmh.ServiceLibrary
{
    public interface IMethodResult
    {
        String Msg { get; set; }
        int Response { get; set; }
    }

    public interface IMethodResult<T> : IMethodResult where T : IMethodResult, new()
    {
        T Success();
        T Success(int SuccessCode);
        T Success(String SuccessMsg);
        T Success(int SuccessCode, string SuccessMessage);

        T Failure();
        T Failure(Exception e);
        T Failure(int ErrorCode);
        T Failure(string ErrorMessage);
        T Failure(int ErrorCode, Exception e);
        T Failure(int ErrorCode, string ErrorMessage);
    }
}
