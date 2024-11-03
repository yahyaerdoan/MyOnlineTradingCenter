using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using System.Net.Mime;
using System.Text.Json;

namespace MyOnlineTradingCenter.RestfulApplicationInterfaceLayer.Extensions;

public static class ConfigureExceptionHandlerExtensions
{
    public static void UseCongigureExceptionHandler<T>(this WebApplication webApplication, ILogger<T> logger)
    {
        webApplication.UseExceptionHandler(builder =>
        {
            builder.Run(async context =>
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = MediaTypeNames.Application.Json;
                var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (exceptionHandlerFeature != null)
                {
                    logger.LogError(exceptionHandlerFeature.Error.Message);
                   await context.Response.WriteAsync(JsonSerializer.Serialize(new
                   {
                       context.Response.StatusCode,
                       exceptionHandlerFeature.Error.Message,
                       Title = "Error!"
                   }));
                }
            });
        });
    }
}
