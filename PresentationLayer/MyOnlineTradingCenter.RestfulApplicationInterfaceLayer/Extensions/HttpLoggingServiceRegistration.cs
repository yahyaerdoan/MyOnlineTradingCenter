using Microsoft.AspNetCore.HttpLogging;

namespace MyOnlineTradingCenter.RestfulApplicationInterfaceLayer.Extensions;

public static class HttpLoggingServiceRegistration
{
    public static void AddHttpLoggingServiceRegistrations(this IServiceCollection services)
    {
        services.AddHttpLogging(logging =>
        {
            logging.LoggingFields = HttpLoggingFields.All;
            logging.RequestHeaders.Add("sec-ch-ua");
            logging.ResponseHeaders.Add("MyResponseHeader");
            logging.MediaTypeOptions.AddText("application/javascript");
            logging.RequestBodyLogLimit = 4096;
            logging.ResponseBodyLogLimit = 4096;
        });
    }
}
