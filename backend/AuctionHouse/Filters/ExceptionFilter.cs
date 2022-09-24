using AuctionHouse.Models;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace AuctionHouse.Filters
{
    public static class ExceptionFilter
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    var exception = contextFeature.Error;

                    if (contextFeature != null)
                    {
                        await context.Response.WriteAsync(new Error
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = exception.Message
                        }.ToString());
                    }
                });
            });
        }
    }
}
