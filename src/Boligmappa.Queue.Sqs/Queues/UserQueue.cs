using Boligmappa.Core.Queues;
using Boligmappa.Queue.Sqs.Queues.Abstractions;

namespace Boligmappa.Queue.Sqs.Queues;

public class UserQueue : IUserQueue
{
    private readonly ISqsQueue queue;

    public UserQueue(ISqsQueue queue)
    {
        this.queue = queue;
    }
    
    public async Task StoreUsers(string connectionId)
    {
        await queue.SendMessage(new Message
        {
            User = connectionId,
            Type = MessageType.StoreUsers
        });
    }
}