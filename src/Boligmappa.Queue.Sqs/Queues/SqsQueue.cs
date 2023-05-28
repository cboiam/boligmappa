using System.Text.Json;
using Amazon.SQS;
using Boligmappa.Configuration;
using Boligmappa.Queue.Sqs.Queues.Abstractions;
using Microsoft.Extensions.Options;
using AwsSqsModel = Amazon.SQS.Model;

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

    public async Task<List<AwsSqsModel.Message>> ReceiveMessagesAsync()
    {
        var response = await client.ReceiveMessageAsync(new AwsSqsModel.ReceiveMessageRequest
        {
            QueueUrl = settings.QueueUrl,
            MaxNumberOfMessages = settings.MaxMessages,
            WaitTimeSeconds = settings.WaitTime
        });
        return response.Messages;
    }

    public async Task DeleteMessage(AwsSqsModel.Message message)
    {
        await client.DeleteMessageAsync(settings.QueueUrl, message.ReceiptHandle);
    }
}