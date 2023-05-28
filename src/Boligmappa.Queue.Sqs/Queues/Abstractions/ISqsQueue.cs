using AwsSqsModel = Amazon.SQS.Model;

namespace Boligmappa.Queue.Sqs.Queues.Abstractions;

public interface ISqsQueue
{
    Task SendMessage(Message data);
    Task<List<AwsSqsModel.Message>> ReceiveMessagesAsync();
    Task DeleteMessage(AwsSqsModel.Message message);
}