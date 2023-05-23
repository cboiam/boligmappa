using Boligmappa.Core.Repositories;
using Boligmappa.Data.Postgres.Repositories;
using Boligmappa.Data.Postgres.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Boligmappa.Data.Postgres;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPostgres(this IServiceCollection services, IConfiguration configuration)
    {
        var rdsSettingsSection = configuration.GetSection(nameof(RdsSettings));
        services.Configure<RdsSettings>(options => rdsSettingsSection.Bind(options));
        var rdsSettings = rdsSettingsSection.Get<RdsSettings>();

        var password = configuration.GetValue<string>("DB_PASSWORD");

        services.AddDbContext<BoligmappaContext>(options =>
            options.UseNpgsql(rdsSettings.BuildConnectionString(password)));

        return services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>))
           .AddScoped<IUserRepository, UserRepository>()
           .AddScoped<IPostRepository, PostRepository>();
    }

    public static IServiceProvider RunMigrations(this IServiceProvider serviceProvider)
    {
        serviceProvider.GetRequiredService<BoligmappaContext>()
            .Database.Migrate();
        return serviceProvider;
    }
}