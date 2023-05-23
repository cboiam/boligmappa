using System.Text.Json;
using System.Text.RegularExpressions;
using Boligmappa.Configuration;
using Boligmappa.Queue.Sqs;

namespace Boligmappa.ConsoleBase;

public static class Messages
{
    public static string ConnectionId { get; set; }

    public static Message GetFeaturedHistoryPostsMessage => new Message
    {
        User = ConnectionId,
        Type = MessageType.GetFeaturedHistoryPosts
    };

    public static Message GetPopularUsers => new Message
    {
        User = ConnectionId,
        Type = MessageType.GetPopularUsers
    };

    public static Message GetMasterCardUsers => new Message
    {
        User = ConnectionId,
        Type = MessageType.GetMasterCardUsers
    };

    public static Message StorePosts => new Message
    {
        User = ConnectionId,
        Type = MessageType.StorePosts
    };

    public static void PrintFormattedData(this Message message, object data)
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

        var messageData = JsonSerializer.Serialize(data, Configurations.SerializerOptions);
        var regex = new Regex(@"[,\{\}\[\]""]");
        messageData = regex.Replace(messageData, string.Empty);

        Console.WriteLine($"{messageTitle}\n{messageData}\n");
    }
}