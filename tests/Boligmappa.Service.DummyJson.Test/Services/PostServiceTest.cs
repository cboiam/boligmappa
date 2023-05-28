using System.Net;
using Boligmappa.Core.Entities;

namespace Boligmappa.Service.DummyJson.Test.Services;

public class PostServiceTest : ServiceTest
{
    private readonly PostService instance;

    public PostServiceTest() : base()
    {
        instance = new PostService(options);
    }

    [Fact]
    public async Task GetPosts_ValidateRequest()
    {
        using (var httpTest = new HttpTest())
        {
            var result = await instance.GetPosts();

            httpTest.ShouldHaveCalled("https://dummyjson.com/posts")
                .WithQueryParam("limit", 0);
        }
    }

    [Fact]
    public async Task GetPosts_EmptyResult()
    {
        using (var httpTest = new HttpTest())
        {
            httpTest.RespondWith(@"{ ""posts"": [] }",
                HttpStatusCode.OK.GetHashCode());

            var result = await instance.GetPosts();

            result.Should().BeEmpty();
        }
    }

    [Fact]
    public async Task GetPosts_ReturnPost_ShouldBeMappedCorrectly()
    {
        using (var httpTest = new HttpTest())
        {
            httpTest.RespondWith(@"
                { 
                    ""posts"": [{ 
                        ""id"": 1, 
                        ""userId"": 1, 
                        ""title"": ""title"", 
                        ""body"": ""body"",
                        ""reactions"": 3,
                        ""tags"": [
                            ""tag""    
                        ]
                    }]
                }",
                HttpStatusCode.OK.GetHashCode());

            var result = await instance.GetPosts();

            result.Should().NotBeEmpty()
                .And.HaveCount(1)
                .And.ContainEquivalentOf(new Post(1)
                {
                    UserId = 1,
                    Title = "title",
                    Body = "body",
                    Reactions = 3,
                    Tags = new List<string> { "tag" }
                });
        }
    }

    [Fact]
    public async Task GetPosts_PropagateExceptionOnInternalServerError()
    {
        using (var httpTest = new HttpTest())
        {
            httpTest.RespondWith(string.Empty, HttpStatusCode.InternalServerError.GetHashCode());

            Func<Task> call = async () => await instance.GetPosts();

            await call.Should().ThrowAsync<FlurlHttpException>();
        }
    }
}