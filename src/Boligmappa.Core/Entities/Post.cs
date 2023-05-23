using Boligmappa.Core.Entities.Abstractions;

namespace Boligmappa.Core.Entities;

public record Post : Entity
{
    public string Title { get; init; }
    public string Body { get; init; }
    public int UserId { get; init; }
    public string UserName { get; init; }
    public IEnumerable<string> Tags { get; init; }
    public int Reactions { get; init; }
    public bool HasHistoryTag => Tags.Contains("history");
    public bool HasFrenchTag => Tags.Contains("french");
    public bool HasFictionTag => Tags.Contains("fiction");
    public bool HasReactions => Reactions > 0;
    public bool HasMorethanTwoReactions => Reactions > 2;

    public Post(int id) : base(id) { }
}