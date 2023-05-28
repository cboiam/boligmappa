using Boligmappa.Queue.Sqs;
using Microsoft.AspNetCore.SignalR.Client;

namespace Boligmappa.ConsoleBase;

public static class ServerEventHandler
{
    private static HubConnection Connection { get; set; }
    private static Menu Menu { get; set; }
    private static CancellationToken EventCancellationToken { get; set; }
    private static CancellationTokenSource LoaderCancellationTokenSource { get; set; } = new();
    private static Loader loader = new Loader();

    public static void Configure(HubConnection connection, Menu menu, CancellationToken eventCancellationToken)
    {
        Connection = connection;
        Menu = menu;
        EventCancellationToken = eventCancellationToken;
        ConfigureEventResponseHandling();
    }

    public static Func<Task> SendEvent(MessageType type)
    {
        return async () =>
        {
            var message = Messages.Get(type, Connection.ConnectionId);
            await Connection.InvokeAsync("SendEvent", message, EventCancellationToken);

            LoaderCancellationTokenSource.TryReset();
            await loader.Spin(LoaderCancellationTokenSource.Token);
        };
    }

    public static void ConfigureEventResponseHandling()
    {
        Connection.On<Message, string>("EventResponse", async (message, data) =>
        {
            Console.Clear();
            LoaderCancellationTokenSource.Cancel();
            message.PrintFormattedData(data);

            Console.Write("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();

            await Menu.Display();
        });
    }
}