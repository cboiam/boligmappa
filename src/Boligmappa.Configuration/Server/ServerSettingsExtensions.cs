using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;

namespace Boligmappa.Configuration.Server;

public static class ServerSettingsExtensions
{
    public static ServerSettings GetServerSettings(this IConfiguration configuration)
    {
        return configuration.GetSection(nameof(ServerSettings))
            .Get<ServerSettings>();
    }

    public static HubConnection Connect(this ServerSettings settings)
    {
        return new HubConnectionBuilder()
            .WithUrl(new Uri(settings.Endpoint))
            .WithAutomaticReconnect(new[] { TimeSpan.Zero, TimeSpan.Zero, TimeSpan.FromSeconds(10) })
            .Build();
    }
}