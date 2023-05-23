using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Boligmappa.Configuration;

public static class Configurations
{
    public static IConfiguration BuildConfiguration()
    {
        return new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddEnvironmentVariables()
            .AddJsonFile("appsettings.json")
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json")
            .Build();
    }

    public static ILoggingBuilder BuildLogger(this ILoggingBuilder logging, IConfiguration configuration)
    {
        return logging.ClearProviders()
            .AddConfiguration(configuration);
    }

    public static JsonSerializerOptions SerializerOptions => new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true
    };
}
