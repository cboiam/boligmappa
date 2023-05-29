using Boligmappa.Core.Repositories;
using Boligmappa.Data.Postgres.Models;
using Boligmappa.Data.Postgres.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
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
            Post existingPost = await repository.DbSet.Where(u => u.Id == post.Id)
                .AsNoTracking()
                .Include(u => u.Tags)
                .FirstOrDefaultAsync();

            if (existingPost != null)
            {
                var newTags = post.Tags.Where(t => existingPost.Tags?.Any(e => e.Name == t) == false)
                    .Select(t => new Tag { Name = t, PostId = existingPost.Id });

                existingPost.Body = post.Body;
                existingPost.Title = post.Title;
                existingPost.UserId = post.UserId;
                existingPost.Tags.AddRange(newTags);
                existingPost.Reactions = post.Reactions;
                existingPost.HasMorethanTwoReactions = post.HasMorethanTwoReactions;
                
                await repository.Update(existingPost);
                return;
            }

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
                HasMorethanTwoReactions = post.HasMorethanTwoReactions
            };
            await repository.Add(newPost);
        }
    }
}