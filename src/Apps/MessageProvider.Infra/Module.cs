using Microsoft.Extensions.DependencyInjection;

namespace MessageProvider.Infra;

public static class Module
{
    public static IServiceCollection AddAdapters(this IServiceCollection services)
    {
        services.AddTransient<IServiceBusAdapter, ServiceBusAdapter>();

        return services;
    }
}