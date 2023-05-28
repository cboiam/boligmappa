using Boligmappa.ConsoleBase;
using Boligmappa.Queue.Sqs;

await ConsoleRunner.Start(menu =>
    menu.Add("Get featured history posts", ServerEventHandler.SendEvent(MessageType.GetFeaturedHistoryPosts))
        .Add("Get popular users", ServerEventHandler.SendEvent(MessageType.GetPopularUsers))
        .Add("Get mastercard users", ServerEventHandler.SendEvent(MessageType.GetMasterCardUsers)));