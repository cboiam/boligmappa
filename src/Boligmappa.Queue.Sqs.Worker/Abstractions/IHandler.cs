namespace Boligmappa.Queue.Sqs.Worker.Abstractions;

public interface IHandler
{
    Task Handle(Message message);
}
