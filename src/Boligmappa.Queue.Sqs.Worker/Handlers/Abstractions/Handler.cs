using Boligmappa.Queue.Sqs.Worker.Abstractions;
using Microsoft.AspNetCore.SignalR.Client;

namespace Boligmappa.Queue.Sqs.Worker.Handlers.Abstractions;

public static class Handler
{
    public abstract class EventHandler : IHandler
    {
        private readonly HubConnection connection;

        protected EventHandler(HubConnection connection)
        {
            this.connection = connection;
        }

        public async Task Handle(Message message)
        {
            var result = await Handle();
            if (connection.State == HubConnectionState.Disconnected)
            {
                await connection.StartAsync();
            }
            await connection.SendAsync("SendEventResponse", message, result);
            await ExecuteAfterHandle(message);
        }

        protected abstract Task<object> Handle();

        protected virtual Task ExecuteAfterHandle(Message message) { return Task.CompletedTask; }
    }

    public abstract class PayloadHandler<T> : IHandler
    {
        private readonly HubConnection connection;

        protected PayloadHandler(HubConnection connection)
        {
            this.connection = connection;
        }

        public async Task Handle(Message message)
        {
            var data = message.GetData<T>();
            var result = await Handle(data);
            await connection.SendAsync("SendEventResponse", message, result);
            await ExecuteAfterHandle(message);
        }

        protected abstract Task<object> Handle(T data);

        protected virtual Task ExecuteAfterHandle(Message message) { return Task.CompletedTask; }
    }
}