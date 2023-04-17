using System.Reflection;
using Core.Consumer;
using Core.NewsAPI;
using MassTransit;

namespace NewsKraken.API;

public static class Startup
{
    public static IServiceCollection RegisterServices(this IServiceCollection service)
    {
        return service
            .AddScoped<NewsGatherer>();
    }

    public static IServiceCollection RegisterMassTransit(this IServiceCollection service)
    {
        var entryAssembly = Assembly.GetExecutingAssembly();
        return service.AddMassTransit(x =>
        {
            // x.AddConsumers(entryAssembly);
            x.AddConsumer<TestConsumer>();
            x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host("rabbitmq",h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });
                cfg.ReceiveEndpoint("ABC", ep =>
                {
                    ep.PrefetchCount = 16;
                    ep.UseMessageRetry(r => r.Interval(2, 100));
                    // ep.ConfigureConsumers(provider);
                    ep.ConfigureConsumer<TestConsumer>(provider);
                });
            }));
        });
    }
}