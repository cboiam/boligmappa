using System.Text.Json;
using Boligmappa.Configuration;
using Boligmappa.Queue.Sqs;
using Boligmappa.Queue.Sqs.Queues.Abstractions;
using Boligmappa.Queue.Sqs.Worker.Abstractions;
using Microsoft.Extensions.Logging;
using AwsSqsModel = Amazon.SQS.Model;

public class Worker : Microsoft.Extensions.Hosting.BackgroundService
{
    private readonly ISqsQueue sqsQueue;
    private readonly ILogger<Worker> logger;
    private readonly IHandlerFactory handlerFactory;

    public Worker(ISqsQueue sqsQueue, ILogger<Worker> logger, IHandlerFactory handlerFactory)
    {
        this.sqsQueue = sqsQueue;
        this.logger = logger;
        this.handlerFactory = handlerFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            logger.LogInformation("Start handling messages");
            var messages = await sqsQueue.ReceiveMessagesAsync();
            await ProcessMessagesAsync(messages);
            logger.LogInformation("Messages handled: {count}", messages.Count());
        }
    }

    public async Task ProcessMessagesAsync(IEnumerable<AwsSqsModel.Message> messages)
    {
        foreach (var message in messages)
        {
            var body = JsonSerializer.Deserialize<Message>(message.Body, Configurations.SerializerOptions);
            
            logger.LogInformation("Handling event: {type}", body.Type);
            var handler = handlerFactory.GetHandler(body.Type);
            await handler.Handle(body);
            logger.LogInformation("Sending response to {user}", body.User);

            await sqsQueue.DeleteMessage(message);
        }
    }
}