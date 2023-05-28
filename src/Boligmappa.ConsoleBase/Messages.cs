using System.Text.RegularExpressions;
using Boligmappa.Queue.Sqs;

namespace Boligmappa.ConsoleBase;

public static class Messages
{
    public static Message Get(MessageType type, string connectionId) =>
        new Message
        {
            User = connectionId,
            Type = type
        };

    public static void PrintFormattedData(this Message message, string data)
    {
        var messageTitle = message.Type switch
        {
            MessageType.GetFeaturedHistoryPosts => "Featured History Posts",
            MessageType.StoreUsers => "Stored Users",
            MessageType.GetPopularUsers => "Popular Users",
            MessageType.GetMasterCardUsers => "Master Card Users",
            MessageType.StorePosts => "Stored Posts",
            _ => throw new NotImplementedException()
        };

        var regex = new Regex(@"[,\{\}\[\]""]");
        var messageData = regex.Replace(data, string.Empty);

        Console.WriteLine($"{messageTitle}\n{messageData}\n");
    }
}