using Boligmappa.Core.Entities;
using Boligmappa.Core.Handlers.Abstractions;
using Boligmappa.Core.Repositories;
using Boligmappa.Core.Services;
using Microsoft.Extensions.Logging;

namespace Boligmappa.Core.Handlers;

public class PostHandler : IPostHandler
{
    private readonly IPostService postService;
    private readonly IPostRepository postRepository;
    private readonly ILogger<PostHandler> logger;

    public PostHandler(IPostService postService, IPostRepository postRepository, ILogger<PostHandler> logger)
    {
        this.postService = postService;
        this.postRepository = postRepository;
        this.logger = logger;
    }

    public async Task<IEnumerable<Post>> GetFeaturedHistoryPosts()
    {
        logger.LogInformation("Loading featured posts");
        var posts = await postService.GetPosts();
        var featuredPosts = posts.Where(p => p.HasReactions && p.HasHistoryTag);

        logger.LogInformation("Featured posts loaded: {count}", featuredPosts.Count());
        return featuredPosts;
    }

    public async Task<IEnumerable<Post>> StorePosts()
    {
        logger.LogInformation("Loading posts");
        var posts = await postService.GetPosts();

        logger.LogInformation("Saving posts");
        await postRepository.SaveAll(posts);

        logger.LogInformation("Posts loaded: {count}", posts.Count());
        return posts;
    }
}