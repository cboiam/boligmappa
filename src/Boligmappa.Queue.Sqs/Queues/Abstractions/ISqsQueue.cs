namespace Boligmappa.Queue.Sqs.Queues.Abstractions;

public interface ISqsQueue
{
    Task SendMessage(Message data);
}