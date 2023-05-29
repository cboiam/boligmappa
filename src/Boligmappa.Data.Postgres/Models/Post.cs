namespace Boligmappa.Data.Postgres.Models;

using Boligmappa.Core.Models;
using Entities = Boligmappa.Core.Entities;

public class Post : Model<Entities.Post>
{
    public string Title { get; set; }
    public string Body { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public List<Tag> Tags { get; set; }
    public int Reactions { get; set; }
    public bool HasMorethanTwoReactions { get; set; }

    public override Entities.Post ToEntity()
    {
        return new Entities.Post(Id)
        {
            Title = Title,
            Body = Body,
            UserId = UserId,
            UserName = User.UserName,
            Tags = Tags?.Select(t => t.Name),
            Reactions = Reactions,
            HasMorethanTwoReactions = HasMorethanTwoReactions
        };
    }
}