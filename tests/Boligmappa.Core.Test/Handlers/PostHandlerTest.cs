using Boligmappa.Core.Entities;
using Boligmappa.Core.Handlers;
using Boligmappa.Core.Repositories;
using Boligmappa.Core.Services;
using Microsoft.Extensions.Logging;

namespace Boligmappa.Core.Test.Handlers;

public class PostHandlerTest
{
    private readonly Mock<IPostService> postService;
    private readonly Mock<IPostRepository> postRepository;
    private readonly PostHandler instance;

    public PostHandlerTest()
    {
        postService = new Mock<IPostService>();
        postRepository = new Mock<IPostRepository>();

        instance = new PostHandler(postService.Object,
            postRepository.Object,
            Mock.Of<ILogger<PostHandler>>());
    }

    [Fact]
    public async Task GetFeaturedHistoryPosts_EmptyResults_ReturnsEmpty()
    {
        postService.Setup(x => x.GetPosts())
            .ReturnsAsync(new List<Post>());

        var result = await instance.GetFeaturedHistoryPosts();

        result.Should().BeEmpty();
    }

    [Fact]
    public async Task GetFeaturedHistoryPosts_PostOnlyHasReaction_ReturnsEmpty()
    {
        var post = new Post(1)
        {
            Reactions = 1
        };

        postService.Setup(x => x.GetPosts())
            .ReturnsAsync(new List<Post> { post });

        var result = await instance.GetFeaturedHistoryPosts();

        result.Should().BeEmpty();
    }

    [Fact]
    public async Task GetFeaturedHistoryPosts_PostOnlyHasHistoryTag_ReturnsEmpty()
    {
        var post = new Post(1)
        {
            Tags = new List<string> { "history" }
        };

        postService.Setup(x => x.GetPosts())
            .ReturnsAsync(new List<Post> { post });

        var result = await instance.GetFeaturedHistoryPosts();

        result.Should().BeEmpty();
    }

    [Fact]
    public async Task GetFeaturedHistoryPosts_FeaturedPost_ReturnsPost()
    {
        var post = new Post(1)
        {
            Reactions = 1,
            Tags = new List<string> { "history" }
        };

        postService.Setup(x => x.GetPosts())
            .ReturnsAsync(new List<Post> { post });

        var result = await instance.GetFeaturedHistoryPosts();

        result.Should().NotBeEmpty()
            .And.Contain(post);
    }

    [Fact]
    public async Task StorePosts_ShouldSaveAllPosts()
    {
        var posts = new List<Post> { 
            new Post(1)
            {
                Reactions = 1
            }
        };

        postService.Setup<Task<IEnumerable<Post>>>(x => x.GetPosts())
            .ReturnsAsync(posts);

        var result = await instance.StorePosts();

        result.Should().BeEquivalentTo(posts);
        postRepository.Verify(x => x.SaveAll(posts));
    }
}