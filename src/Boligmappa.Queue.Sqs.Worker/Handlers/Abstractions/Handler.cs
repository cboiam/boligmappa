using System.Text.Json;
using Boligmappa.Configuration;
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
            var resultData = JsonSerializer.Serialize(result, Configurations.SerializerOptions);
            await connection.InvokeAsync("SendEventResponse", message, resultData);
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
            var resultData = JsonSerializer.Serialize(result, Configurations.SerializerOptions);
            await connection.InvokeAsync("SendEventResponse", message, resultData);
            await ExecuteAfterHandle(message);
        }

        protected abstract Task<object> Handle(T data);

        protected virtual Task ExecuteAfterHandle(Message message) { return Task.CompletedTask; }
    }
}