using Boligmappa.Core.Entities.Abstractions;

namespace Boligmappa.Core.Entities;

public class Post : Entity
{
    public string Title { get; init; }
    public string Body { get; init; }
    public int UserId { get; init; }
    public string UserName { get; init; }
    public IEnumerable<string> Tags { get; init; }
    public int Reactions { get; init; }
    public bool HasHistoryTag => ContainTag("history");
    public bool HasFrenchTag => ContainTag("french");
    public bool HasFictionTag => ContainTag("fiction");
    public bool HasReactions => Reactions > 0;

    private bool? hasMorethanTwoReactions;
    public bool HasMorethanTwoReactions
    {
        get { return hasMorethanTwoReactions ?? Reactions > 2; }
        set { hasMorethanTwoReactions = value; }
    }

    public Post(int id) : base(id) { }

    private bool ContainTag(string tag) =>
        Tags?.Contains(tag, StringComparer.InvariantCultureIgnoreCase) == true;
}