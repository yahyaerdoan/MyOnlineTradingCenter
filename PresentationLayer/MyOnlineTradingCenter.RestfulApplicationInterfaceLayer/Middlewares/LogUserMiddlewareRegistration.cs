using Serilog.Context;

namespace MyOnlineTradingCenter.RestfulApplicationInterfaceLayer.Middlewares;

public static class LogUserMiddlewareRegistration
{
    public static IApplicationBuilder UseLoggedUserRegistrations(this IApplicationBuilder builder)
    {
        return builder.Use(async (context, next) =>
        {
            var userName = context.User?.Identity?.IsAuthenticated == true ? context.User?.Identity?.Name : "Anonymous";
            using (LogContext.PushProperty("user_name", userName))
            {
                await next();
            }
        });
    }
}
