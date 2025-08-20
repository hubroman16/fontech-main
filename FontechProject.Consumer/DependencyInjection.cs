using Microsoft.Extensions.DependencyInjection;

namespace FontechProject.Consumer;

public static class DependencyInjection
{
    public static void AddConsumer(this IServiceCollection services)
    {
        services.AddHostedService<RabbitMqListener>();
    }

}