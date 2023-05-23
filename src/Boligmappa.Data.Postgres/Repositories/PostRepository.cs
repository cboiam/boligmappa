using Boligmappa.Core.Repositories;
using Boligmappa.Data.Postgres.Models;
using Boligmappa.Data.Postgres.Repositories.Abstractions;
using Entities = Boligmappa.Core.Entities;

namespace Boligmappa.Data.Postgres.Repositories;

public class PostRepository : IPostRepository
{
    private readonly IRepository<Post, Entities.Post> repository;

    public PostRepository(IRepository<Post, Entities.Post> repository)
    {
        this.repository = repository;
    }

    public async Task SaveAll(IEnumerable<Entities.Post> posts)
    {
        foreach (var post in posts)
        {
            var newPost = new Post
            {
                Id = post.Id,
                Title = post.Title,
                Body = post.Body,
                UserId = post.UserId,
                Tags = post.Tags.Select(t =>
                new Tag
                {
                    Name = t,
                    PostId = post.Id
                }).ToList(),
                Reactions = post.Reactions,
            };

            if (await repository.Exists(newPost.Id))
            {
                await repository.Update(newPost);
                return;
            }
            await repository.Add(newPost);
        }
    }
}