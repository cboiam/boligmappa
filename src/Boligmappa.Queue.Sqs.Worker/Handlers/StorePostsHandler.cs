using Boligmappa.Core.Handlers.Abstractions;
using Boligmappa.Queue.Sqs.Worker.Handlers.Abstractions;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;

namespace Boligmappa.Queue.Sqs.Worker.Handlers;

public class StorePostsHandler : Handler.EventHandler
{
    private readonly IPostHandler postHandler;

    public StorePostsHandler(HubConnection connection, IServiceProvider serviceProvider) : base(connection)
    {
        postHandler = serviceProvider.GetRequiredService<IPostHandler>();
    }

    protected override async Task<object> Handle() =>
        await postHandler.StorePosts();
}
