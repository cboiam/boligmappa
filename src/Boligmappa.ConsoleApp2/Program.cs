using Boligmappa.ConsoleBase;

ConsoleRunner.Start(menu =>
    menu.Add("Store posts", ServerEventHandler.SendEvent(Messages.StorePosts)));