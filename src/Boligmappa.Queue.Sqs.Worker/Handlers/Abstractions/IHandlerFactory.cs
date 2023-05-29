namespace Boligmappa.Queue.Sqs.Worker.Handlers.Abstractions;

public interface IHandlerFactory
{
    IHandler GetHandler(MessageType messageType);
}