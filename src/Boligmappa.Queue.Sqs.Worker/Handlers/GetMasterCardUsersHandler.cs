using Boligmappa.Core.Handlers.Abstractions;
using Boligmappa.Queue.Sqs.Worker.Handlers.Abstractions;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;

namespace Boligmappa.Queue.Sqs.Worker.Handlers;

public class GetMasterCardUsersHandler : Handler.EventHandler
{
    private readonly IUserHandler userHandler;

    public GetMasterCardUsersHandler(HubConnection connection, IServiceProvider serviceProvider) : base(connection)
    {
        userHandler = serviceProvider.GetRequiredService<IUserHandler>();
    }

    protected override async Task<object> Handle() =>
        await userHandler.GetMasterCardUsers();
}
