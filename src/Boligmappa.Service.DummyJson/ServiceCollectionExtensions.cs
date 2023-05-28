using Boligmappa.Core.Services;
using Boligmappa.Service.DummyJson.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Boligmappa.Service.DummyJson;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddDummyJsonService(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DummyJsonSettings>(options => 
            configuration.GetSection(nameof(DummyJsonSettings))
                .Bind(options));

        services.AddTransient<IUserService, UserService>()
            .AddTransient<IPostService, PostService>()
            .AddTransient<ITodoService, TodoService>();

        return services;
    }
}