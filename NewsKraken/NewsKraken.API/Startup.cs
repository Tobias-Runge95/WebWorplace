using System.Reflection;
using Core;
using Core.Consumer;
using Core.NewsAPI;
using MassTransit;

namespace NewsKraken.API;

public static class Startup
{
    public static IServiceCollection RegisterServices(this IServiceCollection service)
    {
        return service
            .AddScoped<NewsGatherer>()
            .AddScoped<ArticleMapper>();
    }

    public static IServiceCollection RegisterMassTransit(this IServiceCollection service)
    {
        var entryAssembly = GetAssemblies();
        return service.AddMassTransit(x =>
        {
            x.AddConsumers(entryAssembly.ToArray());
            // x.AddConsumer<TestConsumer>();
            // x.AddConsumersFromNamespaceContaining<TestConsumer>();
            x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host(new Uri("rabbitmq://localhost"),h =>
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
                    ep.ConfigureConsumer<GetNewsConsumer>(provider);
                    ep.ConfigureConsumer<LoadArticleConsumer>(provider);
                    ep.ConfigureConsumer<LoadAllSavedArticlesConsumer>(provider);
                    ep.ConfigureConsumer<SaveNewsConsumer>(provider);
                    ep.ConfigureConsumer<SaveMultipleNewsConsumer>(provider);
                });
            }));
        });
    }

    private static List<Assembly> GetAssemblies()
    {
        var assembly = Assembly.GetAssembly(typeof(TestConsumer));
        var assemblies = new List<Assembly>();

        foreach (var type in assembly.ExportedTypes)
        {
            if (type.FullName.Contains("Consumer"))
            {
                assemblies.Add(type.Assembly);
            }
        }

        return assemblies;
    }
}