using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.Logging;
using System.Net;
using WebAPI.DTOs;
using WebAPI.Exceptions;
using WebAPI.Utils;

namespace WebAPI.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        private static ILoggerManager _logger;

        public static void Configure(ILoggerManager logger)
        {
            _logger = logger;
        }
       
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
                        int statusCode = contextFeature.Error switch
                        {
                            NotFoundException => StatusCodes.Status404NotFound,
                            NotAuthorizeException => StatusCodes.Status401Unauthorized,
                            _ => StatusCodes.Status500InternalServerError
                        };

                        context.Response.StatusCode = statusCode;

                        string message = contextFeature.Error switch
                        {
                            NotFoundException => contextFeature.Error.Message,
                            NotAuthorizeException => contextFeature.Error.Message,
                            _ => "Internal Server Error"
                        };

                        _logger.LogError(statusCode + " " + message);
                   

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
