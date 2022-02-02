using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.WebAPI.ExceptionLayer
{
    public interface IExceptionDA
    {
        Task<string> addExceptionDetails(string ClassName, string MethodName, string RequestData,
                                                      string Message, string ExceptionDetails, int UserId, int ApplicationId);
    }
}
