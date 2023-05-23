namespace Boligmappa.Queue.Sqs.Worker.Abstractions;

public interface IHandlerFactory
{
    IHandler GetHandler(MessageType messageType);
}