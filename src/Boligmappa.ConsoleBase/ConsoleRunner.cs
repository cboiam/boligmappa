using Boligmappa.Configuration;
using Boligmappa.Configuration.Server;
using EasyConsole;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Boligmappa.ConsoleBase;

public static class ConsoleRunner
{
    public static void Start(Func<Menu, Menu> menuBuilder)
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

        var cancellationTokenSource = new CancellationTokenSource();
        Console.CancelKeyPress += (s, e) =>
        {
            Console.Clear();
            Console.WriteLine("Exiting application...");
            cancellationTokenSource.Cancel();
        };

        connection.Closed += async (error) =>
        {
            await Task.Delay(new Random().Next(0, 5) * 1000);
            await connection.StartAsync(cancellationTokenSource.Token);
            ServerEventHandler.Configure(connection, cancellationTokenSource.Token);
        };

        ServerEventHandler.Configure(connection, cancellationTokenSource.Token);
        ServerEventHandler.ConfigureEventResponseHandling(connection);

        var menu = menuBuilder(new())
            .Add("Exit", () => cancellationTokenSource.Cancel());

        while (!cancellationTokenSource.IsCancellationRequested)
        {
            Console.Clear();
            menu.Display();
        }
    }
}