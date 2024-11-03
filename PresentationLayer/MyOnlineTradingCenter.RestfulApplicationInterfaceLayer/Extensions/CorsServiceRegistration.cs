namespace MyOnlineTradingCenter.RestfulApplicationInterfaceLayer.Extensions;

public static class CorsServiceRegistration
{
    public static void AddCorsServiceRegistrations(this IServiceCollection services)
    {
        services.AddCors(opt => opt.AddDefaultPolicy(policy => policy
        .WithOrigins("http://localhost:4200", "https://localhost:4200").AllowAnyHeader().AllowAnyMethod().AllowCredentials()));
    }
}
