namespace Boligmappa.Queue.Sqs.Worker.Handlers.Abstractions;

public interface IHandler
{
    Task Handle(Message message);
}
