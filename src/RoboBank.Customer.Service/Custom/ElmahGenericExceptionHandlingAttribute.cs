using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;

namespace RoboBank.Customer.Service.Custom
{
    //TODO: Extract this in a NuGet package and publish in an internal repository (reusable WebAPI-based infrastructure)
    public class ElmahGenericExceptionHandlingAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            Elmah.ErrorLog.GetDefault(HttpContext.Current).Log(new Elmah.Error(actionExecutedContext.Exception, HttpContext.Current));

            actionExecutedContext.Response =
                actionExecutedContext.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, actionExecutedContext.Exception.Message);
        }
    }
}
