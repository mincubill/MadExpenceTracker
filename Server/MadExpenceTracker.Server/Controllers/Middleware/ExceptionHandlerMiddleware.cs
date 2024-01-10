using MadExpenceTracker.Core.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace MadExpenceTracker.Server.Controllers.Middleware
{
    public class ExceptionHandlerMiddleware : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, 
            Exception exception, 
            CancellationToken cancellationToken)
        {
            if(exception is NotFoundException)
            {
                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                await httpContext.Response.WriteAsJsonAsync(
                    new
                    {
                        Message = "The requested object does not exists on the database"
                    }
                 );
            }
            else if (exception is CannotUpdateException)
            {
                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await httpContext.Response.WriteAsJsonAsync(
                    new
                    {
                        Message = "An error has occured while updating",
                        Exception = exception.Message
                    }
                 );
            }
            else if (exception is NoConfigurationException)
            {
                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = (int)HttpStatusCode.Conflict;
                await httpContext.Response.WriteAsJsonAsync(
                    new
                    {
                        Message = "No configuration created",
                        Exception = exception.Message
                    }
                 );
            }
            else
            {
                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await httpContext.Response.WriteAsJsonAsync(
                    new
                    {
                        Message = "An interal error has occured",
                        Exception = exception
                    }
                 );
            }
            return true;
        }
    }
}
