using Boligmappa.Core.Entities;

namespace Boligmappa.Core.Services;

public interface IPostService
{
    Task<IEnumerable<Post>> GetPosts();
}