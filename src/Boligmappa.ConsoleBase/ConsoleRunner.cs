using Boligmappa.Configuration;
using Boligmappa.Configuration.Server;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Boligmappa.ConsoleBase;

public static class ConsoleRunner
{
    public static async Task Start(Func<Menu, Menu> menuBuilder)
    {
        var configuration = Configurations.BuildConfiguration();

        IHost host = new HostBuilder()
            .ConfigureAppConfiguration(config =>
                config.AddConfiguration(configuration))
            .ConfigureLogging(logging =>
                logging.BuildLogger(configuration)
                    .AddConsole())
            .ConfigureServices(services =>
                services.AddLogging())
            .Build();

        HubConnection connection = configuration.GetServerSettings()
            .Connect();

        await connection.StartAsync();

        var cancellationTokenSource = new CancellationTokenSource();
        Console.CancelKeyPress += (s, e) =>
        {
            Console.Clear();
            cancellationTokenSource.Cancel();
        };

        var menu = menuBuilder(new())
            .Add("Exit", () =>
            {
                cancellationTokenSource.Cancel();
                return Task.CompletedTask;
            });

        var consoleTimeout = configuration.GetValue<int>("ConsoleTimeout");

        ServerEventHandler.Configure(connection, menu, cancellationTokenSource, consoleTimeout);

        await menu.Display(consoleTimeout, cancellationTokenSource);
        while (!cancellationTokenSource.IsCancellationRequested) { }
    }
}