using Boligmappa.Core.Models;
using Entities = Boligmappa.Core.Entities;

namespace Boligmappa.Service.DummyJson.Models;

public class Post : Model<Entities.Post>
{
    public string Title { get; set; }
    public string Body { get; set; }
    public int UserId { get; set; }
    public List<string> Tags { get; set; }
    public int Reactions { get; set; }

    public override Entities.Post ToEntity() =>
        new Entities.Post(Id)
        {
            Title = Title,
            Body = Body,
            UserId = UserId,
            Tags = Tags,
            Reactions = Reactions
        };
}