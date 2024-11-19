using Microsoft.Extensions.DependencyInjection;

namespace MyOnlineTradingCenter.ApplicationLayer.Concretions.Extenions;

public static class ServiceRegistration
{
    public static void AddApplicationServiceRegistrations(this IServiceCollection services)
    {
        services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(typeof(ServiceRegistration).Assembly));
    }
}
