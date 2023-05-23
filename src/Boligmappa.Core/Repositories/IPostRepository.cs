using Boligmappa.Core.Entities;

namespace Boligmappa.Core.Repositories;

public interface IPostRepository
{
    Task SaveAll(IEnumerable<Post> posts);
}