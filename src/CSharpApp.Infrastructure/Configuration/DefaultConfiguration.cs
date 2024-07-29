using CSharpApp.Application.Factories;

namespace CSharpApp.Infrastructure.Configuration;

public static class DefaultConfiguration
{
    public static IServiceCollection AddDefaultConfiguration(this IServiceCollection services)
    {
        // Factories
        services.AddSingleton<IHttpClientFactory, HttpClientFactory>();

        // Services
        services.AddSingleton<ITodoService, TodoService>();
        services.AddSingleton<IPostService, PostService>();

        return services;
    }
}