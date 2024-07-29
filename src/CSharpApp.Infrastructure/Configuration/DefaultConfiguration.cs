using CSharpApp.Application.Factories;
using CSharpApp.Application.Wrappers;

namespace CSharpApp.Infrastructure.Configuration;

public static class DefaultConfiguration
{
    public static IServiceCollection AddDefaultConfiguration(this IServiceCollection services)
    {
        // Wrappers
        services.AddSingleton<IHttpClientWrapper, HttpClientWrapper>();

        // Factories
        services.AddSingleton<IHttpClientFactory, HttpClientFactory>();

        // Services
        services.AddSingleton<ITodoService, TodoService>();
        services.AddSingleton<IPostService, PostService>();

        return services;
    }
}