using System.Text.Json;
using Amazon.Lambda.Core;
using Amazon.Lambda.SQSEvents;
using Boligmappa.Configuration;
using Boligmappa.Configuration.Server;
using Boligmappa.Core.Handlers;
using Boligmappa.Core.Handlers.Abstractions;
using Boligmappa.Data.Postgres;
using Boligmappa.Queue.Sqs.Worker.Abstractions;
using Boligmappa.Service.DummyJson;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace Boligmappa.Queue.Sqs.Worker;

public class Function
{
    private readonly IServiceProvider serviceProvider;
    private readonly ILogger<Function> logger;

    public Function()
    {
        var configuration = Configurations.BuildConfiguration();

        HubConnection connection = configuration.GetServerSettings()
            .Connect();

        var services = new ServiceCollection()
            .AddLogging(logging => 
                logging.BuildLogger(configuration)
                    .AddAWSProvider())
            .AddSqs(configuration)
            .AddPostgres(configuration)
            .AddDummyJsonService(configuration)
            .AddScoped<IUserHandler, UserHandler>()
            .AddScoped<IPostHandler, PostHandler>()
            .AddScoped<IHandlerFactory, HandlerFactory>()
            .AddSingleton(typeof(HubConnection), connection);

        serviceProvider = services.BuildServiceProvider()
            .RunMigrations();

        logger = serviceProvider.GetRequiredService<ILogger<Function>>();
    }

    public async Task FunctionHandler(SQSEvent sqsEvent, ILambdaContext context)
    {
        logger.LogInformation("Start handling messages");
        foreach (var message in sqsEvent.Records)
        {
            var handlerFactory = serviceProvider.GetRequiredService<IHandlerFactory>();
            await ProcessMessageAsync(message, context, handlerFactory);
        }
        logger.LogInformation("Messages handled: {count}", sqsEvent.Records.Count);
    }

    private async Task ProcessMessageAsync(SQSEvent.SQSMessage message, ILambdaContext context, IHandlerFactory handlerFactory)
    {
        var body = JsonSerializer.Deserialize<Message>(message.Body, Configurations.SerializerOptions);
        
        logger.LogInformation("Handling event: {type}", body.Type);
        var handler = handlerFactory.GetHandler(body.Type);
        await handler.Handle(body);
    }
}