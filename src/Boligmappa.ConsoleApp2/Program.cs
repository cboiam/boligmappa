using Boligmappa.ConsoleBase;
using Boligmappa.Queue.Sqs;

await ConsoleRunner.Start(menu =>
    menu.Add("Store posts", ServerEventHandler.SendEvent(MessageType.StorePosts)));