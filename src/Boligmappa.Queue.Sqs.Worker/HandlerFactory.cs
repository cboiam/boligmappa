using Boligmappa.Queue.Sqs.Worker.Handlers.Abstractions;
using Boligmappa.Queue.Sqs.Worker.Handlers;
using Microsoft.AspNetCore.SignalR.Client;

namespace Boligmappa.Queue.Sqs.Worker;

public class HandlerFactory : IHandlerFactory
{
    private readonly HubConnection connection;
    private readonly IServiceProvider serviceProvider;

    public HandlerFactory(HubConnection connection, IServiceProvider serviceProvider)
    {
        this.connection = connection;
        this.serviceProvider = serviceProvider;
    }

    public IHandler GetHandler(MessageType messageType)
    {
        return messageType switch
        {
            MessageType.GetFeaturedHistoryPosts => new GetFeaturedHistoryPostsHandler(connection, serviceProvider),
            MessageType.StoreUsers => new StoreUsersHandler(connection, serviceProvider),
            MessageType.GetPopularUsers => new GetPopularUsersHandler(connection, serviceProvider),
            MessageType.GetMasterCardUsers => new GetMasterCardUsersHandler(connection, serviceProvider),
            MessageType.StorePosts => new StorePostsHandler(connection, serviceProvider),
            _ => throw new NotImplementedException()
        };
    }
}
