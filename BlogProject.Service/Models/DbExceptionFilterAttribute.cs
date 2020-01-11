using System.Net;
using System.Net.Http;
using System.Data.Common;
using System.Web.Http.Filters;

namespace BlogProject.Service.Models
{
    public class DbExceptionFilterAttribute: ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            if(actionExecutedContext.Exception is DbException)
            {
                actionExecutedContext.Response = new HttpResponseMessage(HttpStatusCode.ExpectationFailed);
            }
        }
    }
}