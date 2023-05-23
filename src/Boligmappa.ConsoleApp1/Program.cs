using Boligmappa.ConsoleBase;

ConsoleRunner.Start(menu =>
    menu.Add("Get featured history posts", ServerEventHandler.SendEvent(Messages.GetFeaturedHistoryPostsMessage))
        .Add("Get popular users", ServerEventHandler.SendEvent(Messages.GetPopularUsers))
        .Add("Get mastercard users", ServerEventHandler.SendEvent(Messages.GetMasterCardUsers)));