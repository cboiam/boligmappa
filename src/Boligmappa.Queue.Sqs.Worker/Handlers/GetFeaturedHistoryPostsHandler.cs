using Boligmappa.Core.Handlers.Abstractions;
using Boligmappa.Core.Queues;
using Boligmappa.Queue.Sqs.Worker.Handlers.Abstractions;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;

namespace Boligmappa.Queue.Sqs.Worker.Handlers;

public class GetFeaturedHistoryPostsHandler : Handler.EventHandler
{
    private readonly IPostHandler postHandler;
    private readonly IUserQueue userQueue;

    public GetFeaturedHistoryPostsHandler(HubConnection connection, IServiceProvider serviceProvider) : base(connection)
    {
        postHandler = serviceProvider.GetRequiredService<IPostHandler>();
        userQueue = serviceProvider.GetRequiredService<IUserQueue>();
    }

    protected override async Task<object> Handle() =>
        await postHandler.GetFeaturedHistoryPosts();

    protected override async Task ExecuteAfterHandle(Message message) =>
        await userQueue.StoreUsers(message.User);
}
