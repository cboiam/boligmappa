using Boligmappa.Core.Entities;

namespace Boligmappa.Core.Handlers.Abstractions;

public interface IPostHandler
{
    Task<IEnumerable<Post>> GetFeaturedHistoryPosts();
    Task<IEnumerable<Post>> StorePosts();        
}