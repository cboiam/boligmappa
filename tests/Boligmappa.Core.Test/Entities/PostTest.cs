using Boligmappa.Core.Entities;

namespace Boligmappa.Core.Test.Entities;

public class PostTest
{
    [Theory]
    [InlineData("history")]
    [InlineData("History")]
    public void Post_WithHistoryTag_HasHistoryTagShouldBeTrue(string tag)
    {
        new Post(1)
        {
            Tags = new List<string> { tag }
        }.HasHistoryTag.Should().BeTrue();
    }

    [Fact]
    public void Post_WithEmptyList_HasHistoryTagShouldBeFalse()
    {
        new Post(1).HasHistoryTag.Should().BeFalse();
    }

    [Fact]
    public void Post_WithoutHistoryTag_HasHistoryTagShouldBeFalse()
    {
        new Post(1)
        {
            Tags = new List<string> { "tag" }
        }.HasHistoryTag.Should().BeFalse();
    }

    [Theory]
    [InlineData("fiction")]
    [InlineData("Fiction")]
    public void Post_WithFictionTag_HasFictionTagShouldBeTrue(string tag)
    {
        new Post(1)
        {
            Tags = new List<string> { tag }
        }.HasFictionTag.Should().BeTrue();
    }

    [Fact]
    public void Post_WithEmptyList_HasFictionTagShouldBeFalse()
    {
        new Post(1).HasFictionTag.Should().BeFalse();
    }

    [Fact]
    public void Post_WithoutFictionTag_HasFictionTagShouldBeFalse()
    {
        new Post(1)
        {
            Tags = new List<string> { "tag" }
        }.HasFictionTag.Should().BeFalse();
    }

    [Theory]
    [InlineData("french")]
    [InlineData("French")]
    public void Post_WithFrenchTag_HasFrenchTagShouldBeTrue(string tag)
    {
        new Post(1)
        {
            Tags = new List<string> { tag }
        }.HasFrenchTag.Should().BeTrue();
    }

    [Fact]
    public void Post_WithEmptyList_HasFrenchTagShouldBeFalse()
    {
        new Post(1).HasFrenchTag.Should().BeFalse();
    }

    [Fact]
    public void Post_WithoutFrenchTag_HasFrenchTagShouldBeFalse()
    {
        new Post(1)
        {
            Tags = new List<string> { "tag" }
        }.HasFrenchTag.Should().BeFalse();
    }

    [Fact]
    public void Post_WithOneReaction_HasReactionShouldBeTrue()
    {
        new Post(1)
        {
            Reactions = 1
        }.HasReactions.Should().BeTrue();
    }

    [Fact]
    public void Post_WithZeroReaction_HasReactionShouldBeFalse()
    {
        new Post(1)
        {
            Reactions = 0
        }.HasReactions.Should().BeFalse();
    }

    [Fact]
    public void Post_WithThreeReactions_HasMoreThenTwoReactionsShouldBeTrue()
    {
        new Post(1)
        {
            Reactions = 3
        }.HasMorethanTwoReactions.Should().BeTrue();
    }


    [Fact]
    public void Post_WithTwoReactions_HasMoreThenTwoReactionsShouldBeFalse()
    {
        new Post(1)
        {
            Reactions = 2
        }.HasMorethanTwoReactions.Should().BeFalse();
    }
}