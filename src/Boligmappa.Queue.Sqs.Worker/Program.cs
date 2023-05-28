using Boligmappa.Configuration;
using Boligmappa.Configuration.Server;
using Boligmappa.Core.Handlers;
using Boligmappa.Core.Handlers.Abstractions;
using Boligmappa.Data.Postgres;
using Boligmappa.Queue.Sqs;
using Boligmappa.Queue.Sqs.Worker;
using Boligmappa.Queue.Sqs.Worker.Abstractions;
using Boligmappa.Service.DummyJson;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var configuration = Configurations.BuildConfiguration();

HubConnection connection = configuration.GetServerSettings()
    .Connect();

var host = Host.CreateDefaultBuilder(args)
    .ConfigureLogging(logging =>
        logging.BuildLogger(configuration)
            .AddConsole())
    .ConfigureServices(services =>
        services.AddLogging()
            .AddSqs(configuration)
            .AddPostgres(configuration)
            .AddDummyJsonService(configuration)
            .AddScoped<IUserHandler, UserHandler>()
            .AddScoped<IPostHandler, PostHandler>()
            .AddScoped<IHandlerFactory, HandlerFactory>()
            .AddSingleton(typeof(HubConnection), connection)
            .AddHostedService<Worker>())
    .Build();

host.Services.RunMigrations();

await host.RunAsync();
