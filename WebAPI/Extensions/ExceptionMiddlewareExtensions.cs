using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using WebAPI.DTOs;
using WebAPI.Exceptions;

namespace WebAPI.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this WebApplication app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if(contextFeature != null)
                    {
                        //log

                        context.Response.StatusCode = contextFeature.Error switch
                        {
                            NotFoundException => StatusCodes.Status404NotFound,
                            NotAuthorizeException => StatusCodes.Status401Unauthorized,
                            _ => StatusCodes.Status500InternalServerError
                        };

                        string message = contextFeature.Error switch
                        {
                            NotFoundException => contextFeature.Error.Message,
                            NotAuthorizeException => contextFeature.Error.Message,
                            _ => "Internal Server Error"
                        };

                        await context.Response.WriteAsync(new ErrorDetailDTO
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = message
                        }.ToString());
                    }
                });
            });
        }
    }
}
