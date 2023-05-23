namespace Boligmappa.Core.Queues;

public interface IUserQueue
{
    Task StoreUsers(string connectionId);
}