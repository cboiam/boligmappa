using Boligmappa.Queue.Sqs;
using Boligmappa.Queue.Sqs.Queues.Abstractions;
using Microsoft.AspNetCore.SignalR;

namespace Boligmappa.Server;

public class AppHub : Hub
{
    private readonly ISqsQueue sqsQueue;

    public AppHub(ISqsQueue sqsQueue)
    {
        this.sqsQueue = sqsQueue;
    }

    public async Task SendEvent(Message sqsEvent) =>
        await sqsQueue.SendMessage(sqsEvent);

    public async Task SendEventResponse(Message sqsEvent, string data) =>
        await Clients.All.SendAsync("EventResponse", sqsEvent, data);
}