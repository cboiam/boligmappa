using Boligmappa.Queue.Sqs;
using Microsoft.AspNetCore.SignalR.Client;

namespace Boligmappa.ConsoleBase;

public static class ServerEventHandler
{
    private static HubConnection Connection { get; set; }
    private static CancellationToken EventCancellationToken { get; set; }
    private static CancellationTokenSource LoaderCancellationTokenSource { get; set; }
    private static Loader loader = new Loader();

    public static void Configure(HubConnection connection, CancellationToken eventCancellationToken)
    {
        Connection = connection;
        Messages.ConnectionId = connection.ConnectionId;
        EventCancellationToken = eventCancellationToken;
    }

    public static Action SendEvent(Message message)
    {
        return async () =>
        {
            if (Connection.State == HubConnectionState.Disconnected)
            {
                await Connection.StartAsync(EventCancellationToken);
            }
            LoaderCancellationTokenSource = new();
            await Connection.SendAsync("SendEvent", message, EventCancellationToken);
            await loader.Spin(LoaderCancellationTokenSource.Token);
        };
    }

    public static void ConfigureEventResponseHandling(HubConnection connection)
    {
        connection.On<Message, object>("EventResponse", (message, data) =>
        {
            if (!LoaderCancellationTokenSource.IsCancellationRequested)
                LoaderCancellationTokenSource.Cancel();
            message.PrintFormattedData(data);
        });
    }
}