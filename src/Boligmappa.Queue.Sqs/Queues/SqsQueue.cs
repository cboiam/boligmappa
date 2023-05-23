using System.Text.Json;
using Amazon.SQS;
using Boligmappa.Configuration;
using Boligmappa.Queue.Sqs.Queues.Abstractions;
using Microsoft.Extensions.Options;

namespace Boligmappa.Queue.Sqs.Queues;

internal class SqsQueue : ISqsQueue
{
    private readonly SqsSettings settings;
    private readonly IAmazonSQS client;

    public SqsQueue(IOptions<SqsSettings> settings, IAmazonSQS client)
    {
        this.settings = settings.Value;
        this.client = client;
    }

    public async Task SendMessage(Message message)
    {
        var body = JsonSerializer.Serialize(message, Configurations.SerializerOptions);
        await client.SendMessageAsync(settings.QueueUrl, body);
    }
}