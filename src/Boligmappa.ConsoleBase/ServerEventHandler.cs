using Boligmappa.Queue.Sqs;
using Microsoft.AspNetCore.SignalR.Client;

namespace Boligmappa.ConsoleBase;

public static class ServerEventHandler
{
    private static HubConnection Connection { get; set; }
    private static Menu Menu { get; set; }
    private static CancellationTokenSource EventCancellationToken { get; set; }
    private static int Timeout { get; set; }
    private static string waitingResponse = null;
    private static Task loading;

    public static void Configure(HubConnection connection, Menu menu, CancellationTokenSource eventCancellationToken, int timeout)
    {
        Connection = connection;
        Menu = menu;
        EventCancellationToken = eventCancellationToken;
        Timeout = timeout;
        ConfigureEventResponseHandling();
    }

    public static Func<Task> SendEvent(MessageType type)
    {
        return async () =>
        {
            var message = Messages.Get(type, Connection.ConnectionId);
            await Connection.InvokeAsync("SendEvent", message, EventCancellationToken.Token);

            waitingResponse = message.User;

            try
            {
                Task task = Loader.Spin();
                loading = task.WaitAsync(TimeSpan.FromMilliseconds(Timeout));
            }
            catch (System.TimeoutException)
            {
                Loader.Stop();
                Console.Clear();
                Console.WriteLine("Server took too long to respond");
                Console.WriteLine("Shutting down...");
                EventCancellationToken.CancelAfter(4000);
            }
        };
    }

    public static void ConfigureEventResponseHandling()
    {
        Connection.On<Message, string>("EventResponse", async (message, data) =>
        {
            if (waitingResponse != message.User)
            {
                return;
            }
            waitingResponse = null;

            Loader.Stop();
            await loading;

            Console.Clear();
            message.PrintFormattedData(data);

            Console.Write("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();

            await Menu.Display(Timeout, EventCancellationToken);
        });
    }
}